
IEnumerable<(int[],int[])> ReadFile()
{
	var file = new StreamReader(@"input.txt");
	string line;

	while ((line = file.ReadLine()) != null)
	{
		var sections = line.Split(',');
		yield return (sections[0].Split('-').Select(int.Parse).ToArray(), sections[1].Split('-').Select(int.Parse).ToArray());
	}
	file.Close();
}

bool Overlap(int a, int x, int y)
	=> a >= x && a <= y;

bool OverlapPair(int[] p1, int[] p2)
	=> Overlap(p1[0], p2[0], p2[1]) || Overlap(p1[1], p2[0], p2[1]) || Overlap(p2[0], p1[0], p1[1]) || Overlap(p2[1], p1[0], p1[1]);


var pairs = ReadFile();

var contains = pairs.Where(p => (p.Item1[0] <= p.Item2[0] && p.Item1[1] >= p.Item2[1]) || (p.Item2[0] <= p.Item1[0] && p.Item2[1] >= p.Item1[1])).ToList();
Console.WriteLine(contains.Count());

var overlaps = pairs.Where(p => OverlapPair(p.Item1, p.Item2)).ToList();
Console.WriteLine(overlaps.Count());

