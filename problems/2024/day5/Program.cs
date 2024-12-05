using System.Reflection.Metadata.Ecma335;
using System.Xml.XPath;

namespace Day4;
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

        Console.WriteLine("Checking line: " + line);

        for (int i = 0; i < pageNums.Count; i++)
        {
            int num = pageNums[i];

            if (exclusions.ContainsKey(num))
            {
                shouldReturn = true;
                Console.WriteLine(num + " in exclusions. Swapping " + num + " backwards with " + exclusions[num][0]);
                // At 13, we add 29 to exclusions.
                // Now we're at 29. num = 29
                // exclusions[num] = 13
                int firstVal = exclusions[num][0]; // 13
                int secondVal = num;    // 29

                // We're at 29. We need to move 29 before 13.
                // pageNums.IndexOf(firstVal) = 1
                pageNums[pageNums.IndexOf(firstVal)] = secondVal;
                pageNums[i] = firstVal;
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
                        exclusions[rule].Add(rules[num][0]);
                    }
                    else
                    {
                        exclusions[rule].Add(rules[num][0]);
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
        day5part2("..\\..\\..\\day5testinput.txt");
    }
}