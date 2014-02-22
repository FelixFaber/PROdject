using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ClassGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.AssemblyLoad += new AssemblyLoadEventHandler(MyAssemblyLoadEventHandler);

            string folder = @"H:\c#\ClassGenerator\test\";

            string sourcePath = Path.Combine(folder, "testClass.cs");
            string targetPath = Path.Combine(folder, "test.dll");

            if (File.Exists(targetPath))
                File.Delete(targetPath);

            File.WriteAllText(sourcePath, GimmeAClass());
            Process.Start(new ProcessStartInfo("cmd.exe", @"/c C:\Windows\Microsoft.NET\Framework\v3.5\csc.exe /t:library /r:System.dll /out:" + targetPath + " " + sourcePath));

            Assembly testDLL = Assembly.LoadFrom(targetPath);
            var myType = testDLL.GetType("OurLittlePOQ.Tester");

            ObjectCreateMethod inv = new ObjectCreateMethod(myType);
            var obj = inv.CreateInstance();

            var myInterface = myType.GetInterfaces().First();

            var methodName = myInterface.GetMembers().First().Name;
            var result = myInterface.InvokeMember(methodName, BindingFlags.InvokeMethod, null, obj, null);

        }

        static private string GimmeAClass()
        {
            StringBuilder classContents = new StringBuilder();

            classContents.AppendLine("using System;");
            classContents.AppendLine("namespace OurLittlePOQ{");
            classContents.AppendLine("public interface IHelloWorld{string HelloWorld();}");
            classContents.AppendLine("public class Tester : IHelloWorld { public string HelloWorld(){ return \"Hello World\"; }}}");

            return classContents.ToString();
        }

        static void MyAssemblyLoadEventHandler(object sender, AssemblyLoadEventArgs args)
        {
            Console.WriteLine("ASSEMBLY LOADED: " + args.LoadedAssembly.FullName);
            Console.WriteLine();
        }

        static void PrintLoadedAssemblies(AppDomain domain)
        {
            Console.WriteLine("LOADED ASSEMBLIES:");
            foreach (Assembly a in domain.GetAssemblies())
            {
                Console.WriteLine(a.FullName);
            }
            Console.WriteLine();
        }
    }
}
