IEnumerable<string> ReadFile()
{
	var file = new StreamReader(@"input.txt");
	string line;

	while ((line = file.ReadLine()) != null)
		yield return line;
	file.Close();
}

string GetFolderPath(List<string> stack)
{
	var path = "";
	foreach (var folder in stack)
		path += $"{folder},";
	return path;
}

var terminal = ReadFile().ToList();
var folderSizes = new Dictionary<string, long>();
var folderStack = new List<string>();

foreach (var line in terminal)
{
	if (line.StartsWith("$ ls") || line.StartsWith("dir"))
		continue;

	if (line.StartsWith("$ cd"))
	{
		if (line.EndsWith(".."))
			folderStack.RemoveAt(folderStack.Count() - 1);
		else
		{
			var folder = line[4..];
			folderStack.Add(folder);
			var path = GetFolderPath(folderStack);
			if (!folderSizes.ContainsKey(path))
				folderSizes.Add(path, 0);
		}
	}
	else
	{
		var size = int.Parse(line.Split(' ')[0]);
		var path = "";
		foreach (var folder in folderStack)
		{
			path += $"{folder},";
			folderSizes[path] += size;
		}
	}
}

var sum = folderSizes.Where(item => item.Value <= 100000 && item.Key != "/").Select(item => item.Value).Sum();
var availableSpace = 70000000 - folderSizes[" /,"];
var requiredSpace = 30000000 - availableSpace;
var minimumDeleted = folderSizes.Where(item => item.Value >= requiredSpace).Min(item => item.Value);

Console.WriteLine(sum);
Console.WriteLine(minimumDeleted);