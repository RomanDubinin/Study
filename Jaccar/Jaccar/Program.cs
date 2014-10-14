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
            JaccarMeasure j = new JaccarMeasure();
            //foreach (string str in j.ToSentences("qwe. 123[74] 14,88"))
            //    Console.WriteLine(str);

            //var a = j.ToSentences("qwe. 123[74] 14,88");
            //Console.WriteLine((a));

            string text = "11,12, 13; 14 15 16 . 21' 22 - 23 24# 25 26[31 32 33? 41 42 43, 44] 51";
            var a = j.NGrammsFromText(text, 3).ToArray();

            Console.ReadKey();
        }
    }
}
