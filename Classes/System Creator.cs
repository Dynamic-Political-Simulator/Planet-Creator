using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Planet_Creator.classes
{
    public class System_Creator
    {
        public StringBuilder CreatePlanet(string Name, Dictionary<string,int> Speciess, Dictionary<string, int> Districts, Dictionary<string, int> Buildings, Dictionary<string, int> Features, List<string> Modifiers)
        {
            StringBuilder PlanetEffect = new StringBuilder();
            PlanetEffect.AppendLine($"create_{Name}_colony = {{");
            PlanetEffect.AppendLine($"\tprevent_anomaly = yes");
            foreach(KeyValuePair<string,int> Building in Buildings)
            {
                for(int i = 0; i < Building.Value; i++)
                {
                    PlanetEffect.AppendLine($"\tadd_building = {Building.Key}");
                }
            }
            foreach(KeyValuePair<string,int> District in Districts)
            {
                PlanetEffect.AppendLine("\twhile = {");
                PlanetEffect.AppendLine($"\t\tcount = {District.Value}");
                PlanetEffect.AppendLine("\t\tadd_district = {");
                PlanetEffect.AppendLine($"\t\t\tdistrict_type = {District.Key}");
                PlanetEffect.AppendLine($"\t\t\tignore_cap = yes");
                PlanetEffect.AppendLine("\t\t}");
                PlanetEffect.AppendLine("\t}");
            }
            foreach (KeyValuePair<string, int> Species in Speciess)
            {
                PlanetEffect.AppendLine("\twhile = {");
                PlanetEffect.AppendLine($"\t\tcount = {Species.Value}");
                PlanetEffect.AppendLine("\t\tcreate_pop = {");
                PlanetEffect.AppendLine($"\t\t\tspecies = {Species.Key}");
                PlanetEffect.AppendLine("\t\t}");
                PlanetEffect.AppendLine("\t}");
            }
            foreach (KeyValuePair<string, int> Deposit in Features)
            {
                for (int i = 0; i < Deposit.Value; i++)
                {
                    PlanetEffect.AppendLine($"\tadd_deposit = {Deposit.Key}");
                }
            }
            foreach (string Modifier in Modifiers)
            {
                
                
                PlanetEffect.AppendLine("\tadd_modifier = {");
                PlanetEffect.AppendLine($"\t\tmodifier = {Modifier}");
                PlanetEffect.AppendLine("\t}");
                
            }
            PlanetEffect.AppendLine("}");
            return PlanetEffect;
        }
    }
}
