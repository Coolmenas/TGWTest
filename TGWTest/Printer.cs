using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using TGWTest.Models;

namespace TGWTest
{
    class Printer
    {
        public void PrintAllParameters(List<Section> parameters)
        {
            Console.WriteLine();
            Console.WriteLine($"---------------------------- Printing Full Configuration ----------------------------");
            foreach (var section in parameters)
            {
                Console.WriteLine($"=== {section.Key} ===");
                foreach (var subSection in section.Value)
                {
                    Console.WriteLine($"- {subSection.Key}");

                    foreach (var parameter in subSection.Value)
                    {
                        Console.WriteLine($"{parameter.Key,-20}: {parameter.Value,10}\t//{parameter.Source,20}");
                    }
                    Console.WriteLine();
                }
            }
        }
        public void PrintParameter(List<Section> sectionList, List<string> keys)
        {
            Console.WriteLine();
            Console.WriteLine($"---------------------------- Printing Selected Parameters ----------------------------");
            List<Parameter> parameterList = new List<Parameter>();
            foreach (Section section in sectionList)
            {
                foreach (SubSection subSection in section.Value)
                {
                    foreach (Parameter par in subSection.Value)
                        parameterList.Add(par);
                }
            }
            foreach (string key in keys)
            {
                var tempPar = parameterList.FirstOrDefault(p => p.Key.ToLower() == key.ToLower());
                if (tempPar != null)
                    Console.WriteLine($"{tempPar.Key,-20}: { tempPar.Value,10}\t//{tempPar.Source,20}");
                else
                    Console.WriteLine($"{key} parameter not found in any configuration");
            }
        }
    }
}
