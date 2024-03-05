using System.Drawing;

public class Map
{
	private char[][] RawMap;

	public int Height { get => RawMap.Length; }
	public int Width { get => RawMap[0].Length; }

	public Map(int width, int height)
	{
		RawMap = new char[height][];

		for (int i = 0; i < height; i++)
			RawMap[i] = new char[width];
		Clear();
	}

	public void DebugMap()
	{
		Debugger.Debug(ToString());
	}

	public string ToString(bool lineBreak = true)
	{
		if (lineBreak)
			return string.Join("", RawMap.Select(row => $"{string.Join("", row)}\n"));
		return string.Join("", RawMap.Select(row => $"{string.Join("", row)}"));
	}


	public void Clear()
	{
		for (int i = 0; i < Height; i++)
			for (int j = 0; j < Width; j++)
				RawMap[i][j] = ' ';
	}

	public void SetElement(Position p, char c)
	{
		if (IsPositionWithinMap(p))
			RawMap[p.y][p.x] = c;
	}

	public void SetElement(Position p, string s)
	{
		for (int i = 0; i < s.Length; i++)
		{
			Position newPosition = new(p.x + i, p.y);
			if (IsPositionWithinMap(newPosition))
				RawMap[newPosition.y][newPosition.x] = s[i];
		}
	}

	public void RemoveElement(Position p)
	{
		if (IsPositionWithinMap(p))
			RawMap[p.y][p.x] = ' ';
	}

	public void RemoveElement(Position p, string s)
	{
		for (int i = 0; i < s.Length; i++)
		{
			Position newPosition = new(p.x + i, p.y);
			if (IsPositionWithinMap(newPosition))
				RawMap[newPosition.y][newPosition.x] = ' ';
		}
	}

	public char? GetElement(Position p)
	{
		if (IsPositionWithinMap(p))
			return RawMap[p.y][p.x];
		return null;
	}

	bool IsPositionWithinMap(Position p)
	{
		return p.x >= 0 && p.y >= 0 && p.x < Width && p.y < Height;
	}
}
