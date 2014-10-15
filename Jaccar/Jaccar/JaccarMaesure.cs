using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Jaccar
{
    public class Comparer : IEqualityComparer<string[]>
    {
        public bool Equals(string[] first, string[] second)
        {
            if (first.Length != second.Length)
                return false;
            for (int i = 0; i < first.Length; i++)
            {
                if (first[i] != second[i])
                    return false;
            }
            return true;
        }

        public int GetHashCode(string[] obj)
        {
            return 0;
        }
    }

    public class JaccarMeasure
    {
        public string[] ToSentences(string text)
        {
            return Regex.Split(text, "[\\[\\].!?(){}]")
                .Select(sentense => sentense.Trim())
                .ToArray();
        }

        public string[][] NGrammsFromSentence(string sentence, int n)
        {
            var words = Regex.Split(sentence, @"\W+").Where(word => word != "").ToArray();
            int sentenceSize = words.Length;
            string[][] nGramms = new string[Math.Max(0, sentenceSize - n + 1)][];
            for (int i = 0; i < sentenceSize - n + 1; i++)
            {
                nGramms[i] = words
                    .Skip(i)
                    .Take(n)
                    .Select(word => word.ToString().ToLower())
                    .ToArray();
            }
            return nGramms;
        }

        public string[][] NGrammsFromText(string text, int n)
        {
            List<string[]> AllNGramms = new List<string[]>();

            foreach (var sentence in ToSentences(text))
            {
                foreach (var nGramms in NGrammsFromSentence(sentence, n))
                {
                    AllNGramms.Add(nGramms);
                }
            }
            return AllNGramms.ToArray();
        }
    }
}
