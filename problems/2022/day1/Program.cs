using System;
using System.Collections.Generic;
using System.IO;

public class Day1
{
    static void d1(string path) {
        string[] lines = File.ReadAllLines(path);

        List<List<int>> elves = new List<List<int>>();

        int index = 0;
        elves.Add(new List<int>());
        foreach(string line in lines)
        {
            if (line == "")
            {
                index++;
                elves.Add(new List<int>());
                continue;
            }
            elves[index].Add(Int32.Parse(line));
        }

        List<int> elfCalories = new List<int>();

        foreach(List<int> elf in elves)
        {
            int elfCount = 0;
            foreach(int calorieCount in elf)
            {
                elfCount += calorieCount;
            }
            elfCalories.Add(elfCount);
        }

        elfCalories.Sort();
        foreach(int i in elfCalories)
        {
            Console.WriteLine(i);
        }

        int sumOfTopThree = elfCalories[elfCalories.Count() - 1] + elfCalories[elfCalories.Count() - 2] + elfCalories[elfCalories.Count() - 3];

        Console.WriteLine($"{elfCalories[elfCalories.Count() - 1]}");
        Console.WriteLine($"{sumOfTopThree}");
    }

    public static void Main()
    {
        d1("d1input.txt");
    }
}