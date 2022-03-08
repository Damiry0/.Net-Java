using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamic
{
    internal class Item
    {
        public int weight { get; set; }
        public int value { get; set; }

        public float Ratio { get;}
        
        public Item(int weight, int value)
        {
            this.weight = weight;
            this.value = value;
            this.Ratio = (float)value / (float)weight;
        }

    }
}
