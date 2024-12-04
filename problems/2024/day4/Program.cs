using System.Reflection.Metadata.Ecma335;

namespace Day4;
public class Program
{
    static void day4part1(string path)
    {
        string[] lines = File.ReadAllLines(path);

        int xmasCount = 0;
        List<List<char>> wordSearch = new List<List<char>>();

        for(int i = 0; i < lines.Length; i++)
        {
            wordSearch.Add(new List<char>());
            foreach(char c in lines[i])
            {
                wordSearch[i].Add(c);
            }
        }
        
        /*
        for(int i = 0; i < wordSearch.Count; i++)
        {
            for(int j = 0; j < wordSearch[i].Count; j++)
            {
                Console.Write(wordSearch[i][j]);
            }
            Console.WriteLine();
        }*/

        //Console.WriteLine("Specific character at (3, 2): " + wordSearch[1][2]);
        // Look up at X, Y coordinates (1-based): wordSearch[Y - 1][X - 1]

        for(int y = 0; y < wordSearch.Count; y++)
        {
            for(int x = 0; x < wordSearch[y].Count; x++)
            {
                if(wordSearch[y][x] == 'X')
                {
                    xmasCount += FindNumXMASFromPoint(x, y, wordSearch);
                }
            }
        }

        Console.WriteLine("End result: " + xmasCount);
    }

    public static int FindNumXMASFromPoint(int x, int y, List<List<char>> wordSearch)
    {
        int numXMAS = 0;

        List<(int, int)> vectorList = new List<(int, int)> ();
        vectorList.Add((1, 0));
        vectorList.Add((1, 1));
        vectorList.Add((1, -1));
        vectorList.Add((0, 1));
        vectorList.Add((0, -1));
        vectorList.Add((-1, 1));
        vectorList.Add((-1, 0));
        vectorList.Add((-1, -1));

        foreach((int, int) vector in vectorList)
        {
            if (XMASInDirection(x, y, wordSearch, vector))
            {
                //Console.WriteLine("XMAS found at: (" + (x + 1) + ", " + (y + 1) + ") in direction: (" + vector.Item1 + ", " + vector.Item2 + ")");
                numXMAS++;
            }
        }

        return numXMAS;
    }

    public static bool XMASInDirection(int x, int y, List<List<char>> wordSearch, (int, int) vector)
    {
        try
        {
            if (!(wordSearch[y][x] == 'X'))
            {
                return false;
            }
            if (!(wordSearch[y + vector.Item2][x + vector.Item1] == 'M'))
            {
                return false;
            }
            if (!(wordSearch[y + (2*vector.Item2)][x + (2*vector.Item1)] == 'A'))
            {
                return false;
            }
            if (!(wordSearch[y + (3*vector.Item2)][x + (3*vector.Item1)] == 'S'))
            {
                return false;
            }
        }
        catch (ArgumentOutOfRangeException)
        {
            return false;
        }

        return true;
    }

    public static void day4part2(string path)
    {
        string[] lines = File.ReadAllLines(path);

        int xmasCount = 0;
        List<List<char>> wordSearch = new List<List<char>>();

        for(int i = 0; i < lines.Length; i++)
        {
            wordSearch.Add(new List<char>());
            foreach(char c in lines[i])
            {
                wordSearch[i].Add(c);
            }
        }
        
        // Look up at X, Y coordinates (1-based): wordSearch[Y - 1][X - 1]

        for(int y = 0; y < wordSearch.Count; y++)
        {
            for(int x = 0; x < wordSearch[y].Count; x++)
            {
                if(IsX_MAS(x, y, wordSearch))
                {
                    xmasCount++;
                }
            }
        }

        Console.WriteLine("End result: " + xmasCount);
    }

    public static bool IsX_MAS(int x, int y, List<List<char>> wordSearch)
    {
        if (!(wordSearch[y][x] == 'A'))
        {
            return false;
        }
        //Console.WriteLine("A found at (" + (x + 1) + ", " + (y + 1) + ")");

        bool topLeftToBotRight = false;
        bool botLeftToTopRight = false;

        try
        {
            if ((wordSearch[y - 1][x - 1] == 'M' && wordSearch[y + 1][x + 1] == 'S')
            || (wordSearch[y - 1][x - 1] == 'S' && wordSearch[y + 1][x + 1] == 'M'))
            {
                //Console.WriteLine("'\\' found at (" + (x + 1) + ", " + (y + 1) + ")");
                topLeftToBotRight = true;
            }

            if ((wordSearch[y + 1][x - 1] == 'M' && wordSearch[y - 1][x + 1] == 'S')
            || (wordSearch[y + 1][x - 1] == 'S' && wordSearch[y - 1][x + 1] == 'M'))
            {
                //Console.WriteLine("'/' found at (" + (x + 1) + ", " + (y + 1) + ")");
                botLeftToTopRight = true;
            }
        }
        catch (ArgumentOutOfRangeException)
        {
        }

        if (topLeftToBotRight && botLeftToTopRight)
        {
            //Console.WriteLine("X-MAS found!");
            return true;
        }
        return false;
    }

    public static void Main(string[] args)
    {
        //day4part1("..\\..\\..\\day4input.txt");
        day4part2("..\\..\\..\\day4input.txt");
    }
}