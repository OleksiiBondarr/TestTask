using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TestTask;
namespace TestTask.Tests
{
    [TestClass]
    public class ReplacerTests
    {
        [TestMethod]
        public void CalculationsTest()
        {
            int num = 6;
            List<int> st = new List<int> { 1, 4, 5, 3, 6, 2 };
            List<int> tg = new List<int> { 5, 3, 2, 4, 6, 1 };
            List<int> mas = new List<int> { 2400, 2000, 1200, 2400, 1600, 4000 };
            Replacer rep = new Replacer(num, st, tg, mas);
            rep.FindLoops();
            int expected = 11200;

            int actual = rep.Calculations();

            Assert.AreEqual(expected, actual);

        }
    }
}
