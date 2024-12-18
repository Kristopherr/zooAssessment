using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zoo3
{
    internal class Enclosure
    {
        public int enclosureID { get; set; }
        public string enclosureName { get; set; }
        public List<Cages> cages { get; set; } = new List<Cages>();

        public Enclosure(int id, string name)
        {
            enclosureID = id;
            enclosureName = name;
        }
    }
}
