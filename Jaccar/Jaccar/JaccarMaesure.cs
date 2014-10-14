using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.RegularExpressions;
namespace Jaccar
{
    public class JaccarMaesure
    {
        public string[] ToSentences(string text)
        {
            return Regex.Split(text, "[\\[\\].!?(){}]")
                .Select(sentense => sentense.Trim())
                .ToArray();
        }

        public string[][] NGrammsFromSentence(string sentence, int n)
        {
            int size = sentence.Split(' ').Length;
            string[][] nGramms = new string[Math.Max(0, size - n + 1)][];
            for (int i = 0; i < size - n + 1; i++)
            {
                nGramms[i] = Regex.Split(sentence, @"\W+")
                    .Skip(i)
                    .Take(n)
                    .Select(word => word.ToString())
                    .ToArray();
            }
            return nGramms;
        }

    }
}
