using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Planet_Creator.Classes;
using Planet_Creator.classes;
using ClosedXML.Excel;
using System.Text.RegularExpressions;

namespace Planet_Creator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter path to planetary data!");
            string datapath = Console.ReadLine(); 
            string fileName = datapath;
            var workbook = new XLWorkbook(fileName);

            foreach(IXLWorksheet Worksheet in workbook.Worksheets)
            {

                DoWorksheet(Worksheet);
            }

            

        }

        static void DoWorksheet(IXLWorksheet ws1)
        {
            List<Planet> Planets = new List<Planet>();
            List < Classes.System > Systems= new List<Classes.System>();
            int i = 3;
            while (ws1.Cell(i,1).Value.ToString()!="end")
            {
                if (!ws1.Row(i).IsEmpty())
                {
                    Classes.System system = new Classes.System() { Name = (string)ws1.Cell(i, 3).Value };
                    if (ws1.Cell(i, 3).IsEmpty())
                    {
                        system.Name = (string)ws1.Cell(i, 1).Value;
                    }
                    Planet planet = new Planet()
                    {
                        Name = (string)ws1.Cell(i, 1).Value,
                        planetclass = (string)ws1.Cell(i, 5).Value,
                        Size = Convert.ToInt32((double)ws1.Cell(i, 7).Value)
                    };
                    int h = 1;
                    while (ws1.Cell(h + i, 1).IsEmpty())
                    {
                        h++;
                    }
                    for (int y = 0; y < h; y++)
                    {
                        if (!ws1.Cell(y + i, 9).IsEmpty())
                        {
                            planet.Species.Add((string)ws1.Cell(y + i, 9).Value, Convert.ToInt32((double)ws1.Cell(y + i, 10).Value));
                        }
                        if (!ws1.Cell(y + i, 11).IsEmpty())
                        {
                            planet.Districts.Add((string)ws1.Cell(y + i, 11).Value, Convert.ToInt32((double)ws1.Cell(y + i, 12).Value));
                        }
                        if (!ws1.Cell(y + i, 13).IsEmpty())
                        {
                            planet.Buildings.Add((string)ws1.Cell(y + i, 13).Value, Convert.ToInt32((double)ws1.Cell(y + i, 14).Value));
                        }
                        if (!ws1.Cell(y + i, 15).IsEmpty())
                        {
                            planet.Features.Add((string)ws1.Cell(y + i, 15).Value, Convert.ToInt32((double)ws1.Cell(y + i, 16).Value));
                        }
                        if (!ws1.Cell(y + i, 17).IsEmpty())
                        {
                            planet.Modifiers.Add((string)ws1.Cell(y + i, 17).Value);
                        }
                    }
                    Planets.Add(planet);
                    system.planet = planet;
                    Systems.Add(system);
                    i += h - 1;
                }
                i++;
            }

            StringBuilder AllPlanetEffect = new StringBuilder();
            System_Creator system_Creator = new System_Creator();

            AllPlanetEffect.AppendLine($"#All {ws1.Name} Planet Effects");
            foreach (Planet planet in Planets)
            {
                AllPlanetEffect.AppendLine(system_Creator.CreatePlanet(planet.Name, planet.Species, planet.Districts, planet.Buildings, planet.Features, planet.Modifiers).ToString());
            }
            Regex rgx = new Regex("[^a-z0-9-]");

            string path = $@"scripted_effects\\{rgx.Replace(ws1.Name.ToLower(), "")}_planet_effects.txt";

            // Create a file to write to.
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.Write(AllPlanetEffect);
            }

            StringBuilder AllSystemEffect = new StringBuilder();
            

            AllSystemEffect.AppendLine($"#All {ws1.Name} Planet system inits");
            foreach (Classes.System system in Systems)
            {
                AllSystemEffect.AppendLine(system_Creator.CreateSystem(system.Name,system.planet.Name,system.planet.Size,system.planet.planetclass,ws1.Name).ToString());
            }


            path = $@"solar_system_initializers\\{rgx.Replace(ws1.Name.ToLower(), "")}_inits.txt";

            // Create a file to write to.
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.Write(AllSystemEffect);
            }
        }
    }
}
