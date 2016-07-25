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
    class Program
    {
        static void Main(string[] args)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            AssemblyName[] referenceList = assembly.GetReferencedAssemblies();
            var refrence = referenceList.FirstOrDefault(p => p.Name == "mscorlib");
           XmlWriter writer = new XmlWriter();
            writer.SaveXml(refrence);

            var reader = new XmlReader();
            string path = Path.GetFullPath("mscorlibXML.xml") ;
            var classes =
                reader.AnalizeXmlReturnClassesNoProp(path);
            foreach (var @class in classes)
            {
                Console.WriteLine(@class);
            }
            Console.WriteLine(classes.Count);
            Console.WriteLine(reader.MethodCount(path));
            Console.WriteLine(reader.PropertiesCount(path));
            Console.WriteLine(reader.MostCommonParamater(path));
            reader.XmlSummery(path);
            reader.SortXml(path);
            //Console.ReadLine();

        }
    }
}
