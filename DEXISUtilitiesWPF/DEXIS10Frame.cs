//using System.Threading.Tasks;
using EntityFrameworkForDEXIS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace DEXISUtilitiesWPF
{
    class DEXIS10Frame
    {

        string DEXISDataPath = new DEXIS10Logic().GetDataPath();

        public string PatientName(string id)
        {
            string name = "";

            using (var db = new DEXISEntities())
            {
                var pat = from patient in db.Patients
                          where patient.ExternalID == id
                          select patient;

                foreach (var patient in pat)
                {
                    name += $"{patient.Person.PersonNames.First()?.FamilyName}, {patient.Person.PersonNames.First()?.GivenName}";
                }
                return name;
            }
        }

        public List<string> ImageList(string id)
        {
            List<string> imageList = new List<string>();

            using (var db = new DEXISEntities())
            {
                var pat = from patient in db.Patients
                          where patient.ExternalID == id
                          select patient;

                foreach (var patient in pat)
                {
                    //Console.WriteLine($"{patient.Person.PersonNames.First()?.FamilyName}, {patient.Person.PersonNames.First()?.GivenName}");
                    var images = patient.Studies.SelectMany(study => study.Series).SelectMany(series => series.Visuals);

                    foreach (var image in images)
                    {
                        string rootDir = Program.ReadSetting("Data", "DataPath", DEXISDataPath);
                        const string subDir = "images";



                        imageList.Add($"{Program.OnDriveImagePath(rootDir, subDir, image.VisualID)}.dex");    //Note:  Assumes .dex file.  Should really see what files exists.                 
                    }
                }
            }
            return imageList;
        }

        //public List<string> ImageByDate

    }

    class Program
    {
        //static void Main(string[] args)
        //{
        //    using (var db = new DEXISEntities())
        //    {
        //        var patients = from patient in db.Patients
        //                       select patient;

        //        foreach (var patient in patients)
        //        {
        //            Console.WriteLine($"{patient.Person.PersonNames.First()?.FamilyName}, {patient.Person.PersonNames.First()?.GivenName}");

        //            var images = patient.Studies.SelectMany(study => study.Series).SelectMany(series => series.Visuals);
        //            foreach (var image in images)
        //            {
        //                string rootDir = ReadSetting("Data", "DataPath", @"c:\dexis imaging suite\data");
        //                const string subDir = "images";

        //                Console.WriteLine($"{OnDriveImagePath(rootDir, subDir, image.VisualID)}.dex");    //Note:  Assumes .dex file.  Should really see what files exists.                 
        //            }
        //        }
        //    }
        //}

        private static void PatImages(string id)
        {
            using (var db = new DEXISEntities())
            {
                var pat = from patient in db.Patients
                          where patient.ExternalID == id
                          select patient;
                foreach (var patient in pat)
                {
                    Console.WriteLine($"{patient.Person.PersonNames.First()?.FamilyName}, {patient.Person.PersonNames.First()?.GivenName}");
                    var images = patient.Studies.SelectMany(study => study.Series).SelectMany(series => series.Visuals);

                    foreach (var image in images)
                    {
                        string rootDir = ReadSetting("Data", "DataPath", @"c:\dexis imaging suite\data");
                        const string subDir = "images";

                        Console.WriteLine($"{OnDriveImagePath(rootDir, subDir, image.VisualID)}.dex");    //Note:  Assumes .dex file.  Should really see what files exists.                 
                    }

                }
                Console.Read();

            }
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        private static extern uint GetPrivateProfileString(
            string lpAppName,
            string lpKeyName,
            string lpDefault,
            StringBuilder lpReturnedString,
            uint nSize,
            string lpFileName);

        public static string ReadSetting(string appName, string keyName, string @default)
        {
            var returnString = new StringBuilder(1024);

            var fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                "Danaher_Dental", "dexis.ini");

            GetPrivateProfileString(appName, keyName, @default, returnString, (uint)returnString.Capacity, fileName);

            return returnString.ToString();
        }

        public static string OnDriveImagePath(string rootDir, string subDir, int fileID)
        {
            var fileNameRoot = fileID.ToString("x8");
            var id = (uint)fileID;
            long grp1 = ((id & 0xF0000000) >> 24) | ((id & 0x0000F000) >> 12);
            long grp2 = ((id & 0x0F000000) >> 20) | ((id & 0x00000F00) >> 8);
            long grp3 = ((id & 0x00F00000) >> 16) | ((id & 0x000000F0) >> 4);

            var rootPlusSub = Path.Combine(rootDir, subDir);
            var grp1Plus2 = Path.Combine(grp1.ToString("x2"), grp2.ToString("x2"));
            var grp1Plus2Plus3 = Path.Combine(grp1Plus2, grp3.ToString("x2"));
            var pathName = Path.Combine(rootPlusSub, grp1Plus2Plus3);
            return Path.Combine(pathName, fileNameRoot);
        }
    }
}
