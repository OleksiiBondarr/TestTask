using System;
using System.Collections.Generic;
using System.Linq;

namespace TestTask
{
    class Program
    {
        private static int num = 0;
        private static List<int> masses = null;
        private static List<int> starts = null;
        private static List<int> target = null;
        static void Main(string[] args)
        {
            //Validating number of elephants
            num = GetIntFromConsole("Enter number of elephants");
            //Validataing masses
            masses = GetListFromConsole("Enter masses of elephants using \" \" between", 1);
            //Validating start positions
            starts = GetListFromConsole("Enter start positions of elephants using \" \" between", 2);
            //Validating target positions
            target = GetListFromConsole("Enter target positions of elephants using \" \" between", 2);

            Replacer rp = new Replacer(num, starts, target, masses);
            rp.FindLoops();
            Console.WriteLine("correct result:\n{0}", rp.Calculations());
        }

        //generic method to get convert string into appropriate format 
        private static T GetFromConsole<T>(string message, int argType, Func<string, int, T> StringConv)
        {
            return StringConv(ConsoleWriteRead(message), argType); ;
        }

        //used for ints
        private static int GetIntFromConsole(string message)
        {
            Func<string, int, int> ConvInt = (x, y) => ToInt(x);
            int res = GetFromConsole<int>(message, 0, ConvInt);
            return ((res >= 1000000) || (res < 2)) ? GetIntFromConsole(message) : res;
        }

        //used for lists
        private static List<int> GetListFromConsole(string message, int argType)
        {
            Func<string, int, List<int>> ConvList = (x, y) => ValidInput(x, y);
            List<int> res = GetFromConsole<List<int>>(message, argType, ConvList);
            return res is null ? GetListFromConsole(message, argType) : res;
        }

        //Method for Parrsing and validating input
        private static List<int> ValidInput(String line, int checkType)
        {
            List<int> tmp = line.Split(" ").ToList().Select(s => ToInt(s)).ToList();
            if (checkType == 1)
                tmp = tmp.Where(elem => (elem <= 6500) && (elem >= 100)).ToList();
            else
                tmp = tmp.Where(elem => (elem <= num) && (elem > 0)).Distinct().ToList();
            if (tmp.Count == num)
                return tmp;
            Console.WriteLine("Enter data in the correct format");
            return null;
        }

        //Method for Console Operations
        private static string ConsoleWriteRead(string message)
        {
            Console.WriteLine(message);
            return Console.ReadLine();
        }

        //Simple Converting String to Int
        private static int ToInt(string value)
        {
            int n;
            if (!int.TryParse(value, out n))
            {
                Console.WriteLine("Enter number using digits");
                return 0;
            }
            return n;
        }
    }
}
