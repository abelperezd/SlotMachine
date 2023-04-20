using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureFactory : MonoBehaviour
{
    private static FigureFactory _instance;

    [SerializeField] private List<Figure> figurePrefabs;

    [SerializeField] private List<FigureType> figureTypes;

    private Dictionary<FigureType, Figure> figureDict = new Dictionary<FigureType, Figure>();

    public static FigureFactory Instance => _instance;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;

        InitializeDict();
    }

    private void InitializeDict()
    {
        for (int i = 0; i < figurePrefabs.Count; i++)
        {
            figureDict.Add(figureTypes[i], figurePrefabs[i]);
        }
    }

    public Figure BuildFigure(FigureType type, Transform parent)
    {
        Figure prefab;
        figureDict.TryGetValue(type, out prefab);

        Figure newInstance = Instantiate(prefab, parent);
        return newInstance;
    }
}
