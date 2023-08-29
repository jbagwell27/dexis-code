using System;
using System.IO;

namespace SetPathInline
{
    class Program
    {

        static void Main(string[] args)
        {
            //Console.WriteLine("What is the UNC path to the Data folder? \nDO NOT add a '\\' at the end. \n ex: '\\\\server\\dexis\\data' \n");
            //string dataPath = Console.ReadLine();
            string[] dataPath = args;

            string iniFile = @"C:\DEXIS\dexis.ini";
            string[] iniLines = File.ReadAllLines(iniFile);

            for (int i = 0; i < iniLines.Length; i++)
            {
                if (iniLines[i].Contains("xdata"))
                {
                    iniLines[i] = "xdata=" + dataPath[0];
                }
            }
            File.WriteAllLines(iniFile, iniLines);
        }
    }
}