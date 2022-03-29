using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class Beer
    {
        public string Name { get; set; }

        public string alpha_acid_min { get; set; }

        public string alpha_acid_max { get; set; }

        public string beta_acid_min { get; set; }

        public string beta_acid_max { get; set; }

        public List<string> purpose { get; set; }

        public string country { get; set; }

        public string description { get; set; }

        public List<string> substitutions { get; set; }

        public override string ToString()
        {
            return Name + " " + country;
        }
    }
}
