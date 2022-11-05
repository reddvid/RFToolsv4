﻿using RFToolsv4.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFToolsv4.Helpers
{
    public class ResultsBuilder
    {
        public static string Build(List<Result> results)
        {
            StringBuilder builder = new();
            foreach (var result in results)
            {
                builder.AppendLine($"{result.Name}: {result.Value} {result.Units}");
            }
            return builder.ToString();
        }
    }
}