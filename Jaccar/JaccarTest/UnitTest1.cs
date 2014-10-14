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
        public void Sentences1()
        {
            var actual = j.ToSentences("qwe. 123[74] 14,88");
            var expected = new string[] { "qwe", "123", "74", "14,88" };
            for(int i = 0; i < actual.Length; i++)
                Assert.AreEqual(expected[i], actual[i]);
        }

        [TestMethod]
        public void NGramms1()
        {
            string text = "11 12 13, 14: 15 16; 17 18 19";
            int n = 3;
            string[][] actual = j.NGrammsFromSentence(text, n);
            
            var expected = new string[][] 
            {
                new string[] {"11", "12", "13"},
                new string[] {"12", "13", "14"},
                new string[] {"13", "14", "15"},
                new string[] {"14", "15", "16"},
                new string[] {"15", "16", "17"},
                new string[] {"16", "17", "18"},
                new string[] {"17", "18", "19"}
            };

            for(int i = 0; i < expected.Length; i++)
            {
                for(int k = 0; k < expected[i].Length; k++)
                {
                    Assert.AreEqual(expected[i][k], actual[i][k]);
                }
            }
        }

        [TestMethod]
        public void NGramms2()
        { 
            string text = "11 12 13 14 15 16 17 18 19";
            int n = 30;
            string[][] actual = j.NGrammsFromSentence(text, n);

            Assert.AreEqual(0, actual.Length);
        }
    }
}
