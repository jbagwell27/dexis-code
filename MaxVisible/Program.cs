using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MaxVisible
{
    class Program
    {

        private string[] lines;

        public Program()
        {
            lines = File.ReadAllLines(@"C:\dexis\dexisxx.ini");
        }

        private void run()
        {
            removeMaxVisible();
            string[] newLines = addMaxVisible(lines);
            File.WriteAllLines(@"c:\dexis\dexis.ini", newLines);
        }
        static void Main(string[] args)
        {
            Console.WriteLine("(c) Joshua Bagwell 2019\n" +
                "This is going to add maxvisible to the dexis.ini file");
            new Program().run();
            Console.WriteLine("Done");
            //Console.Read();
        }

        private void removeMaxVisible()
        {
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Contains("maxvisible"))
                {
                    lines[i] = "";
                }
            }
        }

        private string[] addMaxVisible(string[] lines)
        {
            List<string> lineList = new List<string>(lines);
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Contains("[SOFTWARE]"))
                {
                    lineList.Insert(i + 1, "maxvisible=18");
                }
            }
            return lineList.ToArray();
        }
    }
}
