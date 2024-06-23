using PInvoke;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using RFTools.Uwp.Models;
using UnitsNet;
using UnitsNet.Units;
using Constants = RFTools.Uwp.Models.Constants;

namespace RFTools.Uwp.Helpers
{
    public class Calculator
    {
        /// <summary>
        /// Calculate Skin Depth
        /// </summary>
        /// <param name="resistivity">The material resistivity in <b>Ohms/cm</b></param>
        /// <param name="permeability">Value is usually ~1</param>
        /// <param name="frequency">System frequency in <b>Hertz</b></param>
        public static string SkinDepth(double resistivity, double permeability, double frequency)
        {
            // Skin Depth formula
            // δ =√(ρ / (πf_Hz μ_R μ_o )) (meters) where μ_o = 4π x 10 ^ (-7)

            double denominator = Math.PI * frequency * permeability * 4 * Math.PI * Math.Pow(10, -7);
            double skinDepthInMeters = Math.Sqrt(resistivity / denominator);
           
            return ResultsBuilder.Build(new()
            {
                new Result(name: "Skin Depth", value: skinDepthInMeters, units: "m", isSI: true)
            });
        }

        public static string WavelengthFrequency(double input, double velocity, string unknown)
        {
            // Wavelength formula
            // w = c / f
            double result = velocity / input;

            return ResultsBuilder.Build(new()
            {
                new Result(name: unknown, value: result, units: unknown.Equals("Wavelength") ? "m" : "Hz", isSI: true),
            });
        }

        public static string ConvertPEC(double input, string setting)
        {
            // Power = Watts, dBm, dBW
            // Energy = Wh, BTU, kilojouls, electronvolt, kilocalorie
            // Charge = Coulomb, electron charge, Ah
            List<Result> results = new();

            if (setting.Contains("Power"))
            {
                double dBm = input;
                double milliWatts = Math.Pow(10, dBm / 10);
                double watts = milliWatts / 1_000;
                double dBW = dBm - 30;

                results = new()
                {
                    new Result(name: "Watts", value: watts, units: "W", isSI: true),
                    new Result(name: "dBm", value:  dBm, units: "dBm"),
                    new Result(name: "dBW", value:  dBW, units: "dBW"),
                };
            }
            else if (setting.Contains("Energy"))
            {
                double wattHour = input;
                double btu = input / Selectors.Energy[2].Multiplier;
                double kilojoule = input / Selectors.Energy[4].Multiplier;
                double electronvolt = input / Selectors.Energy[5].Multiplier;
                double kilocalorie = input / Selectors.Energy[6].Multiplier;

                results = new()
                {
                    new Result(name: "Watt-hour", value: wattHour, units: "Wh", isSI: true),
                    new Result(name: "BTU", value: btu, units: "BTU", isSI: true),
                    new Result(name: "Kilojoule", value: kilojoule, units: "kJ"),
                    new Result(name: "Electronvolt", value: electronvolt, units: "eV", isEngineering: true),
                    new Result(name: "Kilocalorie", value: kilocalorie, units: null),
                };
            }
            else
            {
                double coulomb = input;
                double electronCharge = input / Selectors.Charge[3].Multiplier;
                double ampereHour = input / Selectors.Charge[4].Multiplier;

                results = new()
                {
                    new Result(name: "Coulomb", value: coulomb, units: "C", isSI: true),
                    new Result(name: "Electron charge", value: electronCharge, units: "Cₑ", isEngineering: true),
                    new Result(name: "Ampere-hour", value: ampereHour, units: "Ah"),
                };
            }

            return ResultsBuilder.Build(results);
        }

