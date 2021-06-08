using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Planet_Creator.Classes;
using Planet_Creator.classes;
using ClosedXML.Excel;

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
            for (int i = 3; i <= 51; i++)
            {
                if (!ws1.Row(i).IsEmpty())
                {
                    Planet planet = new Planet()
                    {
                        Name = (string)ws1.Cell(i, 1).Value
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
                    i += h - 1;
                }
            }

            StringBuilder AllEffect = new StringBuilder();
            System_Creator system_Creator = new System_Creator();

            AllEffect.AppendLine($"#All {ws1.Name} Planet Effects");
            foreach (Planet planet in Planets)
            {
                AllEffect.AppendLine(system_Creator.CreatePlanet(planet.Name, planet.Species, planet.Districts, planet.Buildings, planet.Features, planet.Modifiers).ToString());
            }

            string path = $@"{ws1.Name}_planet_effects.txt";

            // Create a file to write to.
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.Write(AllEffect);
            }
        }
    }
}
