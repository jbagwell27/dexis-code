using System.Collections.Generic;
using System.Drawing.Text;
using System.IO;

namespace TroubleshootProject
{
    class IniReader
    {
        public string[] FileLines;
        public List<string> Font;
        public List<string> WindowsVersions;
        public List<string> DEXISVersions;
        public List<string> PmsVersions;
        public List<string> Devices;
        public List<string> PdsVersions;
        public int FontSize { get; set; }
        public string FontFamily { get; set; }


        public IniReader()
        {
            Font = new List<string>();
            WindowsVersions = new List<string>();
            DEXISVersions = new List<string>();
            PmsVersions = new List<string>();
            Devices = new List<string>();
            PdsVersions = new List<string>();
            ReadFile();
            ReadFont();
            ReadFontSize();
            ReadFontFamily();
            ReadWindowsVersions();
            ReadDEXISVersions();
            ReadPMSVersions();
            ReadDevices();
            ReadTitaniumVersions();

        }

        private void ReadFontSize()
        {
            string size = "";
            foreach (var item in Font)
            {
                if (item.StartsWith("Size"))
                {
                    size = item.Replace("Size=", "");
                }
            }
            FontSize = int.Parse(size);
        }
        private void ReadFontFamily()
        {
            string family = "";
            foreach (var item in Font)
            {
                if (item.StartsWith("Family"))
                {
                    family = item.Replace("Family=", "");
                }
            }
            var fontsCollection = new InstalledFontCollection();
            List<string> list = new List<string>();
            foreach (var fontFamily in fontsCollection.Families)
            {
                list.Add(fontFamily.Name);
            }
            if (list.Contains(family))
            {
                FontFamily = family;
            }
            else
            {
                FontFamily = "Consolas";
            }
        }

        private void ReadFile()
        {
            if (!File.Exists("settings.ini"))
            {
                new IniGenerator();
            }

            FileLines = File.ReadAllLines("settings.ini");

        }
        private void ReadFont()
        {
            int startIndex = 0;
            int endIndex = 0;
            for (int i = 0; i < FileLines.Length; i++)
            {
                if (FileLines[i] == "[Font]")
                {
                    startIndex = i + 1;
                }
                if (FileLines[i] == "[WindowsVersions]")
                {
                    endIndex = i - 1;
                }
            }
            for (int i = startIndex; i <= endIndex; i++)
            {
                Font.Add(FileLines[i]);
            }
        }
        private void ReadWindowsVersions()
        {
            int startIndex = 0;
            int endIndex = 0;
            for (int i = 0; i < FileLines.Length; i++)
            {
                if (FileLines[i] == "[WindowsVersions]")
                {
                    startIndex = i + 1;
                }
                if (FileLines[i] == "[DEXISVersions]")
                {
                    endIndex = i - 1;
                }
            }
            for (int i = startIndex; i <= endIndex; i++)
            {
                WindowsVersions.Add(FileLines[i]);
            }
        }
        private void ReadDEXISVersions()
        {
            int startIndex = 0;
            int endIndex = 0;
            for (int i = 0; i < FileLines.Length; i++)
            {
                if (FileLines[i] == "[DEXISVersions]")
                {
                    startIndex = i + 1;
                }
                if (FileLines[i] == "[PMSVersions]")
                {
                    endIndex = i - 1;
                }
            }
            for (int i = startIndex; i <= endIndex; i++)
            {
                DEXISVersions.Add(FileLines[i]);
            }
        }

        private void ReadPMSVersions()
        {
            int startIndex = 0;
            int endIndex = 0;
            for (int i = 0; i < FileLines.Length; i++)
            {
                if (FileLines[i] == "[PMSVersions]")
                {
                    startIndex = i + 1;
                }
                if (FileLines[i] == "[Devices]")
                {
                    endIndex = i - 1;
                }
            }
            for (int i = startIndex; i <= endIndex; i++)
            {
                PmsVersions.Add(FileLines[i]);
            }
        }

        private void ReadDevices()
        {
            int startIndex = 0;
            int endIndex = 0;
            for (int i = 0; i < FileLines.Length; i++)
            {
                if (FileLines[i] == "[Devices]")
                {
                    startIndex = i + 1;
                }
                if (FileLines[i] == "[TitaniumVersions]")
                {
                    endIndex = i - 1;
                }
            }
            for (int i = startIndex; i <= endIndex; i++)
            {
                Devices.Add(FileLines[i]);
            }
        }

        private void ReadTitaniumVersions()
        {
            int startIndex = 0;
            int endIndex = 0;
            for (int i = 0; i < FileLines.Length; i++)
            {
                if (FileLines[i] == "[TitaniumVersions]")
                {
                    startIndex = i + 1;
                }
                if (FileLines[i] == "#ENDFILE")
                {
                    endIndex = i - 1;
                }
            }
            for (int i = startIndex; i <= endIndex; i++)
            {
                PdsVersions.Add(FileLines[i]);
            }
        }

    }

    class IniGenerator
    {

        private string AllText;

        public IniGenerator()
        {
            AllText = GetFont() + "\r\n" +
                GetWindowsVersions() + "\r\n" +
                GetSwVersions() + "\r\n" +
                GetPMSVersions() + "\r\n" +
                GetDevices() + "\r\n" +
                GetTitaniumVersions() + "\r\n" +
                "#ENDFILE";
            File.WriteAllText("settings.ini", AllText);
        }

        private string GetFont()
        {
            return "[Font]\r\n" + Properties.Resources.Font;
        }
        private string GetWindowsVersions()
        {
            return "[WindowsVersions]\r\n" + Properties.Resources.WindowsVersions;
        }
        private string GetSwVersions()
        {
            return "[DEXISVersions]\r\n" + Properties.Resources.SwVersions;
        }
        private string GetPMSVersions()
        {
            return "[PMSVersions]\r\n" + Properties.Resources.PMSVersions;
        }
        private string GetDevices()
        {
            return "[Devices]\r\n" + Properties.Resources.Devices;
        }
        private string GetTitaniumVersions()
        {
            return "[TitaniumVersions]\r\n" + Properties.Resources.TitaniumVersions;
        }


    }
}
