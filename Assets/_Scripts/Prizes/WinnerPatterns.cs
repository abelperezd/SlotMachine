using System.Collections.Generic;

/// <summary> Winner patterns and prizes </summary>
public static class WinnerPatterns
{
    internal static Dictionary<FigureType, int[]> Prizes => _prizes;
    private static Dictionary<FigureType, int[]> _prizes = new Dictionary<FigureType, int[]>()
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

    internal static List<int[,]> Patterns => _patterns;
    private static List<int[,]> _patterns = new List<int[,]>
    {
        /* -- MODEL --
        new int[3, 5] {
            { 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0 }
        },
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
        },
        new int[3, 5] {
            { 0, 1, 0, 1, 0 },
            { 0, 0, 0, 0, 0 },
            { 1, 0, 1, 0, 1 }
        },
        new int[3, 5] {
            { 1, 0, 1, 0, 1 },
            { 0, 0, 0, 0, 0 },
            { 0, 1, 0, 1, 0 }
        },
        new int[3, 5] {
            { 1, 0, 0, 0, 1 },
            { 0, 1, 0, 1, 0 },
            { 0, 0, 1, 0, 0 }
        },
        new int[3, 5] {
            { 0, 0, 1, 0, 0 },
            { 0, 1, 0, 1, 0 },
            { 1, 0, 0, 0, 1 }
        },
        new int[3, 5] {
            { 1, 1, 0, 0, 0 },
            { 0, 0, 1, 0, 0 },
            { 0, 0, 0, 1, 1 }
        },
        new int[3, 5] {
            { 0, 0, 0, 1, 1 },
            { 0, 0, 1, 0, 0 },
            { 1, 1, 0, 0, 0 }
        },
    };
}
