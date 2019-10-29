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
        public Dictionary<int, int> Mass { get; set; }
        public List<List<int>> Loops { get; set; }
        public Replacer(int num, List<int> st, List<int> tg, List<int> ms)
        {
            Number = num;
            Pairs = st.Zip(tg, (a, b) => new Tuple<int, int>(a, b)).ToList();
            List<int> l = new List<int>();
            l.AddRange(Enumerable.Range(1, num));
            Mass = l.Zip(ms, (a, b) => new {a, b}).ToDictionary(item => item.a, item => item.b);
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
                    int tmpitem = item.Item2;
                    
                    foreach (Tuple<int, int> item2 in this.Pairs)
                    {
                        if (checks[item2.Item1]&&(item2.Item1==tmpitem))
                        {
                            checks[item2.Item1] = false;
                            tmpLoop.Add(item2.Item1);
                            if (tmpLoop.Contains(item2.Item2))
                            {
                                break;
                            }
                            else
                            {
                                checks[item2.Item2] = false;
                                tmpLoop.Add(item2.Item2);
                            }
                        }
                    }
                }
                if (tmpLoop.Count>1) this.Loops.Add(tmpLoop);
            }
        }
        //Calculate 2 methods and find best one
        public int Calculations()
        {
            int minMassGlobal = Mass.OrderBy(a => a.Value).First().Value;
            int math1Calcs = 0;
            int math2Calcs = 0;
            foreach (List<int> l in this.Loops){
                int sumMassLoop = 0;
                int minMassLoop = Int32.MaxValue;
                foreach (int elephant in l)
                {
                    sumMassLoop += Mass[elephant];
                    if (Mass[elephant] < minMassLoop) minMassLoop = Mass[elephant];
                }
                math1Calcs += sumMassLoop + (l.Count - 2) * minMassLoop;
                math2Calcs += sumMassLoop + minMassLoop + (l.Count + 1) * minMassGlobal;
            }
            
            return Math.Min(math1Calcs,math2Calcs);
        }

    }
}
