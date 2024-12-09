using System.Reflection.Metadata.Ecma335;
using System.Xml.XPath;

namespace Day5;
public class Program
{
    static void day5part1(string path)
    {
        string[] lines = File.ReadAllLines(path);
        Dictionary<int, List<int>> rules = new Dictionary<int, List<int>>();

        bool settingRules = true;

        int result = 0;

        foreach (string line in lines)
        {
            if (line == "")
            {
                settingRules = false;
                //Console.WriteLine("Rules:");
                /*foreach (int i in rules.Keys)
                {
                    foreach (int j in rules[i])
                    {
                        Console.WriteLine(j + " must come after " + i);
                    }
                }*/
                continue;
            }
            if (settingRules)
            {
                string[] rule = line.Split('|');
                int leftNum = Int32.Parse(rule[0]);
                int rightNum = Int32.Parse(rule[1]);
                if (!rules.ContainsKey(rightNum))
                {
                    //Console.WriteLine("Adding that " + rightNum + " must come after " + leftNum);
                    rules.Add(rightNum, new List<int>());
                    rules[rightNum].Add(leftNum);
                }
                else
                {
                    //Console.WriteLine("Adding that " + rightNum + " must come after " + leftNum);
                    rules[rightNum].Add(leftNum);
                }
            }
            else
            {
                result += GetResultFromUpdate(line, rules);
            }

        }

        Console.WriteLine("End result: " + result);
    }

    /// <summary>
    /// Get the middle page value from a line given a set of rules
    /// Go from LEFT to RIGHT, checking for any applicable rules. If so, add those to exclusions.
    /// </summary>
    /// <returns>0 if incorrectly-ordered. Middle page number otherwise</returns>
    public static int GetResultFromUpdate(string line, Dictionary<int, List<int>> rules)
    {
        string[] pageNums = line.Split(',');
        List<int> exclusions = new List<int>();

        //Console.WriteLine("Checking line: " + line);

        for (int i = 0; i < pageNums.Length; i++)
        {
            int num = Int32.Parse(pageNums[i]);

            if (exclusions.Contains(num))
            {
                //Console.WriteLine(num + " in exclusions. Returning 0");
                return 0;
            }

            if (rules.ContainsKey(num))
            {
                foreach (int rule in rules[num])
                {
                    //Console.WriteLine("Number " + num + " adds exclusion " + rule);
                    exclusions.Add(rule);
                }
            }
        }

        int middleNumber = Int32.Parse(pageNums[(pageNums.Length - 1)/2]);
        //Console.WriteLine("Value number found. Returning " + middleNumber);
        return middleNumber;
    }

    static void day5part2(string path)
    {
        string[] lines = File.ReadAllLines(path);
        Dictionary<int, List<int>> rules = new Dictionary<int, List<int>>();

        bool settingRules = true;

        int result = 0;

        foreach (string line in lines)
        {
            if (line == "")
            {
                settingRules = false;
                //Console.WriteLine("Rules:");
                /*foreach (int i in rules.Keys)
                {
                    foreach (int j in rules[i])
                    {
                        Console.WriteLine(j + " must come after " + i);
                    }
                }*/
                continue;
            }
            if (settingRules)
            {
                string[] rule = line.Split('|');
                int leftNum = Int32.Parse(rule[0]);
                int rightNum = Int32.Parse(rule[1]);
                if (!rules.ContainsKey(rightNum))
                {
                    //Console.WriteLine("Adding that " + rightNum + " must come after " + leftNum);
                    rules.Add(rightNum, new List<int>());
                    rules[rightNum].Add(leftNum);
                }
                else
                {
                    //Console.WriteLine("Adding that " + rightNum + " must come after " + leftNum);
                    rules[rightNum].Add(leftNum);
                }
            }
            else
            {
                result += GetCorrectedResultFromUpdate(line, rules);
            }

        }

        Console.WriteLine("End result: " + result);
    }

    /// <summary>
    /// Get the middle page value from a reordered line given a set of rules
    /// Go from RIGHT to LEFT, checking for any applicable rules. If so, add those to exclusions.
    /// </summary>
    /// <returns>0 if incorrectly-ordered. Middle page number otherwise</returns>
    public static int GetCorrectedResultFromUpdate(string line, Dictionary<int, List<int>> rules)
    {
        string[] pageNumsAsStr = line.Split(',');
        List<int> pageNums = new List<int>();
        foreach (string pageNum in pageNumsAsStr)
        {
            pageNums.Add(Int32.Parse(pageNum));
        }

        // Keys are the exclusions. Value is the number that caused that exclusion.
        Dictionary<int, List<int>> exclusions = new Dictionary<int, List<int>>();

        bool shouldReturn = false;

        //Console.WriteLine("Checking line: " + line);

        for (int i = 0; i < pageNums.Count; i++)
        {
            int num = pageNums[i];

            // If there is an incorrectly-ordered update, it is caught here, then ordered correctly.
            if (exclusions.ContainsKey(num))
            {
                shouldReturn = true;
                //Console.WriteLine(num + " in exclusions. Swapping " + num + " backwards with " + exclusions[num][0]);

                int firstVal = exclusions[num][0]; // 13
                int secondVal = num;    // 29

                pageNums[pageNums.IndexOf(firstVal)] = secondVal;
                pageNums[i] = firstVal;

                // Restart from the beginning in case another rule breaks
                exclusions.Clear();
                i = 0;
                continue;
            }

            if (rules.ContainsKey(num))
            {
                foreach (int rule in rules[num])
                {
                    //Console.WriteLine("Number " + num + " adds exclusion " + rule);
                    //exclusions.Add(rule, rules[num][0]);

                    if (!exclusions.ContainsKey(rule))
                    {
                        exclusions.Add(rule, new List<int>());
                        exclusions[rule].Add(num);
                    }
                    else
                    {
                        exclusions[rule].Add(num);
                    }
                }
            }
        }

        if (!shouldReturn)
        {
            return 0;
        }
        int middleNumber = pageNums[(pageNums.Count - 1)/2];
        //Console.WriteLine("Value number found. Returning " + middleNumber);
        return middleNumber;
    }

