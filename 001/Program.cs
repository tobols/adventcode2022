
var elves = ReadFile();
var cals = elves.Select(e => e.Sum()).OrderByDescending(e => e).ToList();

Console.WriteLine(cals.First());
Console.WriteLine(cals.Take(3).Sum());



IEnumerable<IEnumerable<int>> ReadFile()
{
	var file = new StreamReader(@"input2.txt");
	string line;

	List<int> cals = new List<int>();
	List<List<int>> elves = new List<List<int>>();

	while ((line = file.ReadLine()) != null)
	{
		if (line == "")
		{
			elves.Add(cals);
			cals = new List<int>();
			continue;
		}
		cals.Add(int.Parse(line));
	}

	file.Close();
	return elves;
}