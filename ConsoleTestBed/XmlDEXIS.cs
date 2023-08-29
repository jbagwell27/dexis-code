using System;
using System.Collections.Generic;
using System.Xml;

namespace ConsoleTestBed
{
    class XmlDEXIS
    {
        public List<string> DEXISVersions;
        public List<string> IntegraVersions;
        public List<string> PolarisVersions;
        XmlDocument Doc;

        public List<string> HardwareList;

        public XmlDEXIS()
        {

            Doc = new XmlDocument();
            Doc.Load("Departments.xml");

            HardwareList = new List<string>();
            DEXISVersions = new List<string>();
            IntegraVersions = new List<string>();
            PolarisVersions = new List<string>();

            ParseSoftware();
            ParseDriver();
            ParseHardware();

            foreach (var item in HardwareList)
            {
                //Console.WriteLine(item);
            }
        }



        public void ParseSoftware()
        {
            foreach (XmlNode node in Doc.DocumentElement)
            {
                if (node.Name == "dexis")
                {
                    foreach (XmlNode child in node.ChildNodes)
                    {
                        if (child.Name == "software")
                        {
                            foreach (XmlNode grandChild in child.ChildNodes)
                            {
                                foreach (XmlAttribute attr in grandChild.Attributes)
                                {
                                    if (attr.Value == "DEXIS 9")
                                        DEXISVersions.AddRange(grandChild.InnerText.Split(';'));
             
                                    if (attr.Value == "DEXIS 10")
                                        DEXISVersions.AddRange(grandChild.InnerText.Split(';'));
                                    
                                    if (attr.Value == "Integrator")
                                        IntegraVersions.AddRange(grandChild.InnerText.Split(';'));
                                }
                            }
                        }
                    }
                }
            }
        }

        public void ParseDriver()
        {
            foreach (XmlNode node in Doc.DocumentElement)
            {
                if (node.Name == "dexis")
                {
                    foreach (XmlNode child in node.ChildNodes)
                    {
                        if (child.Name == "driver")
                        {
                            foreach (XmlNode grandChild in child.ChildNodes)
                            {
                                foreach (XmlAttribute attr in grandChild.Attributes)
                                {
                                    if (attr.Value == "Polaris")
                                        PolarisVersions.AddRange(grandChild.InnerText.Split(';'));
                                }
                            }
                        }
                    }
                }
            }
        }

        public void ParseHardware()
        {
            foreach (XmlNode node in Doc.DocumentElement)
            {
                if (node.Name == "dexis")
                {
                    foreach (XmlNode child in node.ChildNodes)
                    {
                        if (child.Name == "hardware")
                        {
                            foreach (XmlNode grandChild in child.ChildNodes)
                            {
                                XmlAttribute attr = grandChild.Attributes[0];

                                HardwareList.Add(attr.Value);
                            }
                        }
                    }
                }
            }
        }

    }

}
