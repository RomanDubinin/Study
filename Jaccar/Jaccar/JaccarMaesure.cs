using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Jaccar
{
    public class NGram : IEqualityComparer<NGram>
    {
        public string[] words { get; private set; }
        public int count { get; private set; }

        public NGram(){}

        public NGram(string[] w, int n)
        {
            words = new string[n];
            w.CopyTo(words, 0);
            count = n;
        }

        public bool Equals(NGram first, NGram second)
        {
            return first == second;
        }

        public int GetHashCode(NGram obj)
        {
            return 0;
        }
    
        public static bool operator==(NGram first, NGram second)
        {
            if (first.count != second.count)
                return false;
            for (int i = 0; i < first.count; i++)
            {
                if (first.words[i] != second.words[i])
                    return false;
            }
            return true;
        }

        public static bool operator !=(NGram first, NGram second)
        { 
            return !(first == second);
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

        public NGram[] NGrammsFromSentence(string sentence, int n)
        {
            var words = Regex.Split(sentence, @"\W+")
                .Where(word => word != "")
                .ToArray();
            int sentenceSize = words.Length;
            NGram[] nGramms = new NGram[Math.Max(0, sentenceSize - n + 1)];
            for (int i = 0; i < sentenceSize - n + 1; i++)
            {
                nGramms[i] = new NGram (words
                    .Skip(i)
                    .Take(n)
                    .Select(word => word.ToString().ToLower())
                    .ToArray(), n);
            }
            return nGramms;
        }

        public NGram[] NGrammsFromText(string text, int n)
        {
            List<NGram> AllNGramms = new List<NGram>();

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