        public static string StandingWave(double input, string known)
        {
            double vswr;
            double reflectionCoefficient;
            double returnLoss;
            if (known.Contains("VSWR"))
            {
                vswr = input;
                reflectionCoefficient = (vswr - 1) / (vswr + 1);
                returnLoss = -20 * Math.Log10(reflectionCoefficient);
            }
            else if (known.Contains("Reflection Coefficient"))
            {
                reflectionCoefficient = input;
                vswr = Math.Abs((reflectionCoefficient + 1) / (reflectionCoefficient - 1));
                returnLoss = -20 * Math.Log10(reflectionCoefficient);
            }
            else
            {
                returnLoss = input;
                reflectionCoefficient = Math.Pow(10, (returnLoss / -20));
                vswr = Math.Abs((reflectionCoefficient + 1) / (reflectionCoefficient - 1));
            }

            double mismatchLoss = -10 * Math.Log10(1 - reflectionCoefficient * reflectionCoefficient);

            return ResultsBuilder.Build(new()
            {
                new Result(name: "VSWR", value: vswr, units: null),
                new Result(name: "Reflection Coefficient (Γ)", value: reflectionCoefficient, units: null),
                new Result(name: "Return Loss", value: returnLoss, units: "dB"),
                new Result(name: "Mismatch Loss", value: mismatchLoss, units: "dB"),
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="firstValue"></param>
        /// <param name="secondValue"></param>
        /// <param name="unknown"></param>
        /// <returns></returns>
        public static string Resonance(double firstValue, double secondValue, string unknown)
        {
            double outputValue;
            string unit;

            if (unknown.Contains("Resonant Frequency"))
            {
                outputValue = 1 / (2 * Math.PI * Math.Sqrt(firstValue * secondValue));
                unit = "Hz";
            }
            else if (unknown.Contains("Capacitance"))
            {
                outputValue = 1 / (2 * Math.PI * firstValue * 2 * Math.PI * firstValue * secondValue);
                unit = "F";
            }
            else
            {
                outputValue = 1 / (2 * Math.PI * firstValue * 2 * Math.PI * firstValue * secondValue);
                unit = "H";
            }

            outputValue = SINumber.GetSI(outputValue).Value;
            unit = SINumber.GetSI(outputValue).Unit + unit;

            return ResultsBuilder.Build(new()
            {
                new Result(name: unknown, value: outputValue, units: unit),
            });
        }

        public static string DeltaWye(double first, double second, double third, string setting)
        {
            List<Result> results = new();
            double firstConvert;
            double secondConvert;
            double thirdConvert;
            string firstUnit;
            string secondUnit;
            string thirdUnit;

            if (setting.Equals("Delta to Wye"))
            {
                double denominator = first + second + third;
                firstConvert = second * third / denominator;
                secondConvert = first * third / denominator;
                thirdConvert = first * second / denominator;

                firstConvert = SINumber.GetSI(firstConvert).Value;
                secondConvert = SINumber.GetSI(secondConvert).Value;
                thirdConvert = SINumber.GetSI(thirdConvert).Value;

                firstUnit = SINumber.GetSI(firstConvert).Unit + "Ω";
                secondUnit = SINumber.GetSI(secondConvert).Unit + "Ω";
                thirdUnit = SINumber.GetSI(thirdConvert).Unit + "Ω";

                results = new()
                {
                    new Result(name: "R₁", value: firstConvert, units: firstUnit),
                    new Result(name: "R₂", value: secondConvert, units: secondUnit),
                    new Result(name: "R₃", value: thirdConvert, units: thirdUnit),
                };
            }
            else
            {
                double denominator = (first * second) + (second * third) + (third * first);
                firstConvert = denominator / first;
                secondConvert = denominator / second;
                thirdConvert = denominator / third;

                firstConvert = SINumber.GetSI(firstConvert).Value;
                secondConvert = SINumber.GetSI(secondConvert).Value;
                thirdConvert = SINumber.GetSI(thirdConvert).Value;

                firstUnit = SINumber.GetSI(firstConvert).Unit + "Ω";
                secondUnit = SINumber.GetSI(secondConvert).Unit + "Ω";
                thirdUnit = SINumber.GetSI(thirdConvert).Unit + "Ω";

                results = new()
                {
                    new Result(name: "Rᴀ", value: firstConvert, units: firstUnit),
                    new Result(name: "Rʙ", value: secondConvert, units: secondUnit),
                    new Result(name: "Rᴄ", value: thirdConvert, units: thirdUnit),
                };
            }

            return ResultsBuilder.Build(results);
        }

        /// <summary>
        /// Calculate Free-Space path Loss
        /// </summary>
        /// <param name="txGain">Transmitter Gain in dBi</param>
        /// <param name="rxGain">Receiver Gain in dBi</param>
        /// <param name="distance">Distance in kilometers</param>
        /// <param name="frequency">Frequency in Megahertz</param>
        /// <returns></returns>
        public static string PathLoss(double txGain, double rxGain, double distance, double frequency)
        {
            double pathLoss = (20 * Math.Log10(distance)) + (20 * Math.Log10(frequency)) + 32.44 - rxGain - txGain;

            return ResultsBuilder.Build(new()
            {
                new Result(name: "Path Loss", value: pathLoss, units: "dB"),
            });
        }

        /// <summary>
        /// Calculate Link Budget
        /// </summary>
        /// <param name="txPower">Transmitter output power in dBm</param>
        /// <param name="txGain">Transmitter Gain in dBi</param>
        /// <param name="txLoss">Transmitter Loss in dB</param>
        /// <param name="rxGain">Receiver Gain in dBi</param>
        /// <param name="rxLoss">Receiver Loss in dB</param>
        /// <param name="distance">System Distance in Kilometers</param>
        /// <param name="frequency">System Frequency in Megahertz</param>
        /// <returns></returns>
        public static string LinkBudget(double txPower, double txGain, double txLoss, double rxGain, double rxLoss, double distance, double frequency)
        {
            double pathLoss = (20 * Math.Log10(distance)) + (20 * Math.Log10(frequency)) + 32.44;
            double receivedPower = txPower + txGain + rxGain - txLoss - rxLoss - pathLoss;

            return ResultsBuilder.Build(new()
            {
                new Result(name: "Transmitted Power", value: txPower, units: "dBm"),
                new Result(name: "Received Power", value: receivedPower, units: "dBm"),
                new Result(name: "Path Loss", value: pathLoss, units: "dB"),
            });
        }

        public static string TwoWireLine(double impedance, double diameter, double dielectric, string unit = "")
        {
            double distance;
            double space;

            // distance = Math.Pow(Math.E, Math.Log(diameter) + ((Math.Sqrt(dielectric) * impedance) / 120)) / 2;
            distance = diameter * Math.Cosh(Math.PI * (impedance / 376.73) * Math.Sqrt(dielectric));
            space = distance - diameter;

            return ResultsBuilder.Build(new()
            {
                new Result(name: "Center-to-center Distance", value: distance, units: unit),
                new Result(name: "Space between Conductors", value: space, units: unit),
            });
        }

        public static string FresnelZone(double distance, double frequency, double obstruction = 0)
        {
            // Distance input is in meters
            // Frequency input is in Hertz
            List<Result> results = new();

            double distanceInKm = distance / 1_000;
            double frequencyInGHertz = frequency / 1_000_000_000;
            double obstructionInKm = obstruction / 1_000;

            double earthCurvature = (Math.Pow(distanceInKm, 2) / (8 * 6_378)) * 1_000;
            double sixtyPercentClearance = 8.657 * Math.Sqrt(0.6 * distanceInKm / frequencyInGHertz);
            double fresnelZone = 17.31 * Math.Sqrt(distanceInKm / (4 * frequencyInGHertz));

            if (obstruction == 0) // Calculate Fresnel zone radius, 60% clearance, earth curv height
            {
                results = new()
                {
                    new Result(name: $"Fresnel Zone Max Radius @ { distanceInKm / 2 } km", value: fresnelZone, units: "m"),
                    new Result(name: "60% Clearance Radius", value: sixtyPercentClearance, units: "m"),
                    new Result(name: "Earth Curvature Height", value: earthCurvature, units: "m"),
                };
            }
            else
            {
                results = new()
                {
                    new Result(name: $"Fresnel Zone Radius @ { obstructionInKm } km", value: fresnelZone, units: "km"),
                    new Result(name: "Earth Curvature Height", value: earthCurvature, units: "m"),
                };
            }

            return ResultsBuilder.Build(results);
        }

    }
}
