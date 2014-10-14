using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jaccar
{
    class Program
    
    {
        static void Main(string[] args)
        {
            JaccarMaesure j = new JaccarMaesure();
            //foreach (string str in j.ToSentences("qwe. 123[74] 14,88"))
            //    Console.WriteLine(str);

            //var a = j.ToSentences("qwe. 123[74] 14,88");
            //Console.WriteLine((a));

            string text = "11 12 13 14 15 16 17 18 19";
            var a = j.NGrammsFromSentence(text, 30).ToArray();
            Console.ReadKey();
        }
    }
}
