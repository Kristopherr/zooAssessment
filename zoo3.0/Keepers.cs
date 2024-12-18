using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlasgowZoo
{
    internal class Keepers
    {
        public string keeperName { get; set; }
        public string keeperSpecialty { get; set; }

        public Keepers(string name)
        {
            keeperName = name;
        }
    }
}
