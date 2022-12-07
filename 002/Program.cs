IEnumerable<(char, char)> ReadFile()
{
	var file = new StreamReader(@"input.txt");
	string line;

	while ((line = file.ReadLine()) != null)
		yield return (line[0], line[2]);

	file.Close();
}

var rpsA = new char[] { 'A', 'B', 'C' };
var rpsB = new char[] { 'X', 'Y', 'Z' };
var points = new int[] { 3, 6, 0 };

var rounds = ReadFile();
var sum = 0;
foreach (var round in rounds)
{
	var aa = Array.IndexOf(rpsA, round.Item1);
	var bb = Array.IndexOf(rpsB, round.Item2);
	var cc = (bb + 3 - aa) % 3;

	sum += bb + 1 + points[cc];
}

Console.WriteLine(sum);

sum = 0;
foreach (var round in rounds)
{
	var aa = Array.IndexOf(rpsA, round.Item1);
	var bb = Array.IndexOf(rpsB, round.Item2);
	var cc = (bb + 3 - 1) % 3;
	aa = (aa + cc) % 3;

	sum += aa + 1 + points[cc];
}

Console.WriteLine(sum);

