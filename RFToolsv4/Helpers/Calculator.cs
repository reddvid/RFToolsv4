﻿using RFToolsv4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitsNet;
using UnitsNet.Units;

namespace RFToolsv4.Helpers
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
            Length meter = Length.FromMeters(skinDepthInMeters);

            return ResultsBuilder.Build(new()
            {
                new Result(name: "Skin Depth", value: meter.Micrometers, units: "microns")
            });
        }

        public static string WavelengthFrequency(double input, string unknown)
        {
            // Wavelength formula
            // w = c / f
            double result = Constants.SPEED_OF_LIGHT / input;
            string unit = unknown.Equals("Wavelength") ? "MHz" : "m";

            return ResultsBuilder.Build(new()
            {
                new Result(name: unknown, value: result, units: unit),
            });
        }

        public static string ConvertPEC(double input, string setting)
        {
            // Power = Watts, dBm, dBW
            // Energy = Wh, BTU, kilojouls, electronvolt, kilocalorie
            // Charge = Coulomb, electron charge, Ah
            List<Result> results = new();

            if (setting.Equals("Power:"))
            {
                double milliWatts = input;
                double dBm = 10 * Math.Log10(milliWatts);
                double dBW = 10 * Math.Log10(milliWatts / 1000);

                results = new()
                {
                    new Result(name: "Watts", value: milliWatts, units: "W"),
                    new Result(name: "dBm", value: dBm, units: "dBm"),
                    new Result(name: "dBW", value: dBW, units: "dBW"),
                };
            }

            return ResultsBuilder.Build(results);
        }

        public static string StandingWave(double input, string known)
        {
            List<Result> results = new();
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
    }
}
