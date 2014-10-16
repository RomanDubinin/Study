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
                .GroupBy(g => g, new NGram())
                .Select(group => group.Key)
                .ToArray();

            int common = 0;
            int total = 0;

            foreach (var nG in allNGrams)
            {
                common += Math.Min(text1NGramms.Count(x => x == nG), text2NGramms.Count(x => x == nG));
                total += Math.Max(text1NGramms.Count(x => x == nG), text2NGramms.Count(x => x == nG));
            }

            Console.WriteLine(text1NGramms.Count());
            Console.WriteLine(text2NGramms.Count());
            Console.WriteLine(common / (double)total);
        }
    }
}
