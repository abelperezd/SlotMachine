using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roller : MonoBehaviour
{
    [SerializeField]
    private int _position = 0;

    [SerializeField] List<FigureType> figures;

    public int Position => _position;

    public  Vector3 _startPosition;
    [SerializeField] private float _yRestartPosition = .75f;

    internal event Action OnRollerStopped;

    private void Awake()
    {
        SpawnFigures();
        InitializePosition();
    }

    private void InitializePosition()
    {
        float y = figures.Count * RollerManager.FIGURE_SIZE + _yRestartPosition;
        _startPosition = new Vector3(transform.position.x, y, 0);
    }

    private void SpawnFigures()
    {
        float figureSize = RollerManager.FIGURE_SIZE;

        float spawnPos = figureSize;

        for (int i = 0; i < figures.Count; i++)
        {
            FigureFactory.Instance.BuildFigure(figures[i], transform).transform.localPosition = new Vector3(0,spawnPos,0);
            spawnPos -= figureSize;
        }

        //To make the repetition trick
        for (int i = 0; i < 3; i++)
        {
            FigureFactory.Instance.BuildFigure(figures[i], transform).transform.localPosition = new Vector3(0, spawnPos, 0);
            spawnPos -= figureSize;
        }
    }

    public void StartSpinning(SpinningConfiguration config)
    {
        StartCoroutine(Spin(config));
    }

    IEnumerator Spin(SpinningConfiguration config)
    {
        TryRestart();
        int extraSubSteps = 1;

        for (int j = 0; j < config.stepsToDo; j++)
        {
            for (int i = 0; i < extraSubSteps; i++)
            {
                TryRestart();
                transform.position -= Vector3.up * config.velInUnitsPerStep / extraSubSteps;
                //yield return null;
                yield return new WaitForSeconds(config.duration / config.stepsToDo / extraSubSteps);
            }
            if (j > (config.stepsToDo - config.slowDownSteps) && j % config.visualizationFrequency == 0)
                extraSubSteps += config.extraSubstepsFactor;
        }
        OnRollerStopped?.Invoke();
    }

    private void TryRestart()
    {
        if (transform.position.y <= _yRestartPosition)
            transform.position = _startPosition - Vector3.up * (Mathf.Abs(transform.position.y - _yRestartPosition));
    }
}
