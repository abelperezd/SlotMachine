using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roller : MonoBehaviour
{
    #region Fields and properties

    [SerializeField] private List<FigureType> figures;
    
    internal int Position => _position; //roller position
    [Range(0,4)]
    [SerializeField] private int _position = 0;

    [SerializeField] private float _yRestartPosition = .75f;

    private Vector3 _startPosition;

    #endregion

    #region Events

    internal event Action OnRollerStopped;

    #endregion

    #region Unity callbacks

    private void Start()
    {
        SpawnFigures();
        InitializePosition();
    }

    #endregion

    #region Methods

    /// <summary> Compute the reset position to do the "infinite" trick </summary>
    private void InitializePosition()
    {
        float y = figures.Count * Figure.FIGURE_SIZE + _yRestartPosition;
        _startPosition = new Vector3(transform.position.x, y, 0);
    }

    private void SpawnFigures()
    {
        float spawnPos = Figure.FIGURE_SIZE;

        for (int i = 0; i < figures.Count; i++)
        {
            Figure fig = FigureFactory.Instance.BuildFigure(figures[i], transform);
            fig.transform.localPosition = new Vector3(0, spawnPos, 0);
            fig.name = fig.name + i;
            spawnPos -= Figure.FIGURE_SIZE;
        }

        //To make the "infinite" trick
        for (int i = 0; i < 3; i++)
        {
            Figure fig = FigureFactory.Instance.BuildFigure(figures[i], transform);
            fig.transform.localPosition = new Vector3(0, spawnPos, 0);
            fig.name = fig.name + (figures.Count + i);
            spawnPos -= Figure.FIGURE_SIZE;
        }
    }

    public void StartSpinning(SpinningConfiguration config)
    {
        StartCoroutine(Spin(config));
    }

    IEnumerator Spin(SpinningConfiguration config)
    {
        TryRestartLoop();

        int extraSubSteps = 1; //to do the slow down

        for (int j = 0; j < config.stepsToDo; j++)
        {
            for (int i = 0; i < extraSubSteps; i++)
            {
                TryRestartLoop();
                transform.position -= Vector3.up * config.velocity / extraSubSteps;
                yield return new WaitForSeconds(config.duration / config.stepsToDo / extraSubSteps);
            }
            //if we entered the slowDown steps, for each complete figure add an extra substep to slow it down
            if (j > (config.stepsToDo - config.slowDownSteps) && j % config.visualizationFrequency == 0)
                extraSubSteps += config.extraSubstepsFactor;
        }

        OnRollerStopped?.Invoke();
    }

    /// <summary> Check if we arreived to the end of the roll and a restart is needed </summary>
    private void TryRestartLoop()
    {
        //The second part (Mathf.abs..) is to respect the position if we passed the exact position because of the
        //step size
        if (transform.position.y <= _yRestartPosition)
            transform.position = _startPosition - Vector3.up * (Mathf.Abs(transform.position.y - _yRestartPosition));
    }

    #endregion
}
