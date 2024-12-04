using System.Reflection.Metadata.Ecma335;

namespace Day3;
public class Program
{
    static void day3part1(string path)
    {
        string[] lines = File.ReadAllLines(path);

        int result = 0;

        foreach(string line in lines)
        {
            string remainingLine = line;
            while(remainingLine.Length > 0)
            {
                string? nextMulInstruction;
                (remainingLine, nextMulInstruction) = GetNextMulInstruction(remainingLine);

                if (nextMulInstruction is not null)
                {
                    result += PerformMulInstruction(nextMulInstruction);
                }
            }
            //Console.WriteLine("Line read. Result: " + result);
        }

        Console.WriteLine("End result: " + result);
    }

    public static (string, string?) GetNextMulInstruction(string line)
    {
        int index = line.IndexOf("mul(");
        int closingIndex = line.IndexOf(')');
        if (index == -1 || closingIndex == -1)
        {
            //Console.WriteLine("None found in line: " + line);
            return ("", null);
        }

        if (closingIndex < index)
        {
            //Console.WriteLine("')' found too early in line: " + line);
            return (line.Substring(closingIndex + 1), null);
        }

        string toParse = line.Substring(index + 4, (closingIndex - index - 4));
        //Console.WriteLine("Mul found: " + toParse);
        string[] arguments = toParse.Split(',');

        if (arguments.Length != 2)
        {
            //Console.WriteLine("Invalid - arguments: " + arguments);
            return (line.Substring(index + 1), null);
        }

        string? arg = arguments[0];
        string? arg2 = arguments[1];
        int val1;
        int val2;
        if (Int32.TryParse(arg, out val1) && Int32.TryParse(arg2, out val2))
        {
            return (line.Substring(index + 1), toParse);
        }

        return (line.Substring(index + 1), null);
    }

    public static int PerformMulInstruction(string mul)
    {
        string[] args = mul.Split(',');
        int arg1 = Int32.Parse(args[0]);
        int arg2 = Int32.Parse(args[1]);

        if (arg1 < -999 || arg1 > 999 || arg2 < -999 || arg2 > 999)
        {
            Console.WriteLine("Numbers " + arg1 + " and " + arg2 + " invalid due to length");
            return 0;
        }
        return arg1 * arg2;
    }

    static void day3part2(string path)
    {
        string[] lines = File.ReadAllLines(path);

        int result = 0;
        bool enabled = true;

        foreach(string line in lines)
        {
            string remainingLine = line;
            while(remainingLine.Length > 0)
            {
                if (!enabled)
                {
                    int doIndex = remainingLine.IndexOf("do()");
                    if (doIndex != -1)
                    {
                        enabled = true;
                        remainingLine = remainingLine.Substring(doIndex + 4);
                        Console.WriteLine("DO hit. New line: " + remainingLine);
                    }
                    else
                    {
                        remainingLine = "";
                    }
                }

                int dontIndex = remainingLine.IndexOf("don't()");
                int mulIndex = remainingLine.IndexOf("mul(");

                if (dontIndex != -1 && mulIndex != -1 && dontIndex < mulIndex)
                {
                    enabled = false;
                    Console.WriteLine("DONT hit with line: " + remainingLine);
                    continue;
                }

                string? nextMulInstruction;
                (remainingLine, nextMulInstruction) = GetNextMulInstruction(remainingLine);

                if (nextMulInstruction is not null)
                {
                    result += PerformMulInstruction(nextMulInstruction);
                }
            }
            //Console.WriteLine("Line read. Result: " + result);
        }

        Console.WriteLine("End result: " + result);
    }

    public static void Main(string[] args)
    {
        //day3part1("..\\..\\..\\day3input.txt");
        day3part2("..\\..\\..\\day3input.txt");
    }
}