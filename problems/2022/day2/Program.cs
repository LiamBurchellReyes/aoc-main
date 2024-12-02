using System;
using System.Collections.Generic;
using System.IO;

/*
--- Day 2: Rock Paper Scissors ---
The Elves begin to set up camp on the beach. To decide whose tent gets to be closest to the snack storage, a giant Rock Paper Scissors tournament is already in progress.

Rock Paper Scissors is a game between two players.
Each game contains many rounds; in each round, the players each simultaneously choose one of Rock, Paper, or Scissors using a hand shape.
Then, a winner for that round is selected: Rock defeats Scissors, Scissors defeats Paper, and Paper defeats Rock.
If both players choose the same shape, the round instead ends in a draw.

Appreciative of your help yesterday, one Elf gives you an encrypted strategy guide (your puzzle input) that they say will be sure to help you win.
"The first column is what your opponent is going to play: A for Rock, B for Paper, and C for Scissors. The second column--"
Suddenly, the Elf is called away to help with someone's tent.

The second column, you reason, must be what you should play in response: X for Rock, Y for Paper, and Z for Scissors.
Winning every time would be suspicious, so the responses must have been carefully chosen.

The winner of the whole tournament is the player with the highest score.
Your total score is the sum of your scores for each round.
The score for a single round is the score for the shape you selected (1 for Rock, 2 for Paper, and 3 for Scissors)
    plus the score for the outcome of the round (0 if you lost, 3 if the round was a draw, and 6 if you won).

Since you can't be sure if the Elf is trying to help you or trick you, you should calculate the score you would get if you were to follow the strategy guide.

For example, suppose you were given the following strategy guide:

A Y
B X
C Z
This strategy guide predicts and recommends the following:

In the first round, your opponent will choose Rock (A), and you should choose Paper (Y).
    This ends in a win for you with a score of 8 (2 because you chose Paper + 6 because you won).
In the second round, your opponent will choose Paper (B), and you should choose Rock (X).
    This ends in a loss for you with a score of 1 (1 + 0).
The third round is a draw with both players choosing Scissors, giving you a score of 3 + 3 = 6.
In this example, if you were to follow the strategy guide, you would get a total score of 15 (8 + 1 + 6).

What would your total score be if everything goes exactly according to your strategy guide?

--- Part Two ---
The Elf finishes helping with the tent and sneaks back over to you.
"Anyway, the second column says how the round needs to end: X means you need to lose, Y means you need to end the round in a draw, and Z means you need to win. Good luck!"

The total score is still calculated in the same way, but now you need to figure out what shape to choose so the round ends as indicated. The example above now goes like this:

In the first round, your opponent will choose Rock (A), and you need the round to end in a draw (Y), so you also choose Rock. This gives you a score of 1 + 3 = 4.
In the second round, your opponent will choose Paper (B), and you choose Rock so you lose (X) with a score of 1 + 0 = 1.
In the third round, you will defeat your opponent's Scissors with Rock for a score of 1 + 6 = 7.
Now that you're correctly decrypting the ultra top secret strategy guide, you would get a total score of 12.

Following the Elf's instructions for the second column, what would your total score be if everything goes exactly according to your strategy guide?
*/
public class Program
{
    public enum RPS
    {
        None = 0,
        Rock = 1,
        Paper = 2,
        Scissors = 3
    }
    public static void d2(string path)
    {
        string[] file = File.ReadAllLines(path);

        int score = 0;
        foreach(string line in file)
        {
            string[] command = line.Split(" ");
            score += ProcessCommand(command[0][0], command[1][0]);
        }

        Console.WriteLine($"score: {score}");
    }

    public static int ProcessCommand(char input, char response)
    {
        RPS opponent = RPS.None;
        RPS ally = RPS.None;
        switch (input)
        {
            case 'A':
                opponent = RPS.Rock;
                break;
            case 'B':
                opponent = RPS.Paper;
                break;
            case 'C':
                opponent = RPS.Scissors;
                break;
            default:
                break;
        }

        switch (response)
        {
            case 'X':
                ally = RPS.Rock;
                break;
            case 'Y':
                ally = RPS.Paper;
                break;
            case 'Z':
                ally = RPS.Scissors;
                break;
            default:
                break;
        }

        if (ally == RPS.None || opponent == RPS.None)
        {
            Console.WriteLine("Invalid characters received!");
        }

        int score = (int) ally;

        if (ally == opponent)
        {
            // Draw
            score += 3;
        }
        if ((ally == RPS.Rock && opponent == RPS.Scissors)
            || (ally == RPS.Paper && opponent == RPS.Rock)
            || (ally == RPS.Scissors && opponent == RPS.Paper))
        {
            // Victory
            score += 6;
        }
        // Return score
        return score;
    }

    public static void d2p2(string path)
    {
        string[] file = File.ReadAllLines(path);

        int score = 0;
        foreach(string line in file)
        {
            string[] command = line.Split(" ");
            score += ProcessCommand2(command[0][0], command[1][0]);
        }

        Console.WriteLine($"score: {score}");
    }

    public static int ProcessCommand2(char input, char response)
    {
        RPS opponent = RPS.None;
        RPS ally = RPS.None;
        switch (input)
        {
            case 'A':
                opponent = RPS.Rock;
                break;
            case 'B':
                opponent = RPS.Paper;
                break;
            case 'C':
                opponent = RPS.Scissors;
                break;
            default:
                break;
        }

        switch (response)
        {
            case 'X':
                ally = GetLosingOption(opponent);
                break;
            case 'Y':
                ally = GetDrawingOption(opponent);
                break;
            case 'Z':
                ally = GetWinningOption(opponent);
                break;
            default:
                break;
        }

        if (ally == RPS.None || opponent == RPS.None)
        {
            Console.WriteLine("Invalid characters received!");
        }

        int score = (int) ally;

        if (ally == opponent)
        {
            // Draw
            score += 3;
        }
        if ((ally == RPS.Rock && opponent == RPS.Scissors)
            || (ally == RPS.Paper && opponent == RPS.Rock)
            || (ally == RPS.Scissors && opponent == RPS.Paper))
        {
            // Victory
            score += 6;
        }
        // Return score
        return score;
    }

    public static RPS GetLosingOption(RPS input)
    {
        RPS output = RPS.None;
        switch (input)
        {
            case RPS.Paper:
                output = RPS.Rock;
                break;
            case RPS.Scissors:
                output = RPS.Paper;
                break;
            case RPS.Rock:
                output = RPS.Scissors;
                break;
            default:
                break;
        }
        return output;
    }
    public static RPS GetDrawingOption(RPS input)
    {
        return input;
    }
    public static RPS GetWinningOption(RPS input)
    {
        RPS output = RPS.None;
        switch (input)
        {
            case RPS.Paper:
                output = RPS.Scissors;
                break;
            case RPS.Scissors:
                output = RPS.Rock;
                break;
            case RPS.Rock:
                output = RPS.Paper;
                break;
            default:
                break;
        }
        return output;
    }
    
    public static void Main()
    {
        d2p2("day2input.txt");
    }
}