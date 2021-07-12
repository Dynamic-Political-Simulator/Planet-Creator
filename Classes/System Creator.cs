using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace Planet_Creator.classes
{
    public class System_Creator
    {
        public StringBuilder CreatePlanet(string Name, Dictionary<string,int> Speciess, Dictionary<string, int> Districts, Dictionary<string, int> Buildings, Dictionary<string, int> Features, List<string> Modifiers)
        {
            StringBuilder PlanetEffect = new StringBuilder();
            Regex rgx = new Regex("[^a-z0-9-]");
            string CutName = rgx.Replace(Name.ToLower(), "");
            PlanetEffect.AppendLine($"create_{CutName}_colony = {{");
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
                PlanetEffect.AppendLine($"\t\t\tspecies = event_target:{Species.Key.ToLower()}");
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

        public StringBuilder CreateSystem(string Name, string PlanetName, int Size, string planetclass, string ownername)
        {
            StringBuilder SystemEffect = new StringBuilder();
            Regex rgx = new Regex("[^a-z0-9-]");
            Regex whitespace = new Regex(@"\s+");
            string CutName = rgx.Replace(Name.ToLower(), "");
            string CutOnwerName = rgx.Replace(ownername.ToLower(), "");
            string CutPlanetName = rgx.Replace(PlanetName.ToLower(), "");
            SystemEffect.AppendLine($"{CutName}_init={{");
            SystemEffect.AppendLine($"\tname = \"{Name}\"");
            SystemEffect.AppendLine("\tclass = \"rl_standard_stars\"");
            SystemEffect.AppendLine("\tplanet = {");
            SystemEffect.AppendLine("\t\tcount = 1");
            SystemEffect.AppendLine("\t\tclass = star");
            SystemEffect.AppendLine("\t\torbit_distance = 0");
            SystemEffect.AppendLine("\t\torbit_angle = 1");
            SystemEffect.AppendLine("\t\tsize = { min = 20 max = 30 }");
            SystemEffect.AppendLine("\thas_ring = no");
            SystemEffect.AppendLine("\t}");
            SystemEffect.AppendLine("\tchange_orbit = 45");
            SystemEffect.AppendLine("\tplanet = {");
            SystemEffect.AppendLine("\t\tcount = { min = 2 max = 10 }");
            SystemEffect.AppendLine("\t\tclass = random_non_colonizable");
            SystemEffect.AppendLine("\t\torbit_distance = 20");
            SystemEffect.AppendLine("\t\tchange_orbit = @base_moon_distance");
            SystemEffect.AppendLine("\t\tmoon = {");
            SystemEffect.AppendLine("\t\t\tcount = { min = 0 max = 1 }");
            SystemEffect.AppendLine("\t\t\tclass = random_non_colonizable");
            SystemEffect.AppendLine("\t\t\torbit_angle = { min = 90 max = 270 }");
            SystemEffect.AppendLine("\t\t\torbit_distance = 5");
            SystemEffect.AppendLine("\t\t}");
            SystemEffect.AppendLine("\t}");

            SystemEffect.AppendLine("\tplanet = {");
            SystemEffect.AppendLine($"\t\tname = \"{PlanetName}\"");
            SystemEffect.AppendLine($"\t\tclass = \"{planetclass}\"");
            SystemEffect.AppendLine($"\t\torbit_distance = 35");
            SystemEffect.AppendLine($"\t\torbit_angle = {{ min = 90 max = 270 }}");
            SystemEffect.AppendLine($"\t\tsize = {Size}");
            SystemEffect.AppendLine("\t\thas_ring = no");
            SystemEffect.AppendLine("\t\tinit_effect = {");
            SystemEffect.AppendLine($"\t\t\tset_owner = event_target:{CutOnwerName}");
            SystemEffect.AppendLine($"\t\t\tset_controller = event_target:{CutOnwerName}");
            SystemEffect.AppendLine($"\t\t\tcreate_{CutPlanetName}_colony = yes");
            SystemEffect.AppendLine("\t\t}");
            SystemEffect.AppendLine("\t}");
            SystemEffect.AppendLine("\tinit_effect = {");
            SystemEffect.AppendLine("\t\tsolar_system = {");
            SystemEffect.AppendLine("\t\t\tcreate_starbase = {");
            SystemEffect.AppendLine("\t\t\t\tsize = starbase_outpost");
            SystemEffect.AppendLine($"\t\t\t\towner = event_target:{CutOnwerName}");
            SystemEffect.AppendLine("\t\t\t}");
            SystemEffect.AppendLine("\t\t}");
            SystemEffect.AppendLine("\t}");
            SystemEffect.AppendLine("}");

            return SystemEffect;
        }
    }
}
