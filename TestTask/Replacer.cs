using FsCheck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestTask
{   
    public class Replacer
    {
        public int Number { get; set; }
        public List<Tuple<int, int>> Pairs { get; set; }
        public Dictionary<int, int> MassDict { get; set; }
        public List<List<int>> Loops { get; set; }
        public Replacer(int num, List<int> start, List<int> target, List<int> masses)
        {
            Number = num;
            Pairs = start.Zip(target, (a, b) => new Tuple<int, int>(a, b)).ToList();
            List<int> l = new List<int>();
            l.AddRange(Enumerable.Range(1, num));
            MassDict = l.Zip(masses, (a, b) => new {a, b}).ToDictionary(item => item.a, item => item.b);
        }
        //Find all the loops which are longer than 1( 1 length loops have no influence on the result)
        public void FindLoops()
        {
            this.Loops = new List<List<int>>();
            Dictionary<int, bool> checks = new Dictionary<int, bool>();
            for (int i = 1; i <= Number; i++)
            {
                checks.Add(i, true);
            }
            foreach (Tuple<int, int> item in this.Pairs)
            {
                List<int> tmpLoop = new List<int>();
                if (checks[item.Item1])
                {
                    tmpLoop.Add(item.Item1);
                    checks[item.Item1] = false;
                    bool tmpCheck = false;
                    Tuple<int, int> tmpitem1 = item;
                    do {
                        Tuple<int, int>  item2 = this.Pairs.Where(elem => elem.Item1 == tmpitem1.Item2).ToList()[0];
                        tmpCheck = checks[item2.Item1];
                        if (tmpCheck)
                        {
                            checks[item2.Item1] = false;
                            tmpLoop.Add(item2.Item1);
                            tmpitem1 = item2;
                        }
                    } while (tmpCheck);
                }
                if (tmpLoop.Count > 1)
                    this.Loops.Add(tmpLoop);
            }
        }
        //Calculate 2 methods and choose best one
        public int Calculations()
        {
            int minMassGlobal = MassDict.Values.Min();
            int math1Calcs = 0;
            int math2Calcs = 0;
            foreach (List<int> loop in this.Loops){
                int sumMassLoop = loop.Select(x => MassDict[x]).Sum();
                int minMassLoop = loop.Select(x => MassDict[x]).Min();
                math1Calcs += sumMassLoop + (loop.Count - 2) * minMassLoop;
                math2Calcs += sumMassLoop + minMassLoop + (loop.Count + 1) * minMassGlobal;
            }
            return Math.Min(math1Calcs,math2Calcs);
        }

    }
}
