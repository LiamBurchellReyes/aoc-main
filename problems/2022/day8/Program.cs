using System;
using System.Collections.Generic;
using System.IO;

struct Tree
{
    public Tree(int newSize, int newX, int newY)
    {
        size = newSize;
        x = newX;
        y = newY;
    }
    public int x;
    public int y;
    public int size;
};

public class Program
{
    static void d8(string path) {
        string[] file = File.ReadAllLines(path);
        int [,] grid = new int[file[0].Length, file.Length];

        // Create grid
        for (int y = 0; y < file.Length; y++)
        {
            string row = file[y];
            for (int x = 0; x < row.Length; x++)
            {
                grid[x, y] = (int) Char.GetNumericValue(row[x]);
                //Console.Write(grid[x, y]);
            }
            //Console.WriteLine();
        }

        //Console.WriteLine(isVisible(grid, 51, 0));
        int numVisibleTrees = 0;
        List<Tree> visibleTrees = new List<Tree>();

        for (int x = 0; x < grid.GetLength(0); x++)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                //Console.WriteLine($"({x}, {y}): Value {grid[x, y]} | {isVisible(grid, x, y)}");
                //if (isVisible(grid, x, y))
                if (isVisible(grid, (x, y), (0, 1)))
                {
                    numVisibleTrees++;
                    visibleTrees.Add(new Tree(grid[x, y], x, y));
                }
            }
        }

        foreach(Tree tree in visibleTrees)
        {
            //Console.WriteLine($"{tree.size}: ({tree.x}, {tree.y})");
        }
        Console.WriteLine($"Visible trees: {numVisibleTrees}");
    }

    static bool isVisible(int[,] grid, (int treeX, int treeY) tree, (int, int) vector)
    {
        


        return false;
    }

    /*static bool isVisible(int[,] grid, int treeX, int treeY) {
        int treeValue = grid[treeX, treeY];

        //Console.WriteLine($"treeX: {treeX}");
        //Console.WriteLine($"treeY: {treeY}");

        //Console.WriteLine($"grid.GetLength(0): {grid.GetLength(0)}");
        //Console.WriteLine($"grid.GetLength(1): {grid.GetLength(1)}");
        
        // Check northward
        bool isNorthVisible = true;
        for (int y = (treeY - 1); y > 0; y--)
        {
            Console.WriteLine($"Tree ({treeX}, {treeY}) (Value {grid[treeX, treeY]}) compared to ({treeX}, {y}) (Value {grid[treeX, y]})");
            if (grid[treeX, y] >= treeValue)
            {
                isNorthVisible = false;
                break;
            }
        }
        if (isNorthVisible)
        {
            return true;
        }

        // Check southward
        /*bool isSouthVisible = true;
        for (int y = treeY + 1; y < grid.GetLength(0); y--)
        {
            if (grid[treeX, y] >= treeValue)
            {
                isSouthVisible = false;
                break;
            }
        }
        if (isSouthVisible)
        {
            return true;
        }*/

        //return false;
        

        // Check horizontal line
        /*for (int x = 0; x < grid.GetLength(1); x++)
        {
            //Console.WriteLine($"Is {treeValue} ({treeX}, {treeY}) greater than {grid[x, treeY]} ({x}, {treeY})?)");
            if (x == treeX)
            {
                //Console.WriteLine("Skipped by symmetry");
                continue;
            }
            if (grid[x, treeY] >= treeValue)
            {
                return false;
            }
        }
        // Check vertical line
        for (int y = 0; y < grid.GetLength(0); y++)
        {
            //Console.WriteLine($"Is {treeValue} ({treeX}, {treeY}) greater than {grid[treeX, y]} ({treeX}, {y})?)");
            if (y == treeY)
            {
                //Console.WriteLine("Skipped by symmetry");
                continue;
            }
            if (grid[treeX, y] >= treeValue)
            {
                return false;
            }
        }

        return true;*/
    //}

    public static void TestMethod(string path)
    {
        string[] file = File.ReadAllLines(path);
        Array.Sort(file);

        foreach (string line in file)
        {
            double highestChar = Char.GetNumericValue(line[0]);
            for (int i = 0; i < line.Length; i++)
            {
                if (Char.GetNumericValue(line[i]) > highestChar)
                {
                    highestChar = Char.GetNumericValue(line[i]);
                }
            }

            Console.WriteLine($"{line} - {highestChar}");
        }
    }

    public static void Main()
    {
        //d8("day8/d8input.txt");
        d8("day8/d8copyinput.txt");

        //TestMethod("day8/d8input.txt");
        /*int[,] grid = {
            {3,0,3,7,3},
            {2,5,5,1,2},
            {6,5,3,3,2},
            {3,3,5,4,9},
            //{3,5,3,9,0}
        };*/
        //Console.WriteLine(isVisible(grid, 1, 1));
    }
}