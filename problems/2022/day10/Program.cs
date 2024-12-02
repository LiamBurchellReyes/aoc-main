using System;
using System.Collections.Generic;
using System.IO;

public class Program
{
    enum Command
    {
        Noop,
        None,
        AddX
    }
    static Queue<(Command, int)> cycle = new Queue<(Command, int)>();
    static int index = 1;
    static int strength = 1;

    static int coolSignal = 0;
    static List<int> coolSignals = new List<int>();

    static string CRTRow = "";
    static List<string> CRTScreen = new List<string>();

    public static void d10p1(string path)
    {
        string[] file = File.ReadAllLines(path);
        foreach(string line in file)
        {
            if (line == "noop")
            {
                cycle.Enqueue((Command.Noop, 0));
            }
            else
            {
                string[] command = line.Split(" ");
                cycle.Enqueue((Command.None, 0));
                cycle.Enqueue((Command.AddX, Int32.Parse(command[1])));
            }
        }

        while (cycle.Count != 0)
        {
            ProcessCommand();
        }

        // foreach (string line in CRTScreen)
        // {
        //     Console.WriteLine(line);
        // }

        /*int sumCoolSignals = 0;
        foreach (int i in coolSignals)
        {
            Console.WriteLine($"Signal: {i}");
            sumCoolSignals += i;
        }*/

        Console.WriteLine($"Signal Strength Sum: {coolSignals.Last()}");
    }

    public static void ProcessCommand()
    {
        /*Console.Write("Sprite position: ");
        for (int i = 0; i < 40; i++)
        {
            Console.Write(isSpriteVisible(i) ? "#" : ".");
        }

        Console.WriteLine();
        Console.WriteLine();

        Console.Write($"Start cycle {index}: ");

        if (isSpriteVisible((index % 40)))
        {
            CRTRow += "#";
            //Console.Write("#");
        }
        else
        {
            CRTRow += ".";
            //Console.Write(".");
        }

        if (index % 40 == 0)
        {
            CRTScreen.Add(CRTRow);
            CRTRow = "";
            //Console.WriteLine();
        }

        Console.WriteLine($"Current CRT row: {CRTRow}");

        Console.Write($"During cycle {index}: ");*/

        (Command command, int amount) = cycle.Dequeue();
        if (command == Command.Noop)
        {
            //Console.WriteLine($"command noop");
        }
        if (command == Command.None)
        {
            //Console.WriteLine($"starting command addx");
        }
        if (command == Command.AddX)
        {
            //Console.WriteLine($"finishing command addx {amount}");
            strength += amount;
        }

        index++;

        if (index == 20
            || index == 60
            || index == 100
            || index == 140
            || index == 180
            || index == 220)
        {
            coolSignal += (index * strength);
            coolSignals.Add(coolSignal);
            //Console.WriteLine($"{index} | {strength} | {index * strength}");
        }
        // Console.WriteLine($"{command.ToString()} | {amount}");

    }

    public static void d10p2(string path)
    {
        string[] file = File.ReadAllLines(path);
        foreach(string line in file)
        {
            if (line == "noop")
            {
                cycle.Enqueue((Command.Noop, 0));
            }
            else
            {
                string[] command = line.Split(" ");
                cycle.Enqueue((Command.None, Int32.Parse(command[1])));
                cycle.Enqueue((Command.AddX, Int32.Parse(command[1])));
            }
        }

        Console.Write("Sprite position: ");
        DrawSprite();
        Console.WriteLine("\n");
        while (cycle.Count != 0)
        {
            //Console.WriteLine($"Cycle.count: {cycle.Count}");
            ProcessCommand2();
        }
        CRTScreen.Add(CRTRow);

        foreach (string line in CRTScreen)
        {
            Console.WriteLine(line);
        }

        //Console.WriteLine($"Signal Strength Sum: {coolSignals.Last()}");
    }

    public static void ProcessCommand2()
    {
        Console.Write($"Start cycle \t {index}: Command to process: ");

        (Command command, int amount) = cycle.Dequeue();
        if (command == Command.Noop)
        {
            Console.WriteLine($"noop");
        }
        if (command == Command.None)
        {
            Console.WriteLine($"start addx {amount}");
        }
        if (command == Command.AddX)
        {
            Console.WriteLine($"finish command addx {amount}");
        }

        Console.WriteLine($"During cycle \t {index}: Draw pixel in CRT position: {((index - 1) % 40)}");
        if (((index - 1) % 40) == 0)
        {
            CRTScreen.Add(CRTRow);
            CRTRow = "";
        }
        if (IsSpriteVisible(((index - 1) % 40)))
        {
            CRTRow += "#";
        }
        else
        {
            CRTRow += ".";
        }

        Console.WriteLine($"CRT Row: \t   : {CRTRow}");
        //Console.WriteLine($"During cycle \t {index}: Execute command {command.ToString()}");
        if (command == Command.AddX)
        {
            strength += amount;
        }

        Console.Write($"Sprite position:\t");
        DrawSprite();
        Console.WriteLine("");

        index++;
    }

    public static bool IsSpriteVisible(int coordinate)
    {
        bool isVisible = false;
        if (coordinate == strength - 1
            || coordinate == strength
            || coordinate == strength + 1)
        {
            isVisible = true;
        }
        return isVisible;
    }

    public static void DrawSprite()
    {
        for (int i = 0; i < 40; i++)
        {
            Console.Write(IsSpriteVisible(i) ? "#" : ".");
        }
    }
    public static void Main()
    {
        //d10p1("day10input.txt");
        //d10p1("day10smallinput.txt");
        //d10p2("day10smallinput.txt");
        d10p2("day10input.txt");
    }
}