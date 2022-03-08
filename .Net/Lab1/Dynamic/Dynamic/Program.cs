using Dynamic;

Console.WriteLine("Input bag capacity:");
var bagCapacity = int.Parse(s: Console.ReadLine());
var bagTarget = new Bag(bagCapacity);
var bagSource = new Bag(bagCapacity + 20);

Console.WriteLine("Input seed:");
bagSource.GenerateRandomBackpack(int.Parse(Console.ReadLine()));
Console.WriteLine("Generated backpack:");
bagSource.Display();
Console.WriteLine("Sorted generated backpack:");
bagSource.SortByRatio();
bagSource.Display();
Console.WriteLine("Solved:");
bagTarget.KnapSack(bagSource);
bagTarget.Display();



// Console.WriteLine("Vault:");
// bag.Display();
// Console.WriteLine("Backpack:");
// var backpack = bag.KnapSack();
// foreach (var item in backpack)
// {
//     Console.WriteLine("Weight:{0},Value:{1},Ratio:{2}",item.weight,item.value,item.Ratio);
// }
