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
            foreach (string str in j.ToSentences("qwe. 123[74] 14,88"))
                Console.WriteLine(str);

            var a = j.ToSentences("qwe. 123[74] 14,88");
            Console.WriteLine((a));
        }
    }
}
