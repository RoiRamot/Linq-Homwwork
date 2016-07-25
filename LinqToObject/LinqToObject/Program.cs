using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LinqToObject
{
    class Program
    {
        static void Main(string[] args)
        {
            //Assembly assembly = Assembly.GetExecutingAssembly();
            //AssemblyName[] referenceList = assembly.GetReferencedAssemblies();
            //var refrence = referenceList.FirstOrDefault(p => p.Name == "mscorlib");
            //if (refrence != null)
            //{
            //    Assembly mscorlibAss = Assembly.Load(refrence);
            //    var intefaces = mscorlibAss.GetTypes().Where(p => p.IsInterface).OrderByDescending(p => p.Name);
            //    var methods = mscorlibAss.GetTypes().Where(p => p.IsInterface).Select(p => p.GetMethods()).Select(p => MethodInfo.GetCurrentMethod()).OrderByDescending(p => p.Name);

            //    foreach (var inteface in intefaces)
            //    {
            //        Console.WriteLine(inteface.Name);
            //    }
            //    foreach (var method in methods)
            //    {
            //        Console.WriteLine(method.Name);
            //    }
            //}



            var processes = Process.GetProcesses().Where(p=>p.Threads.Count> 5).OrderByDescending(p=>p.Id);
            

            foreach (var process in processes)
            {
                Console.Write("Process name: {0}", process.ProcessName);
                Console.Write(" Process ID: {0}", process.Id);
                try
                {
                    Console.Write(" Start time: {0}", process.StartTime);
                }
                catch (Exception)
                {

                    Console.WriteLine("Start time was unavailable");
                }
                Console.WriteLine();
            }

            //foreach (var item in processes.GroupBy(p => p.BasePriority))
            //{
            //    Console.WriteLine(item.Key);
            //    foreach (var process in item)
            //    {
            //        Console.Write("   Process name: {0}", process.ProcessName);
            //        Console.Write(" Process ID: {0}", process.Id);
            //        try
            //        {
            //            Console.Write(" Start time: {0}", process.StartTime);
            //        }
            //        catch (Exception)
            //        {

            //            Console.WriteLine("Start time was unavailable");
            //        }
                    
            //        Console.WriteLine();
            //    }
            //}
            //Console.WriteLine(Process.GetProcesses().Select(p => p.Threads.Count).Count());
            Console.ReadLine();
        }
    }
}
