using System.Collections.Generic;

/// <summary> Properties of a found pattern </summary>
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

/// <summary> Check if there are patterns and compute the prize </summary>
public class PrizeChecker
{
    /// <summary> Go through all the prized patterns and try to find coincidences</summary>
    public List<PatternFound> CheckPrizes(FigureType[,] figures)
    {
        List<PatternFound> patternsFound = new List<PatternFound>();

        for (int i = 0; i < WinnerPatterns.Patterns.Count; i++)
        {
            (FigureType typeFound, int lengthFound) = CheckPattern(figures, i);

            if (lengthFound < 2)
                continue;

            //get all the prices that a figure has
            int[] prizesForType;
            WinnerPatterns.Prizes.TryGetValue(typeFound, out prizesForType);

            patternsFound.Add(new PatternFound(WinnerPatterns.Patterns[i], lengthFound, prizesForType[lengthFound - 2]));
        }

        return patternsFound;
    }

    /// <summary> Try to find a given pattern </summary>
    private (FigureType, int) CheckPattern(FigureType[,] figures, int patternIndex)
    {
        int[,] pattern = WinnerPatterns.Patterns[patternIndex];

        bool patternBroken = false;

        int length = 0; //length of the pattern found

        FigureType typeChecked = 0; //type we are looking for in the pattern

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

                //if there is not a match in the type, stop exploring
                patternBroken = true;
                break;
            }
            if (patternBroken)
                break;
        }

        return (typeChecked, length);
    }

}
