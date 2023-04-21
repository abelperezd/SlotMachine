using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PrizeManager : MonoBehaviour
{
    #region Fields and properties

    [Range(.1f, 2)]
    [SerializeField] private float _prizeAnimationDuration = 1;

    public static PrizeManager Instance => _instance;
    private static PrizeManager _instance;

    private List<Figure> _selectedFigures = new List<Figure>();

    private PrizeChecker _checker = new PrizeChecker();

    #endregion

    #region Events

    internal event Action<int, float> OnShowPrize;

    #endregion

    #region Unity callbacks

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
    }

    #endregion

    #region Methods

    /// <summary> Add a visible figure to latter on check the patterns </summary>
    public void AddFigureToCheck(Figure fig)
    {
        _selectedFigures.Add(fig);

        if (_selectedFigures.Count != 15)
            return;

        //if we have all the visible figures, get their type and get the patterns found
        List<PatternFound> patternsFound = _checker.CheckPrizes(GetFigureTypes());

        StartCoroutine(ProcessPricesAndRestart(patternsFound));
    }

    /// <summary> Play an aimation for all the found patterns, add the prize and restar the game </summary>
    private IEnumerator ProcessPricesAndRestart(List<PatternFound> patternsFound)
    {
        int totalPrize = 0; //prize of all the found patterns

        if (patternsFound.Count > 0)
        {
            foreach (PatternFound p in patternsFound)
            {
                AudioManager.Instance.PlayPrizeSound();
                //for each pattern found, get the figures and play an animation on them
                for (int j = 0; j < p.length; j++)
                {
                    for (int i = 0; i < p.pattern.GetLength(0); i++)
                    {
                        if (p.pattern[i, j] == 0)
                            continue;

                        //get the prized figure and play an animation
                        Figure fig = _selectedFigures.Find(f => f.Coordinates.x == j && f.Coordinates.y == i);
                        fig.PlayPrizeAnimation(1 / _prizeAnimationDuration);

                        //Debug.Log("I have prize!: " + fig.name + " -> " + fig.transform.parent.name);
                    }
                }

                Figure.UpdateAnimation();

                totalPrize += p.prize;
                //play the prize animation
                OnShowPrize?.Invoke(p.prize, 1 / _prizeAnimationDuration);

                yield return new WaitForSeconds(_prizeAnimationDuration);
            }
        }

        _selectedFigures.Clear();

        //all the animations played -> tell the manager that we can restart
        GameManager.Instance.GameFinished(totalPrize);
    }

    /// <summary> Create a matrix with the figureType in each coordinate </summary>
    public FigureType[,] GetFigureTypes()
    {
        FigureType[,] types = new FigureType[3, 5];

        foreach (Figure fig in _selectedFigures)
            types[fig.Coordinates.y, fig.Coordinates.x] = fig.Type;

        return types;
    }

    #endregion
}