IEnumerable<(char[], char[])> ReadFile()
{
	var file = new StreamReader(@"input.txt");
	string line;

	while ((line = file.ReadLine()) != null)
	{
		var length = line.Length / 2;
		yield return (line[0..length].ToCharArray(), line[length..].ToCharArray());
	}
	file.Close();
}


int GetPriority(char c)
{
	if (char.IsLower(c))
		return c - 96;
	else
		return c - 38;
}

var sacks = ReadFile().ToList();

var sum = 0;
foreach (var comp in sacks)
{
	var aaa = comp.Item1.Intersect(comp.Item2);
	sum += GetPriority(aaa.First());
}

Console.WriteLine(sum);

sum = 0;
for (int i = 0; i < sacks.Count(); i += 3)
{
	var elf1 = sacks[i].Item1.Concat(sacks[i].Item2).ToArray();
	var elf2 = elf1.Intersect(sacks[i + 1].Item1.Concat(sacks[i + 1].Item2).ToArray());
	var elf3 = elf2.Intersect(sacks[i + 2].Item1.Concat(sacks[i + 2].Item2).ToArray());
	sum += GetPriority(elf3.First());
}

Console.WriteLine(sum);

