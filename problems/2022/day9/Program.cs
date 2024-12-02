using System;
using System.Collections.Generic;
using System.IO;

public class Program
{
    static (int, int) head = (0, 0);
    static (int, int) tail = (0, 0);
    static (int, int)[] rope =
    {
        (0, 0),
        (0, 0),
        (0, 0),
        (0, 0),
        (0, 0),
        (0, 0),
        (0, 0),
        (0, 0),
        (0, 0),
        (0, 0)
    };

    static Dictionary<(int, int), int> traveledTiles = new Dictionary<(int, int), int>();
    public static void d9(string path)
    {
        string[] file = File.ReadAllLines(path);
        foreach(string line in file)
        {
            string[] command = line.Split(" ");
            ProcessCommand(command[0][0], Int32.Parse(command[1]));
        }
    }

    public static void ProcessCommand(char direction, int distance)
    {
        (int, int) movementVector = (0, 0);
        switch (direction)
        {
            case 'U':
                movementVector = (0, 1);
                break;
            case 'D':
                movementVector = (0, -1);
                break;
            case 'L':
                movementVector = (-1, 0);
                break;
            case 'R':
                movementVector = (1, 0);
                break;
            default:
            break;
        }

        Console.WriteLine($"{direction} {distance}");
        for (int i = 0; i < distance; i++)
        {
            MoveHead(movementVector);
        }
    }

    public static void MoveHead((int, int) movementVector)
    {
        head.Item1 += movementVector.Item1;
        head.Item2 += movementVector.Item2;
        MoveTail();
    }

    public static void MoveTail()
    {
        (int, int) movementVector = (0, 0);

        // Possibly mimic head movement
        if (head.Item1 == tail.Item1 - 2)
        {
            movementVector.Item1 = -1;
        }
        else if (head.Item1 == tail.Item1 + 2)
        {
            movementVector.Item1 = +1;
        }
        else if (head.Item2 == tail.Item2 - 2)
        {
            movementVector.Item2 = -1;
        }
        else if (head.Item2 == tail.Item2 + 2)
        {
            movementVector.Item2 = +1;
        }

        // Cover diagonal movement
        // Horizontal
        if (movementVector.Item1 != 0 && head.Item2 != tail.Item2)
        {
            if ((head.Item2 - tail.Item2) > 0)
            {
                movementVector.Item2 = 1;
            }
            else
            {
                movementVector.Item2 = -1;
            }
            //movementVector.Item1 = (head.Item1 - tail.Item1);
        }
        // Vertical
        if (movementVector.Item2 != 0 && head.Item1 != tail.Item1)
        {
            if ((head.Item1 - tail.Item1) > 0)
            {
                movementVector.Item1 = 1;
            }
            else
            {
                movementVector.Item1 = -1;
            }
            //movementVector.Item1 = (head.Item1 - tail.Item1);
        }

        Console.WriteLine($"\t       Tail movement: ({movementVector.Item1}, {movementVector.Item2})");

        tail.Item1 += movementVector.Item1;
        tail.Item2 += movementVector.Item2;

        // Add to dict
        if (traveledTiles.ContainsKey(tail))
        {
            traveledTiles[tail] += 1;
        }
        else
        {
            traveledTiles.Add(tail, 1);
        }
        Console.WriteLine($"Head: ({head.Item1}, {head.Item2}) | Tail: ({tail.Item1}, {tail.Item2}) | TraveledTile: ({tail.Item1}, {tail.Item2}): {traveledTiles[tail]}");
    }

    /**
    *
    */

    public static void d9p2(string path)
    {
        string[] file = File.ReadAllLines(path);
        foreach(string line in file)
        {
            string[] command = line.Split(" ");
            ProcessCommand2(command[0][0], Int32.Parse(command[1]));
        }
    }

    public static void ProcessCommand2(char direction, int distance)
    {
        (int, int) movementVector = (0, 0);
        switch (direction)
        {
            case 'U':
                movementVector = (0, 1);
                break;
            case 'D':
                movementVector = (0, -1);
                break;
            case 'L':
                movementVector = (-1, 0);
                break;
            case 'R':
                movementVector = (1, 0);
                break;
            default:
            break;
        }

        Console.WriteLine($"{direction} {distance}");
        for (int i = 0; i < distance; i++)
        {
            MoveHead2(movementVector);
        }
    }

    public static void MoveHead2((int, int) movementVector)
    {
        rope[0].Item1 += movementVector.Item1;
        rope[0].Item2 += movementVector.Item2;
        Console.Write($"Head: ({rope[0].Item1}, {rope[0].Item2})");
        MoveNode(1);
    }

    public static void MoveNode(int index)
    {
        // Console.WriteLine($"In MoveNode with index {index}");
        (int, int) movementVector = (0, 0);

        // Possibly mimic head movement
        if (rope[index - 1].Item1 == rope[index].Item1 - 2)
        {
            movementVector.Item1 = -1;
        }
        else if (rope[index - 1].Item1 == rope[index].Item1 + 2)
        {
            movementVector.Item1 = +1;
        }
        else if (rope[index - 1].Item2 == rope[index].Item2 - 2)
        {
            movementVector.Item2 = -1;
        }
        else if (rope[index - 1].Item2 == rope[index].Item2 + 2)
        {
            movementVector.Item2 = +1;
        }

        // Cover diagonal movement
        // Horizontal
        if (movementVector.Item1 != 0 && rope[index - 1].Item2 != rope[index].Item2)
        {
            if ((rope[index - 1].Item2 - rope[index].Item2) > 0)
            {
                movementVector.Item2 = 1;
            }
            else
            {
                movementVector.Item2 = -1;
            }
            //movementVector.Item1 = (rope[index - 1].Item1 - tail.Item1);
        }
        // Vertical
        if (movementVector.Item2 != 0 && rope[index - 1].Item1 != rope[index].Item1)
        {
            if ((rope[index - 1].Item1 - rope[index].Item1) > 0)
            {
                movementVector.Item1 = 1;
            }
            else
            {
                movementVector.Item1 = -1;
            }
            //movementVector.Item1 = (rope[index - 1].Item1 - rope[index].Item1);
        }

        //Console.WriteLine($"\t       Tail movement: ({movementVector.Item1}, {movementVector.Item2})");

        rope[index].Item1 += movementVector.Item1;
        rope[index].Item2 += movementVector.Item2;

        //Console.WriteLine($"Head: ({head.Item1}, {head.Item2}) | Tail: ({tail.Item1}, {tail.Item2}) | TraveledTile: ({tail.Item1}, {tail.Item2}): {traveledTiles[tail]}");
        Console.Write($"| Node {index}: ({rope[index].Item1}, {rope[index].Item2})");

        if (index != rope.Count() - 1)
        {
            MoveNode(index + 1);
        }
        else
        {
            Console.WriteLine();
            // Add to dict
            if (traveledTiles.ContainsKey(rope[index]))
            {
                traveledTiles[(rope[index].Item1, rope[index].Item2)] += 1;
            }
            else
            {
                traveledTiles.Add((rope[index].Item1, rope[index].Item2), 1);
            }
        }
    }

    public static void Main()
    {
        d9p2("day9input.txt");

        Console.WriteLine(traveledTiles.Count);
    }
}