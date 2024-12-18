using GlasgowZoo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zoo3
{
    internal class Cages
    {
        public int cageID { get; set; }
        public string cageName { get; set; }
        public List<Animals> animals { get; set; } = new List<Animals>();
        public List<Keepers> keepers { get; set; } = new List<Keepers>();

        public Cages(int id, string name)
        {
            cageID = id;
            cageName = name;
        }
    }
}
