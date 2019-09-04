using Microsoft.VisualStudio.TestTools.UnitTesting;
using CountVowels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountVowels.Tests
{
    [TestClass()]
    public class ProgramTests
    {
        [TestMethod()]
        public void CountVowelsTest()
        {
            string testString = "Been fun";                 
            Assert.AreEqual(3, Program.CountVowels(testString));
            Assert.AreEqual(2, Program.e);
            Assert.AreEqual(1, Program.u);
        }

        [TestMethod()]
        public void CountVowelsTest2()
        {
            string testString = "";
            Assert.AreEqual(0, Program.CountVowels(testString));
            testString = null;
            Assert.AreEqual(0, Program.CountVowels(testString));
        }
    }
}