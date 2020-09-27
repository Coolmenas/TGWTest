using System;
using System.Collections.Generic;
using TGWTest.Parsers;

namespace TGWTest
{
    class TGWTest
    {
        static void Main(string[] args)
        {
            //Jei dar tobulinant tai tikriausiai padaryciau:
            //gudresni failu eiles pasirinkima kad nereiketu atsidayt kodo ir perkompiliuot
            //Parameter klaseje optiona nurodyti kokio tipo tai value.
            //Parametro access galbut padaryti pagal pilna path rakta ("Section", "SubSection","Key")
            var configParser = new ConfigParser("Data/Base_Config.txt", "Data/Project_Config.txt");
            var parameters = configParser.LoadAllParameters();
            var printer = new Printer();
            printer.PrintAllParameters(parameters);
            printer.PrintParameter(
                parameters, new List<string>(){
                "NumberOfSystems",
                "OrdersPerHour",
                "OrderLinesPerOrder",
                "ResultStartTime"
                });
            Console.ReadKey();
        }
    }
}
