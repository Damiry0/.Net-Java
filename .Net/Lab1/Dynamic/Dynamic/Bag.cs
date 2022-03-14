using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamic
{
    public class Bag
    {
        public List<Item> Items { get; init; }

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

            var iconList = new List<string>()
            {
                new string("💎"),
                new string("💍"),
                new string("💰"),
                new string("🦄"),
                new string("💻"),
                new string("💲"),
                new string("💩"),
            };
            
            for (var i = 0; i < this.maxSize; i++)
            {
                var randomWeight = fixRand.Next(1,20);
                var randomValue = fixRand.Next(1,30);
                var randomIconIndex = fixRand.Next(1, 7);
                this.Items.Add(new Item(randomWeight, randomValue,iconList[randomIconIndex]));
            }
        }

        public override string ToString()
        {
            var str = "";
            return Items.Aggregate(str, (current, item) => current + ("Item: "+ item.icon + " Weight: " + item.weight + "\n" + "Value: " + item.value + " Ratio: " + item.Ratio + "\n"));
        }

    }
}
