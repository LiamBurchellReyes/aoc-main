namespace Day1;
public class Program
{
    static void day1part1(string path)
    {
        string[] lines = File.ReadAllLines(path);

        List<int> leftList = new List<int>();
        List<int> rightList = new List<int>();

        foreach(string line in lines)
        {
            string[] parts = line.Split("   ");
            leftList.Add(Int32.Parse(parts[0]));
            rightList.Add(Int32.Parse(parts[1]));
        }

        leftList.Sort();
        rightList.Sort();

        int totalDistance = 0;

        for (int i = 0; i < leftList.Count; i++)
        {
            int distance = Math.Abs(leftList[i] - rightList[i]);
            totalDistance += distance;
        }

        Console.WriteLine("Total distance: " + totalDistance);
    }

    static void day1part2(string path)
    {
        string[] lines = File.ReadAllLines(path);

        List<int> leftList = new List<int>();
        Dictionary<int, int> rightDict = new Dictionary<int, int>();

        foreach(string line in lines)
        {
            string[] parts = line.Split("   ");
            leftList.Add(Int32.Parse(parts[0]));
            if (rightDict.ContainsKey(Int32.Parse(parts[1])))
            {
                rightDict[Int32.Parse(parts[1])]++;
            }
            else
            {
                rightDict.Add(Int32.Parse(parts[1]), 1);
            }
        }

        /*foreach(KeyValuePair<int, int> kvp in rightDict)
        {
            Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
        }*/

        int similarityScore = 0;

        for (int i = 0; i < leftList.Count; i++)
        {
            if (!rightDict.ContainsKey(leftList[i]))
            {
                //Console.WriteLine("Skipping key " + leftList[i]);
                continue;
            }
            int similarity = leftList[i] * rightDict[leftList[i]];

            //Console.WriteLine("Key: " + leftList[i] + " found " + rightDict[leftList[i]] + " times in right list");
            similarityScore += similarity;
        }

        Console.WriteLine("Total similarity score: " + similarityScore);
    }


    public static void Main(string[] args)
    {
        //day1part1("day1input.txt");
        day1part2("day1input.txt");
    }
}