using System;
using System.IO;

namespace ChangePath
{
    class Program
    {
        static void Main(string[] args)
        {
            string startPath = @"\\UNCPath";

            if (File.Exists("log"))
            {
                startPath = File.ReadAllText("log");
            }

            Console.WriteLine("What is the UNC path to the Data folder? \nDO NOT add a '\\' at the end. \n ex: '\\\\server\\dexis\\data'");
            string dataPath = Console.ReadLine();
            File.WriteAllText("log", dataPath);


            string recordFile = "SilentInstallRecord.bat";
            string silentFile = "SilentInstall.bat";

            string recordText = File.ReadAllText(recordFile);
            string silentText = File.ReadAllText(silentFile);

            recordText = recordText.Replace(startPath, dataPath);
            silentText = silentText.Replace(startPath, dataPath);


            File.WriteAllText(recordFile, recordText);
            File.WriteAllText(silentFile, silentText);

            //Console.Read();
        }
    }
}