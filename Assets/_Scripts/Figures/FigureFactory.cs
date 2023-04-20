using System.Collections.Generic;
using UnityEngine;

/// <summary> To buil the figures </summary>
public class FigureFactory : MonoBehaviour
{
    #region Fields

    private static FigureFactory _instance;

    [SerializeField] private List<Figure> _figurePrefabs;

    [SerializeField] private List<FigureType> _figureTypes;

    private Dictionary<FigureType, Figure> _figureDict = new Dictionary<FigureType, Figure>();
    
    public static FigureFactory Instance => _instance;

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

        InitializeDict();
    }

    #endregion

    #region Methods

    private void InitializeDict()
    {
        for (int i = 0; i < _figurePrefabs.Count; i++)
        {
            _figureDict.Add(_figureTypes[i], _figurePrefabs[i]);
        }
    }

    public Figure BuildFigure(FigureType type, Transform parent)
    {
        Figure prefab;
        _figureDict.TryGetValue(type, out prefab);

        Figure newInstance = Instantiate(prefab, parent);
        return newInstance;
    }

    #endregion
}
