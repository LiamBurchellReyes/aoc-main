namespace Day2;
public class Program
{
    static void day2part1(string path)
    {
        string[] lines = File.ReadAllLines(path);

        List<List<int>> reports = new List<List<int>>();

        foreach(string line in lines)
        {
            string[] parts = line.Split(" ");
            List<int> report = new List<int>();
            foreach(string part in parts)
            {
                report.Add(Int32.Parse(part));
            }

            reports.Add(report);
        }

        int safeReports = 0;

        foreach(List<int> report in reports)
        {
            bool isSafe = true;
            bool isIncreasing = (report[0] < report[1]);
            for(int i = 0; i < report.Count - 1; i++)
            {
                if(!safety(report[i], report[i+1], isIncreasing))
                {
                    isSafe = false;
                    break;
                }
            }
            
            if(isSafe)
            {
                safeReports++;
            }
        }

        Console.WriteLine("Number of safe reports: " + safeReports);
    }

    static void day2part2(string path)
    {
        string[] lines = File.ReadAllLines(path);

        List<List<int>> reports = new List<List<int>>();

        foreach(string line in lines)
        {
            string[] parts = line.Split(" ");
            List<int> report = new List<int>();
            foreach(string part in parts)
            {
                report.Add(Int32.Parse(part));
            }

            reports.Add(report);
        }

        int safeReports = 0;

        foreach(List<int> report in reports)
        {
            bool isSafe = true;
            bool isIncreasing = (report[0] < report[1]);
            for(int i = 0; i < report.Count - 1; i++)
            {
                if(!safety(report[i], report[i+1], isIncreasing))
                {
                    isSafe = false;
                    break;
                }
            }
            
            if(isSafe)
            {
                safeReports++;
            }

            if(!isSafe)
            {
                bool isSafeWithRemoval = removalCheck(report);
                if(isSafeWithRemoval)
                {
                    safeReports++;
                }
            }
        }

        Console.WriteLine("Number of safe reports: " + safeReports);
    }

    static bool safety(int a, int b, bool isIncreasing)
    {
        if ((isIncreasing && a >= b) || (!isIncreasing && a <= b))
        {
            return false;
        }

        if (Math.Abs(a - b) > 3)
        {
            return false;
        }

        return true;
    }

    static bool removalCheck(List<int> report)
    {
        for(int i = 0; i < report.Count; i++)
        {
            List<int> newReport = new List<int>();
            for(int j = 0; j < report.Count; j++)
            {
                if (i != j)
                {
                    newReport.Add(report[j]);
                }
            }

            bool isSafe = true;
            bool isIncreasing = (newReport[0] < newReport[1]);
            for(int k = 0; k < newReport.Count - 1; k++)
            {
                if(!safety(newReport[k], newReport[k+1], isIncreasing))
                {
                    isSafe = false;
                    break;
                }
            }

            if (isSafe)
            {
                return true;
            }
        }
        return false;
    }

    public static void Main(string[] args)
    {
        //day2part1("day2testinput.txt");
        day2part2("day2input.txt");
    }
}