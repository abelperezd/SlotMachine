using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Fields and properties

    public int Credit => _credit;
    [Range(1,50)]
    [SerializeField] private int _credit = 50;

    public static GameManager Instance => _instance;
    private static GameManager _instance;

    #endregion

    #region Events

    internal event Action OnGameStarted;
    internal event Action OnGameFinished;

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

    public void StartGame()
    {
        if (Credit <= 0)
            return;

        _credit--;
        OnGameStarted?.Invoke();
    }

    public void GameFinished(int prize)
    {
        _credit += prize;
        OnGameFinished?.Invoke();
    }

    #endregion
}
