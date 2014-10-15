using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Jaccar
{
    class Program
    {
        static void Main(string[] args)
        {
            JaccarMeasure j = new JaccarMeasure();

            string text1 = String.Concat(File.ReadAllLines("Ulitka_na_sklone.txt"));
            string text2 = String.Concat(File.ReadAllLines("Bespokoistvo.txt"));
            var text1NGramms = j.NGrammsFromText(text1, 3);
            var text2NGramms = j.NGrammsFromText(text2, 3);
            var allNGrams = text1NGramms.Concat(text2NGramms)
                .GroupBy(g => g, new Comparer())
                .Select(group => group.Key)
                .ToArray();

            var text1NGrammsDict = text1NGramms
                .GroupBy(nG => nG, new Comparer())
                .ToDictionary(group => group.Key, group => group.Count());
            var text2NGrammsDict = text2NGramms
                .GroupBy(nG => nG, new Comparer())
                .ToDictionary(group => group.Key, group => group.Count());

            int common = 0;
            int total = 0;

            Console.WriteLine("dicts");
            foreach (var nG in allNGrams)
            {
                if (!text1NGramms.Contains(nG, new Comparer()))
                    text1NGrammsDict.Add(nG, 0);
                if (!text2NGramms.Contains(nG, new Comparer()))
                    text2NGrammsDict.Add(nG, 0);

                common += Math.Min(text1NGrammsDict[nG], text2NGrammsDict[nG]);
                total += Math.Max(text1NGrammsDict[nG], text2NGrammsDict[nG]);
            }

            Console.WriteLine(text1NGramms.Count());
            Console.WriteLine(text2NGramms.Count());
            Console.WriteLine(common / total);
        }
    }
}
