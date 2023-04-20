using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PrizeManager : MonoBehaviour
{
    private static PrizeManager _instance;

    public static PrizeManager Instance => _instance;

    private List<Figure> selectedFigures = new List<Figure>();

    private PrizeChecker checker = new PrizeChecker();

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
    }

    public void AddFigureToCheck(Figure fig)
    {
        selectedFigures.Add(fig);

        if (selectedFigures.Count != 15)
            return;

        List<PatternFound> patternsFound = checker.CheckPrizes(GetFigureTypes());

        StartCoroutine(ProcessPricesAndRestart(patternsFound));
    }

    private IEnumerator ProcessPricesAndRestart(List<PatternFound> patternsFound)
    {
        if (patternsFound.Count > 0)
        {
            foreach (PatternFound p in patternsFound)
            {
                for (int j = 0; j < p.length; j++)
                {
                    for (int i = 0; i < p.pattern.GetLength(0); i++)
                    {
                        if (p.pattern[i, j] == 0)
                            continue;

                        Figure fig = selectedFigures.Find(f => f.coordinates.x == j && f.coordinates.y == i);
                        Debug.Log("I have prize!: " + fig.name + " -> " + fig.transform.parent.name);
                    }
                }
                yield return new WaitForSeconds(.5f);
            }
        }
    }
    public FigureType[,] GetFigureTypes()
    {
        FigureType[,] types = new FigureType[3, 5];

        foreach (Figure fig in selectedFigures)
            types[fig.coordinates.y, fig.coordinates.x] = fig.Type;

        return types;
    }
}