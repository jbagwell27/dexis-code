using System.Collections.Generic;

namespace TroubleshootProject
{
    class NoteTemplate
    {
        public readonly int FontSize;
        public readonly string FontFamily;
        public string ContactName { get; set; }
        public string PhoneNumber { get; set; }
        public string CompName { get; set; }
        public string WindowsVersion { get; set; } //XP, 7, 10, server, etc.
        public string CompArch { get; set; } //x86. x64
        public string WindowsType { get; set; } //Server, home, pro
        public bool IsHardware { get; set; }
        public bool IsRemote { get; set; }
        public bool IsMultipleMachines { get; set; }
        public string FirstTimeIssue { get; set; }
        public string IssueVerified { get; set; }
        public string WindowsUpdated { get; set; }
        public string DEXISVersion { get; set; } //9.5.0, 10.1.6, etc.
        public string PmsVersion { get; set; } //Dentrix, Eaglesoft, etc.
        public string Device { get; set; } //Platinum, Titanium, DEXcam, CariVu
        public string HwSerialNumber { get; set; }
        public string PdsVersion { get; set; } //1.0.7, 1.0.5, etc.
        public string DEXcamVersion { get; set; } //3, 4hd, etc
        public string DatabasePath { get; set; }
        public string Problem { get; set; } //Problem Description
        public string TroubleshootingSteps { get; set; } //Troubleshooting steps
        public string Resolution { get; set; } //Resolution Details
        public List<string> Font;
        public List<string> WindowsVersions;
        public List<string> DEXISVersions;
        public List<string> PmsVersions;
        public List<string> Devices;
        public List<string> PdsVersions;

        public List<string> Computers;
        public List<string> CaseAssets;
        //public List<string> Devices;
        //public List<string> HwVersions;

        public IniReader IniRead;

        public NoteTemplate()
        {
            IniRead = new IniReader();

            Font = IniRead.Font;
            WindowsVersions = IniRead.WindowsVersions;
            DEXISVersions = IniRead.DEXISVersions;
            PmsVersions = IniRead.PmsVersions;
            Devices = IniRead.Devices;
            PdsVersions = IniRead.PdsVersions;
            FontSize = IniRead.FontSize;
            FontFamily = IniRead.FontFamily;

            Computers = new List<string>();
            CaseAssets = new List<string>();
        }

        public string OS_StringBuilder(string name, string os, string osversion, string ostype, string arch)
        {
            return name + "\t\t" + os + " " + osversion + " " + ostype + " " + arch;
        }

        public string CaseAsset_StringBuilder(string device, string swVersion, string serialNumber)
        {
            string result = device;
            if (swVersion != "")
            {
                result += "\t" + swVersion;
            }
            if (serialNumber != "")
            {
                result += "\t" + serialNumber;
            }
            return result;
        }
        public void AddHw(string hw)
        {
            CaseAssets.Add(hw);
        }

        public void AddComp(string comp)
        {
            Computers.Add(comp);
        }

        public override string ToString()
        {
            string result = "";
            if (ContactName != "")
            {
                result += "Contact Name:  " + ContactName + "\r\n";
            }
            if (PhoneNumber != "")
            {
                result += "Phone Number:  " + PhoneNumber + "\r\n";
            }

            result += "First Time Issue?:  " + FirstTimeIssue + "\r\n";
            result += "Issue Verified?:  " + IssueVerified + "\r\n";

            if (IsMultipleMachines)
            {
                result += "Multiple Machines Affected\r\n";
            }
            if (WindowsUpdated == "")
            {
                result += "Windows Updated?:  " + WindowsUpdated + "\r\n";
            }



            result += "\r\nIssue:  \r\n" + Problem + "\r\n\r\n" +
                "Troubleshooting Steps:  \r\n" + TroubleshootingSteps + "\r\n\r\n" +
                "Resolution:  \r\n" + Resolution + "\r\n\r\n" +
                "---------------- :ENVIRONMENT INFO: ----------------\r\n\r\n";

            result += "PMS:  " + PmsVersion + "\r\n" +
                "DEXIS Version:  " + DEXISVersion + "\r\n";
            if (DatabasePath != "")
            {
                result += "Database Path:  " + DatabasePath + "\r\n";
            }

            //Case Assets
            if (IsHardware)
            {
                result += "\r\nCase Assets:  \r\n";
                foreach (var item in CaseAssets)
                {
                    result += item + "\r\n";
                }
            }

            //computers
            if (IsRemote)
            {
                result += "\r\nComputers: \r\n";

                foreach (string s in Computers)
                {
                    result += s + "\r\n";
                }
            }

            return result;
        }
    }
}
