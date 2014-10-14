using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jaccar;

namespace JaccarTest
{
    [TestClass]
    public class UnitTest1
    {
        JaccarMaesure j = new JaccarMaesure();

        [TestMethod]
        public void TestMethod1()
        {
            var actual = j.ToSentences("qwe. 123[74] 14,88");
            var expected = new string[] { "qwe", "123", "74", "14,88" };
            for(int i = 0; i < actual.Length; i++)
                Assert.AreEqual(expected[i], actual[i]);
        }
    }
}
