using System;
using System.Collections.Generic;
using System.Text;

namespace Planet_Creator.Classes
{
    public class Planet
    {
        public string Name { get; set; }
        public int Size { get; set; }
        public string planetclass { get; set; }
        public Dictionary<string, int> Species { get; set; } = new Dictionary<string, int>();
        public Dictionary<string, int> Districts { get; set; } = new Dictionary<string, int>();
        public Dictionary<string, int> Buildings { get; set; } = new Dictionary<string, int>();
        public Dictionary<string, int> Features { get; set; } = new Dictionary<string, int>();
        public List<string> Modifiers { get; set; } = new List<string>();
    }

    public class System
    {
        public string Name { get; set; }
        public Planet planet { get; set; }
    }
}
