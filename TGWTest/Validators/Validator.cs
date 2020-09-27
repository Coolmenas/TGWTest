using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TGWTest.Validators
{
    class Validator
    {
        public bool ValidateSection(string section)
        {
            List<string> allowedSectionNames = new List<string>()
            {
                "Data Generation",
                "System Config",
                "Shuttle System"
            };
            if (allowedSectionNames.Contains(section))
                return true;
            else
            {
                Console.WriteLine($"Not a valid section name: {section}");
                return false;
            }
        }
        public bool ValidateSubSection(string subSection)
        {
            List<string> allowedSectionNames = new List<string>()
            {
                "Order Profile",
                "Shuttle System",
                "Power Supply",
                "Results",
                "Structure"
            };
            if (allowedSectionNames.Contains(subSection))
            {
                return true;
            }
            else
            {
                Console.WriteLine($"Not a valid sub-section name: {subSection}");
                return false;
            }
        }
        public bool ValidateParameter(string key, dynamic value)
        {
            try
            {
                switch (key)
                {
                    case "ordersPerHour":
                        int val = 0;
                        int.TryParse(value, out val);
                        if (val < 50000 && val > 0)
                            return true;
                        Console.WriteLine($"{key} value not in range ({value})");
                        return false;
                    case "orderLinesPerOrder":
                        return true;
                    case "inboundStrategy":
                        var values = new List<string> { "random", "optimized" };
                        if (values.Contains(value))
                            return true;
                        Console.WriteLine($"{key} value not recognised");
                        return false;
                    case "powerSupply":
                        return true;
                    case "resultStartTime":
                        return true;
                    case "resultInterval":
                        return true;
                    case "numberOfAisles":
                        return true;
                    default:
                        return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
