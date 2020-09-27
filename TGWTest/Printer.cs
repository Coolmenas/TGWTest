using System;
using System.Collections.Generic;
using System.Text;
using TGWTest.Models;

namespace TGWTest
{
    class Printer
    {
        public void PrintAllParameters(List<Section> parameters)
        {
            foreach (var section in parameters)
            {
                Console.WriteLine($"=== {section.Key} ===");
                foreach (var subSection in section.Value)
                {
                    Console.WriteLine($"- {subSection.Key}");

                    foreach (var parameter in subSection.Value)
                    {
                        Console.WriteLine($"{parameter.Key,-20}: {parameter.Value, 10}\t//{parameter.Source, 20}");
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
