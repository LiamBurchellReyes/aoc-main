using System.Collections.ObjectModel;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace Day6;
public class Program
{
    static void day6part1(string path)
    {
        string[] lines = File.ReadAllLines(path);

        int result = 0;
        int width = lines[0].Length;
        int height = lines.Length;

        List<char> map = new List<char>();

        foreach (string line in lines)
        {
            foreach (char c in line)
            {
                map.Add(c);
            }
        }

        result = GetDistinctPositionsFromMap(map, width, height);
        //PrintMap(map, width, height);

        //(int x, int y) = GetCoordinatesFromIndex(23, width, height);
        //Console.WriteLine("Index 23 gives coordinates: (" + (x + 1) + ", " + (y + 1) + ")");
        //Console.WriteLine("Coordinate (4, 3) gives index: " + GetIndexFromCoordinates((3, 2), width, height));

        Console.WriteLine("End result: " + result);
    }

    static int GetDistinctPositionsFromMap(List<char> map, int width, int height)
    {
        while (GetGuardIndex(map) != -1)
        {
            StepForward(ref map, width, height);
        }

        int xCount = 0;
        foreach (char c in map)
        {
            if (c == 'X')
            {
                xCount++;
            }
        }
        return xCount;
    }

    static void StepForward (ref List<char> map, int width, int height)
    {
        int guardIndex = GetGuardIndex(map);
        (int guardX, int guardY) = GetCoordinatesFromIndex(guardIndex, width, height);
        (int x, int y) vector = GetGuardDirVector(map[guardIndex]);
        (int newX, int newY) newCoordinates = (guardX + vector.x, guardY + vector.y);

        // Walks off the map
        if (newCoordinates.newX >= width || newCoordinates.newX < 0 || newCoordinates.newY >= height || newCoordinates.newY < 0)
        {
            map[guardIndex] = 'X';
            return;
        }
        int newIndex = GetIndexFromCoordinates(newCoordinates, width, height);
        if (map[newIndex] == '#')
        {
            map[guardIndex] = GetRotatedGuard(map[guardIndex]);
        }
        else
        {
            map[newIndex] = map[guardIndex];
            map[guardIndex] = 'X';
        }
    }

    static int GetGuardIndex(List<char> map)
    {
        int upIndex = map.IndexOf('^');
        if (upIndex != -1)
        {
            return upIndex;
        }

        int downIndex = map.IndexOf('v');
        if (downIndex != -1)
        {
            return downIndex;
        }

        int rightIndex = map.IndexOf('>');
        if (rightIndex != -1)
        {
            return rightIndex;
        }

        int leftIndex = map.IndexOf('<');
        if (leftIndex != -1)
        {
            return leftIndex;
        }

        return -1;
    }

    static (int x, int y) GetGuardDirVector(char guard)
    {
        switch (guard)
        {
            case '^':
                return (0, -1);
            case '>':
                return (1, 0);
            case 'v':
                return (0, 1);
            case '<':
                return (-1, 0);
            default:
                throw new Exception("Unrecognized guard!");
        }
    }

    static char GetRotatedGuard(char guard)
    {
        switch (guard)
        {
            case '^':
                return '>';
            case '>':
                return 'v';
            case 'v':
                return '<';
            case '<':
                return '^';
            default:
                throw new Exception("Unrecognized guard!");
        }
    }

    public static void PrintMap(List<char> map, int width, int height)
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Console.Write(map[GetIndexFromCoordinates((x, y), width, height)]);
            }
            Console.WriteLine();
        }
    }

    static int GetIndexFromCoordinates((int x, int y) coordinate, int width, int height)
    {
        return coordinate.x + (width * coordinate.y);
    }

    static (int x, int y) GetCoordinatesFromIndex(int index, int width, int height)
    {
        int x = index % width;
        int y = (int) (index / width);
        return (x, y);
    }

    public static void Main(string[] args)
    {
        day6part1("..\\..\\..\\day6input.txt");
        //day6part2("..\\..\\..\\day6input.txt");
    }
}