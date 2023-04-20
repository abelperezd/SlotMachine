using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private static GameManager _instance;
    public static GameManager Instance => _instance;

    internal event Action OnGameStarted;
    internal event Action OnGameFinished;

    [SerializeField] private int _credit = 50;
    public int Credit => _credit;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
    }

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
}
