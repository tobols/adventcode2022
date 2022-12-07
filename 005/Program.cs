
using System.Diagnostics;

IEnumerable<string> ReadInstructions()
{
	var file = new StreamReader(@"instructions.txt");
	string line;
	while ((line = file.ReadLine()) != null)
		yield return line;
	file.Close();
}

List<string> GetStartState()
{
	var file = new StreamReader(@"startstate.txt");
	List<string> lines = new List<string>();
	string line;
	while ((line = file.ReadLine()).Contains('['))
		lines.Add(line);

	var x = int.Parse(line.Split(' ').Where(str => !string.IsNullOrWhiteSpace(str)).Last());
	List<string> stacks = new List<string>();
	for (int i = 0; i < x; i++)
		stacks.Add("");

	for (int i = lines.Count() - 1; i >= 0; i--)
		for (int j = 0; j < x; j++)
			if (lines[i][j * 4 + 1] != ' ')
				stacks[j] = stacks[j] + lines[i][j * 4 + 1];

	file.Close();
	return stacks;
}

var state = GetStartState();
var instructions = ReadInstructions();
foreach (var instruction in instructions)
{
	var instr = instruction.Split(' ');
	var count = int.Parse(instr[1]);
	var from = int.Parse(instr[3]);
	var to = int.Parse(instr[5]);

	for (int i = 0; i < count; i++)
	{
		state[to - 1] = state[to - 1] + state[from - 1].Last();
		state[from - 1] = state[from - 1][..(state[from - 1].Length - 1)];
	}
}

var tops = state.Select(c => c.Last()).ToArray();
Console.WriteLine(new string(tops));


state = GetStartState();
foreach (var instruction in instructions)
{
	var instr = instruction.Split(' ');
	var count = int.Parse(instr[1]);
	var from = int.Parse(instr[3]);
	var to = int.Parse(instr[5]);

	var sFr = state[from - 1];
	var sTo = state[to - 1];
	state[to - 1] = sTo + sFr[(sFr.Length - count)..];
	state[from - 1] = sFr[..(sFr.Length - count)];
}

tops = state.Select(c => c.Last()).ToArray();
Console.WriteLine(new string(tops));

