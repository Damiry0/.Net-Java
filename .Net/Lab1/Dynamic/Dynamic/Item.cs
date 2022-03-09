using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamic
{
    public class Item
    {
        public int weight { get; set; }
        public int value { get; set; }

        public string icon { get; set; }

        public float Ratio { get;}
        
        public Item(int weight, int value, string icon)
        {
            this.weight = weight;
            this.value = value;
            this.Ratio = (float)value / (float)weight;
            this.icon = icon;
        }

    }
}
