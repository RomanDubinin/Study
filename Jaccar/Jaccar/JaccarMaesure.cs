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



    }
}
