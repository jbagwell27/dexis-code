using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CreateNewISS
{
    class Program
    {
        static void Main(string[] args)
        {
            string allText = File.ReadAllText("record.iss");
            string temp;

            if (allText.Contains(@"szDir=C:\Program Files (x86)\DEXIS"))
            {
                temp = allText.Replace(@"szDir=C:\Program Files (x86)\DEXIS", @"szDir=C:\Program Files\DEXIS");
                File.WriteAllText("record32.iss", temp);
                File.Move("record.iss", "record64.iss");
            }
            else if (allText.Contains(@"szDir=C:\Program Files\DEXIS"))
            {
                temp = allText.Replace(@"szDir=C:\Program Files\DEXIS", @"szDir=C:\Program Files (x86)\DEXIS");
                File.WriteAllText("record64.iss", temp);
                File.Move("record.iss", "record32.iss");
            }
        }
    }
}

