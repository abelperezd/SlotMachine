using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PatternFound
{
    public int[,] pattern;
    public int length;
    public int prize;

    public PatternFound(int[,] pattern, int length, int prize)
    {
        this.pattern = pattern;
        this.length = length;
        this.prize = prize;
    }
}

public class PrizeChecker
{

    private List<int[,]> winnerPatterns = new List<int[,]>
    {
        /* -- MODEL --
        new int[3, 5] {
            { 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0 }
        }
        */
        new int[3, 5] {
            { 1, 1, 1, 1, 1 },
            { 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0 }
        },
        new int[3, 5] {
            { 0, 0, 0, 0, 0 },
            { 1, 1, 1, 1, 1 },
            { 0, 0, 0, 0, 0 }
        },
        new int[3, 5] {
            { 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0 },
            { 1, 1, 1, 1, 1 }
        }
    };

    Dictionary<FigureType, int[]> prizes = new Dictionary<FigureType, int[]>()
    {
        /* -- MODEL --
        { FigureType., new int[] { , , , } }
        */
        { FigureType.bell, new int[] { 25, 50, 75, 100 } },
        { FigureType.aubergine, new int[] { 5, 10, 20, 40 } },
        { FigureType.cherry, new int[] { 1, 2, 5, 10 } },
        { FigureType.watermelon, new int[] { 10, 20, 30, 60 } },
        { FigureType.orange, new int[] { 5, 10, 20, 30 } },
        { FigureType.grape, new int[] { 5, 10, 20, 50 } },
        { FigureType.lemon, new int[] { 2, 5, 10, 20 } }
    };

    public List<PatternFound> CheckPrizes(FigureType[,] figures)
    {

        //pattern, length   
        List<PatternFound> patternsFound = new List<PatternFound>();


        foreach (int[,] pattern in winnerPatterns)
        {
            (FigureType typeFound, int lengthFound) = CheckPattern(figures, pattern);

            if (lengthFound < 2)
                continue;

            int[] prizesForType;
            prizes.TryGetValue(typeFound, out prizesForType);

            patternsFound.Add(new PatternFound(pattern, lengthFound, prizesForType[lengthFound - 2]));

        }

        return patternsFound;
    }

    private (FigureType, int) CheckPattern(FigureType[,] figures, int[,] pattern)
    {
        bool patternBroken = false;

        int length = 0;
        FigureType typeChecked = 0;

        for (int j = 0; j < pattern.GetLength(1); j++)
        {
            for (int i = 0; i < pattern.GetLength(0); i++)
            {
                if (pattern[i, j] == 0)
                    continue;

                if (j == 0)
                    typeChecked = figures[i, j];

                if (typeChecked == figures[i, j])
                {
                    length++;
                    break;
                }

                patternBroken = true;
                break;
            }
            if (patternBroken)
                break;
        }
        return (typeChecked, length);
    }

}
