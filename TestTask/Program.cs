using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TestTask
{
    public class Program
    {
        private static int num = 0;
        private static List<int> masses = null;
        private static List<int> starts = null;
        private static List<int> target = null;
        static void Main(string[] args)
        {
            Console.WriteLine(ProvideResult(args[0]));
        }
        //Method to calculate data from file
        public static int ProvideResult(string filename)
        {
            string[] lines = File.ReadAllLines(filename);
            num = ToInt(lines[0]);
            masses = ValidInput(lines[1], 1);
            starts = ValidInput(lines[2], 2);
            target = ValidInput(lines[3], 2);
            if ((num > 1000000) || (num < 2) || (masses is null) || (starts is null) || (target is null))
                return 0;
            Replacer rp = new Replacer(num, starts, target, masses);
            rp.FindLoops();
            return rp.Calculations();
        }

        //Method for Parsing and validating list input
        private static List<int> ValidInput(String line, int checkType)
        {
                List<int> tmp = line.Split(" ").ToList().Select(s => ToInt(s)).ToList();
                if (checkType == 1)
                    tmp = tmp.Where(elem => (elem <= 6500) && (elem >= 100)).ToList();
                else
                    tmp = tmp.Where(elem => (elem <= num) && (elem > 0)).Distinct().ToList();
                if (tmp.Count == num)
                    return tmp;
                return null;
        }

        //Simple Converting String to Int
        public static int ToInt(string value)
        {
            int n;
            if (!int.TryParse(value, out n))
            { 
                return 0;
            }
            return n;
        }
    }
}
