using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamic
{
    public class Bag
    {
        private List<Item> Items { get; init; }

        private int maxSize { get; set; }
        public Bag(int size)
        {
            this.Items = new List<Item>();
            this.maxSize = size;
        }

        public void Display()
        {
            foreach (var item in Items)
            {
                Console.WriteLine("Weight:{0},Value:{1},Ratio:{2}",item.weight,item.value,item.Ratio);
            }
        }

        public void SortByRatio()
        {
            this.Items.Sort((p,q)=>p.Ratio.CompareTo(q.Ratio));
            this.Items.Reverse();
        }

        public void KnapSack(Bag sourceBag)
        {
            var currentSize = 0;
            this.SortByRatio();
            foreach (var item in sourceBag.Items)
            {
                if (this.maxSize >= (currentSize + item.weight))
                {
                    currentSize = currentSize + item.weight;
                    this.Items.Add(item);
                }
                
                else continue;

            }
        }
        public void GenerateRandomBackpack(int seed)
        {
            var fixRand = new Random(seed);
            
            for (var i = 0; i < this.maxSize; i++)
            {
                var randomWeight = fixRand.Next(1,20);
                var randomValue = fixRand.Next(1,30);
                this.Items.Add(new Item(randomWeight, randomValue));
            }
        }

        public override string ToString()
        {
            var str = "Items: \n";
            return Items.Aggregate(str, (current, item) => current + ("Weight: " + item.weight + " Value: " + item.value + " Ratio: " + item.Ratio + "\n"));
        }

    }
}
