using System;
using TGWTest.Parsers;

namespace TGWTest
{
    class TGWTest
    {
        static void Main(string[] args)
        {
            var configParser = new ConfigParser("Data/Base_Config.txt", "Data/Project_Config.txt");
            var parameters = configParser.LoadAllParameters();
            var printer = new Printer();
            printer.PrintAllParameters(parameters);
            Console.ReadKey();
        }
    }
}
