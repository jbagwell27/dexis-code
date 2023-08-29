using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ConsoleTestBed
{
    class XmlLargeEquip3d
    {
        private readonly XmlDocument Doc;

        public List<string> HardwareList;
        public List<string> SoftwareTypes;

        public XmlLargeEquip3d()
        {
            HardwareList = new List<string>();
            SoftwareTypes = new List<string>();

            Doc = new XmlDocument();
            //XmlWriter.WriteXmlFile();
            try
            {
                Doc.Load("Departments.xml");
            }
            catch (System.Exception)
            {
                Doc.Load("Departments.xml");
            }
            ParseHardware();
            ParseSoftwareTypes();
        }

        public void ParseHardware()
        {
            foreach (XmlNode node in Doc.DocumentElement)
            {
                if (node.Name == "largeequipment3d")
                {
                    foreach (XmlNode child in node.ChildNodes)
                    {
                        if (child.Name == "hardware")
                        {
                            foreach (XmlNode grandChild in child.ChildNodes)
                            {
                                HardwareList.Add(grandChild.Attributes[0].Value);
                            }
                        }
                    }
                }
            }
            HardwareList.Add(string.Empty);
        }

        public List<string> ParseFirmware(string hardware)
        {
            List<string> firmwareList = new List<string>();
            foreach (XmlNode node in Doc.DocumentElement)
            {
                if (node.Name == "largeequipment3d")
                {
                    foreach (XmlNode child in node.ChildNodes)
                    {
                        if (child.Name == "hardware")
                        {
                            foreach (XmlNode grandChild in child.ChildNodes)
                            {
                                XmlAttribute attr = grandChild.Attributes[0];
                                if (attr.Value == hardware)
                                {
                                    foreach (XmlNode gGrandChild in grandChild.ChildNodes)
                                    {
                                        if (gGrandChild.Name == "firmware")
                                        {
                                            firmwareList.AddRange(gGrandChild.InnerText.Split(';'));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return firmwareList;
        }

        public void ParseSoftwareTypes()
        {
            foreach (XmlNode node in Doc.DocumentElement)
            {
                if (node.Name == "largeequipment3d")
                {
                    foreach (XmlNode child in node.ChildNodes)
                    {
                        if (child.Name == "software")
                        {
                            foreach (XmlNode grandChild in child.ChildNodes)
                            {
                                if (grandChild.Name == "type")
                                {
                                    SoftwareTypes.Add(grandChild.Attributes[0].Value);
                                }
                            }
                        }
                    }
                }
            }
        }

        public List<string> ParseSoftwareNames(string programType)
        {
            List<string> swNames = new List<string>();

            foreach (XmlNode node in Doc.DocumentElement)
            {
                if (node.Name == "largeequipment3d")
                {

                    foreach (XmlNode child in node.ChildNodes)
                    {
                        if (child.Name == "software")
                        {
                            foreach (XmlNode grandChild in child.ChildNodes)
                            {
                                if (grandChild.Name == "type")
                                {
                                    XmlAttribute attr = grandChild.Attributes[0];
                                    if (attr.Value == programType)
                                    {

                                        foreach (XmlNode gGrandChild in grandChild.ChildNodes)
                                        {
                                            XmlAttribute xAttr = gGrandChild.Attributes[0];
                                            swNames.Add(xAttr.Value);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return swNames;
        }

        public List<string> ParseSoftwareVersion(string programType, string programName)
        {
            List<string> swVersions = new List<string>();

            foreach (XmlNode node in Doc.DocumentElement)
            {
                if (node.Name == "largeequipment3d")
                {

                    foreach (XmlNode child in node.ChildNodes)
                    {
                        if (child.Name == "software")
                        {
                            foreach (XmlNode grandChild in child.ChildNodes)
                            {
                                if (grandChild.Name == "type")
                                {
                                    XmlAttribute attr = grandChild.Attributes[0];
                                    if (attr.Value == programType)
                                    {
                                        foreach (XmlNode gGrandChild in grandChild.ChildNodes)
                                        {
                                            XmlAttribute xAttr = gGrandChild.Attributes[0];
                                            if (xAttr.Value == programName)
                                            {
                                                foreach (XmlNode ggGrandChild in gGrandChild.ChildNodes)
                                                {
                                                    if (ggGrandChild.Name == "version")
                                                    {
                                                        swVersions.AddRange(ggGrandChild.InnerText.Split(';'));
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return swVersions;
        }
    }
}