    public static void Main(string[] args)
    {
        //day5part1("..\\..\\..\\day5input.txt");
        //day5part2("..\\..\\..\\day5input.txt");
        day5part2_att2("..\\..\\..\\day5input.txt");
    }


    static void day5part2_att2(string path)
    {
        string[] lines = File.ReadAllLines(path);

        // Key: a number
        // Value: list of numbers that MUST come BEFORE the key
        Dictionary<int, List<int>> rules = new Dictionary<int, List<int>>();

        bool settingRules = true;
        int result = 0;

        foreach (string line in lines)
        {
            if (line == "")
            {
                settingRules = false;
                continue;
            }
            if (settingRules)
            {
                string[] rule = line.Split('|');
                int leftNum = Int32.Parse(rule[0]);
                int rightNum = Int32.Parse(rule[1]);
                if (!rules.ContainsKey(rightNum))
                {
                    //Console.WriteLine("Adding that " + rightNum + " must come after " + leftNum);
                    rules.Add(rightNum, new List<int>());
                    rules[rightNum].Add(leftNum);
                }
                else
                {
                    //Console.WriteLine("Adding that " + rightNum + " must come after " + leftNum);
                    rules[rightNum].Add(leftNum);
                }
            }
            else
            {
                result += GetCorrectedUpdateResult_att2(line, rules);
            }

        }

        Console.WriteLine("End result: " + result);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="line">Input string, a list of numbers</param>
    /// <param name="rules">Key: a number; Value: list of numbers that MUST come BEFORE the key</param>
    /// <returns>The middle value of corrected updates</returns>
    public static int GetCorrectedUpdateResult_att2(string line, Dictionary<int, List<int>> rules)
    {
        string[] pageNumsAsStr = line.Split(',');
        List<int> pageNums = new List<int>();
        foreach (string pageNum in pageNumsAsStr)
        {
            pageNums.Add(Int32.Parse(pageNum));
        }

        // Step 1: find out if it is correctly ordered
        // Early return if the update is correctly ordered
        if (IsCorrectlyOrdered(pageNums, rules))
        {
            //Console.WriteLine("Early return for line: " + line);
            return 0;
        }

        // Correct the order

        // Item1: The number that may cause an improper order
        // Item2: The (first) number to swap with
        do
        {
            Dictionary<int, int> exclusions = new Dictionary<int, int>();
            for (int i = 0; i < pageNums.Count; i++)
            {
                if (exclusions.ContainsKey(pageNums[i]))
                {
                    Swap(ref pageNums, pageNums.IndexOf(exclusions[pageNums[i]]), pageNums.IndexOf(pageNums[i]));
                    // Restart from the beginning in case another rule breaks
                    exclusions.Clear();
                    i = 0;
                    continue;
                }
                if (rules.ContainsKey(pageNums[i]))
                {
                    foreach (int rule in rules[pageNums[i]])
                    {
                        if (!exclusions.ContainsKey(rule))
                        {
                            exclusions.Add(rule, pageNums[i]);
                        }
                    }
                }
            }
        }
        while (!IsCorrectlyOrdered(pageNums, rules));

        int middleNumber = pageNums[(pageNums.Count - 1)/2];
        return middleNumber;
    }

    public static bool IsCorrectlyOrdered(List<int> update, Dictionary<int, List<int>> rules)
    {
        List<int> exclusions = new List<int>();
        foreach (int pageNum in update)
        {
            if (exclusions.Contains(pageNum))
            {
                return false;
            }
            if (rules.ContainsKey(pageNum))
            {
                // If a number is part of a rule, add all numbers which CAN'T legally be AFTER it to the exclusions
                foreach (int rule in rules[pageNum])
                {
                    exclusions.Add(rule);
                }
            }
        }
        return true;
    }

    public static void Swap(ref List<int> list, int firstIndex, int secondIndex)
    {
        int temp = list[firstIndex];
        list[firstIndex] = list[secondIndex];
        list[secondIndex] = temp;
    }
}