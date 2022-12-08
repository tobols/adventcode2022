int[][] ReadFile()
{
	var file = new StreamReader(@"input.txt");
	string line;
	List<int[]> rows = new List<int[]>();

	while ((line = file.ReadLine()) != null)
		rows.Add(line.Select(c => int.Parse(c.ToString())).ToArray());
	file.Close();
	return rows.ToArray();
}

bool IsVisible(int[][] map, int xPos, int yPos)
{
	var height = map[yPos][xPos];
	var n = true;
	var s = true;
	var e = true;
	var w = true;

	for (int y = yPos + 1; y < map.Length; y++)
		if (map[y][xPos] >= height)
		{
			s = false;
			break;
		}

	for (int y = yPos - 1; y >= 0; y--)
		if (map[y][xPos] >= height)
		{
			n = false;
			break;
		}

	for (int x = xPos + 1; x < map[yPos].Length; x++)
		if (map[yPos][x] >= height)
		{
			e = false;
			break;
		}

	for (int x = xPos - 1; x >= 0; x--)
		if (map[yPos][x] >= height)
		{
			w = false;
			break;
		}

	return n || e || s || w;
}

int ScenicScore(int[][] map, int xPos, int yPos)
{
	var height = map[yPos][xPos];
	var score = 1;

	for (int y = yPos + 1; ; y++)
		if (y == map.Length - 1 || map[y][xPos] >= height)
		{
			score *= (y - yPos);
			break;
		}

	for (int y = yPos - 1; ; y--)
		if (y == 0 || map[y][xPos] >= height)
		{
			score *= (yPos - y);
			break;
		}

	for (int x = xPos + 1; ; x++)
		if (x == map[yPos].Length - 1 || map[yPos][x] >= height)
		{
			score *= (x - xPos);
			break;
		}

	for (int x = xPos - 1; ; x--)
		if (x == 0 || map[yPos][x] >= height)
		{
			score *= (xPos - x);
			break;
		}

	return score;
}


var map = ReadFile();
var sumVisible = map.Length * 4 - 4;
for (int i = 1; i < map.Length - 1; i++)
	for (int j = 1; j < map[i].Length - 1; j++)
		if (IsVisible(map, i, j))
			sumVisible++;

var maxScore = 0;
for (int i = 1; i < map.Length - 1; i++)
	for (int j = 1; j < map[i].Length - 1; j++)
	{
		var score = ScenicScore(map, i, j);
		if (score > maxScore)
			maxScore = score;
	}

Console.WriteLine(sumVisible);
Console.WriteLine(maxScore);
