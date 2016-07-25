using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LinqToXML
{
    class XmlWriter
    {

        public void SaveXml(AssemblyName refrence)
        {
            List<XElement> classesList = new List<XElement>();
            List<XElement> propertiesList = new List<XElement>();
            List<XElement> MethodsList = new List<XElement>();
            List<XElement> ParamsList = new List<XElement>();
            int counter = 0;
            if (refrence != null)
            {
                Assembly mscorlibAss = Assembly.Load(refrence);
                var classes = mscorlibAss.GetTypes().Where(p => p.IsClass).Where(p => p.IsPublic).Where(p => !p.IsInterface).Where(p =>!p.IsValueType);
                foreach (var @class in classes)
                {
                    propertiesList.AddRange(@class.GetProperties().Select(prop => new XElement("property", new XAttribute("Name", prop.Name), new XAttribute("Type", prop.PropertyType.Name))).Select(tempProp => new XElement(tempProp)));
                    
                    foreach (var method in @class.GetMethods())
                    {
                        ParamsList.AddRange(method.GetParameters().Select(pramater => new XElement("Paramatr", new XAttribute("Name", pramater.Name), new XAttribute("Type", pramater.ParameterType.Name))).Select(tempParam => new XElement(tempParam)));
                        var tempMethod = new XElement("Method",
                           new XAttribute("Name", method.Name),
                           new XAttribute("Return_type", method.ReturnType.Name),
                           new XElement("Paramaters", ParamsList));
                        MethodsList.Add(new XElement(tempMethod));
                    }
                    var tempClass = new XElement("Type",
                                                   new XAttribute("Full_name", @class.FullName),
                                                   new XElement("Properties", propertiesList),
                                                   new XElement("Methods", MethodsList));
                    classesList.Add(tempClass);
                    
                    //Memory crashes- loop for getting only 20 items
                    counter++;
                    if (counter > 20)
                    {
                        break;
                    }

                }
                XElement xmlFile = new XElement("type", classesList);
                xmlFile.Save("mscorlibXML.xml");

            }
        }
           
    }
}
