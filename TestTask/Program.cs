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
            do {
                Console.WriteLine("Enter number of elephants");
                String tmpNum = Console.ReadLine();
                num = ToInt32(tmpNum);   
            } while ((num<2)||(num>= 1000000));
            
            //Validataing masses
            do {
                Console.WriteLine("Enter masses of elephants using \" \" between");
                String tmpMasses = Console.ReadLine();
                masses = ValidInput(num, tmpMasses, 1);
            } while (masses is null);

            //Validating start positions
            do
            {
                Console.WriteLine("Enter start positions of elephants using \" \" between");
                String tmpStarts = Console.ReadLine();
                starts = ValidInput(num, tmpStarts, 2);
            } while (starts is null);

            //Validating target positions
            do { 
                Console.WriteLine("Enter target positions of elephants using \" \" between");
                String tmpTarget = Console.ReadLine();
                target = ValidInput(num, tmpTarget, 2);
            } while (target is null);


            Replacer rp = new Replacer(num, starts, target, masses);
            rp.FindLoops();
            Console.WriteLine(rp.Calculations());
        }

        //Predicate for Validating that each position is correct
        private static Boolean RangeMass(int i)
        {
            if ((i <= 6500)&&(i >= 100))
                return true;
            return false;
        }

        //Predicate for Validating that each mass is correct
        private static Boolean RangeOrder(int i)
        {
            if ((i <= num) && (i > 0))
                return true;
            return false;
        }
        
        //Method for Parrsing and validating input
        private static List<int> ValidInput(int num, String line, int checkType)
        {
                List<int> tmp = line.Split(" ").ToList().Select(s => ToInt32(s)).ToList();
                if (checkType == 1)
                    tmp = tmp.FindAll(RangeMass);
                else
                    tmp = tmp.FindAll(RangeOrder);

                if (tmp.Count == num)
                    return tmp;
                Console.WriteLine("Enter data in the correct format");
                return null;
         
        }
        //Simple Converting String to Int
        public static int ToInt32(string value)
        {
            int n;
            if (!Int32.TryParse(value, out n))
            { 
                Console.WriteLine("Enter number using digits");
                return 0;
            }
            return n;
        }
    }
}
