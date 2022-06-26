using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;

namespace EventListDLL
{
    internal class Program
    {
        private static List<string> typeList = new List<string>();
        static void Main(string[] args)
        {
            string path;
            path = "E:\\SteamLibrary\\steamapps\\common\\Blade & Sorcery\\BladeAndSorcery_Data\\Managed\\ThunderRoad.dll";
            PrintTypes(path);
            typeList.Sort();
            /*foreach(string type in typeList)
            {
                Console.WriteLine(type);
            }
            Console.ReadLine();*/
            // Create file
            string title = "EventsThunderRoad";
            string pathToFolder = Environment.CurrentDirectory + "\\" + "ThunderRoadEvents";
            string pathToFile = pathToFolder + "\\" + title + ".txt";

            if (!Directory.Exists(pathToFolder))
            {
                DirectoryInfo di = Directory.CreateDirectory(pathToFolder);
            }
            // Open writer
            StreamWriter sw = File.CreateText(pathToFile);
            foreach (string str in typeList)
            {
                // Write in the file
                sw.WriteLine(str);
            }
            sw.Flush();
            sw.Close();
        }

        private static void PrintTypes(string fileName)
        {
            ModuleDefinition module = ModuleDefinition.ReadModule(fileName);
            
            foreach (TypeDefinition type in module.Types)
            {
                if (!type.IsPublic || !type.HasEvents || !type.FullName.Contains("ThunderRoad"))
                    continue;
                foreach(EventDefinition eventDefinition in type.Events)
                    typeList.Add($"{type.FullName} {eventDefinition.Name}");
            }
        }

    }
}
