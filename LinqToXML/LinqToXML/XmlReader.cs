using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace LinqToXML
{
    internal class XmlReader
    {
        public List<string> AnalizeXmlReturnClassesNoProp(string filePath)
        {
            List<string> classesWithNoProperties = new List<string>();
            if (File.Exists(filePath))
            {
                XElement xmlFile = XElement.Load(filePath);
                var classes =
                    xmlFile.Elements()
                        .Where(p => !p.ElementsAfterSelf("property").Any())
                        .OrderBy(p => p.Attribute("Full_name").ToString());
                foreach (var @class in classes)
                {
                    classesWithNoProperties.Add(@class.Attribute("Full_name").ToString());
                }
                return classesWithNoProperties;
            }
            throw new FileNotFoundException();
        }

        public int MethodCount(string filePath)
        {
            if (File.Exists(filePath))
            {
                XElement xmlFile = XElement.Load(filePath);
                var allMethods = xmlFile.Descendants("Method");
                return allMethods.Count();
            }
            throw new FileNotFoundException();
        }

        public int PropertiesCount(string filePath)
        {
            if (File.Exists(filePath))
            {
                XElement xmlFile = XElement.Load(filePath);
                var allProperties = xmlFile.Descendants("property");
                return allProperties.Count();

            }
            throw new FileNotFoundException();
        }

        public string MostCommonParamater(string filePath)
        {
            if (File.Exists(filePath))
            {
                XElement xmlFile = XElement.Load(filePath);
                var allParameters = xmlFile.Descendants("Paramatr");
               
                var paramsByType = allParameters.GroupBy(param => param.Attribute("Type")).OrderByDescending(p=>p.Count());
                return paramsByType.FirstOrDefault().Key.ToString();
            }
            throw new FileNotFoundException();
        }

        public void XmlSummery(string filePath)
        {
            if (File.Exists(filePath))
            {
                XElement xmlFile = XElement.Load(filePath);
                List<XElement> xmlItems = new List<XElement>();
                var elemnts = xmlFile.Elements().OrderBy(p =>
                {
                    var xElement = p.Element("method");
                    return xElement != null ? xElement.Value.Count() : 0;
                });
                foreach (var xElement in elemnts)
                {
                    int propCount = xElement.Descendants("property").Count();
                    int methodCount = xElement.Descendants("Method").Count();
                     xmlItems.Add(new XElement("Type",
                        new XAttribute("Name",xElement.Attribute("Full_name").Value),
                        new XElement("Properties_count",propCount),
                        new XElement("Method_count",methodCount))); 
                }
                XElement XmlSummery = new XElement("XML_Summery",xmlItems);
                XmlSummery.Save("Xml_Summery.xml");
            }
            else
            {
                throw new FileNotFoundException();
            }
        }

        public void SortXml(string filePath)
        {
            if (File.Exists(filePath))
            {
                XElement xmlFile = XElement.Load(filePath);
                var xmlSort = xmlFile.Elements("Type").GroupBy(p => p.Descendants("Method").Count()).OrderByDescending(p => p.Key);
                foreach (var key in xmlSort)
                {
                    key.Elements().OrderBy(p => p.Attribute("Full_name"));
                }
                XElement xmlSorted = new XElement("Key");
                foreach (var item in xmlSort)
                {
                    List<XElement> typeList = new List<XElement>();
                    foreach (var xElement in item)
                    {
                        typeList.Add(xElement);
                    }
                    xmlSorted.Add(new XElement("key",item.Key),new XElement("Types",typeList));
                }
                xmlSorted.Save("SortedXML.xml");
            }
            else
            {
                throw new FileNotFoundException();
            }
        }

    }

}

