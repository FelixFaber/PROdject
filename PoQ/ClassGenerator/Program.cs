using ClassGenerator.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ClassGenerator
{
    class Program
    {
        static Random rand = new Random();

        static public string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        static void Main(string[] args)
        {
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.AssemblyLoad += new AssemblyLoadEventHandler(MyAssemblyLoadEventHandler);

            string folder = AssemblyDirectory;

            string sourcePath = Path.Combine(folder, "testClass.cs");
            string targetPath = Path.Combine(folder, "test.dll");

            if (File.Exists(targetPath))
                File.Delete(targetPath);

            File.WriteAllText(sourcePath, GimmeSnakeModule());
            Process.Start(new ProcessStartInfo("cmd.exe", @"/c C:\Windows\Microsoft.NET\Framework\v3.5\csc.exe /t:library /r:System.dll,ClassGenerator.Interfaces.dll /out:" + targetPath + " " + sourcePath));

            Assembly testDLL = Assembly.LoadFrom(targetPath);

            var moduleTypes = GetAllModules(testDLL);

            foreach (var moduleType in moduleTypes)
            {
                if (typeof(ISnakeModule).IsAssignableFrom(moduleType))
                {
                    ObjectCreateMethod inv = new ObjectCreateMethod(moduleType);
                    var plugin = (ISnakeModule)inv.CreateInstance();

                    Console.WriteLine("My snake module runs at: AppleDesire " + plugin.AppleDesireCoefficient + " SnakeSafetyZone " + plugin.SnakeSafetyZoneWidth);
                }
            }
            
            //var myType = testDLL.GetType("ClassGenerator.Plugin.TestPlugin");
            //var methodName = myInterface.GetMembers().First().Name;
            //var result = myInterface.InvokeMember(methodName, BindingFlags.InvokeMethod, null, obj, null);
        }
        static private IEnumerable<Type> GetAllModules(Assembly fromAssembly)
        {
            var results = from type in fromAssembly.GetTypes()
                          where typeof(IModule).IsAssignableFrom(type)
                          select type;
            return results;
        }

        static private string GimmeAClass()
        {
            StringBuilder classContents = new StringBuilder();

            classContents.AppendLine("using System;");
            classContents.AppendLine("using ClassGenerator.Interfaces;");
            classContents.AppendLine("namespace ClassGenerator.Plugin{");
            classContents.AppendLine("public class TestPlugin : IPlugin { public int Execute(){return 0;}}}");

            return classContents.ToString();
        }
        static private string GimmeSnakeModule()
        {
            float appleCoeff = (float) rand.NextDouble();
            int safetyWidth = rand.Next(0, 50);
            int moduleNum = rand.Next(10, 500);

            StringBuilder classContents = new StringBuilder();

            classContents.AppendLine("using System;");
            classContents.AppendLine("using ClassGenerator.Interfaces;");
            classContents.AppendLine("namespace ClassGenerator.Plugin{");
            classContents.AppendLine("public class TestPlugin"+moduleNum+" : ISnakeModule {");
            classContents.AppendLine("public float AppleDesireCoefficient {get{ return " + appleCoeff.ToString("F", new CultureInfo("en-US")) + "f;}} ");
            classContents.AppendLine("public int SnakeSafetyZoneWidth {get{ return "+safetyWidth+";}} ");
            classContents.AppendLine("}}");

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
