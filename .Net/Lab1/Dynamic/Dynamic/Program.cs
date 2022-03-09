using Dynamic;

Console.WriteLine("Input bag capacity:");
var bagTarget = new Bag(int.Parse(s: Console.ReadLine()));
Console.WriteLine("Input number of items:");
var bagSource = new Bag(int.Parse(s: Console.ReadLine()));
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