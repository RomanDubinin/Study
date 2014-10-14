using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jaccar;

namespace JaccarTest
{
    [TestClass]
    public class UnitTest1
    {
        JaccarMeasure j = new JaccarMeasure();

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

        [TestMethod]
        public void NGrammsFromText1()
        {
            string text = "11,12, 13; 14 15: 16. 21' 22 - 23 24# 25 26[31 32 33? 41 42 43, 44] 51";
            int n = 3;
            string[][] actual = j.NGrammsFromText(text, n);
            string[][] expected = new string[][]
            {
                new string[]{"11", "12", "13"},
                new string[]{"12", "13", "14"},
                new string[]{"13", "14", "15"},
                new string[]{"14", "15", "16"},
                new string[]{"21", "22", "23"},
                new string[]{"22", "23", "24"},
                new string[]{"23", "24", "25"},
                new string[]{"24", "25", "26"},
                new string[]{"31", "32", "33"},
                new string[]{"41", "42", "43"},
                new string[]{"42", "43", "44"}
            };

            for (int i = 0; i < expected.Length; i++)
            {
                for (int k = 0; k < expected[i].Length; k++)
                {
                    Assert.AreEqual(expected[i][k], actual[i][k]);
                }
            }
        }


        [TestMethod]
        public void NGrammsFromText2()
        {
            string text = "A11,12, 13; 14 15: 16. 21' 22 - 23 24# 25 26[31 32 33? 41 42 43, 44] 51!12, 13; 14 15";
            int n = 4;
            string[][] actual = j.NGrammsFromText(text, n);
            string[][] expected = new string[][]
            {
                new string[]{"a11", "12", "13", "14"},
                new string[]{"12", "13", "14", "15"},
                new string[]{"13", "14", "15", "16"},
                new string[]{"21", "22", "23", "24"},
                new string[]{"22", "23", "24", "25"},
                new string[]{"23", "24", "25", "26"},
                new string[]{"41", "42", "43", "44"},
                new string[]{"12", "13", "14", "15"}
            };

            for (int i = 0; i < expected.Length; i++)
            {
                for (int k = 0; k < expected[i].Length; k++)
                {
                    Assert.AreEqual(expected[i][k], actual[i][k]);
                }
            }
        }
    }
}
