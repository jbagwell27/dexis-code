using System;
using System.Collections.Generic;
using System.Xml;

namespace ConsoleTestBed
{
    class Program
    {
        public List<string> DriverVersions;
        public List<string> SwVersions;
        public List<string> HwVersions;
        public string Department;
        public static readonly string SOFTWARE = "software";
        public static readonly string HARDWARE = "hardware";
        public static readonly string DRIVER = "driver";

        XmlDocument Doc;


        static int Main(string[] args)
        {
            new XmlLargeEquip3d();
            Console.Read();
            return 0;
        }
       
    }
}

