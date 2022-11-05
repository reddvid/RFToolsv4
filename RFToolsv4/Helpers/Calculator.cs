using RFToolsv4.Models;
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
    }
}
