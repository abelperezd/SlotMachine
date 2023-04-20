using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roller : MonoBehaviour
{
    [SerializeField]
    private int _position = 0;

    public int Position => _position;

    private Vector3 _startPosition;
    [SerializeField] private float _yRestartPosition = .75f;

    internal event Action OnRollerStopped;

    private void Awake()
    {
        _startPosition = transform.position;
    }


    bool countTime = false;
    float timerCount = 0;
    void Update()
    {
        if (countTime)
            timerCount += Time.deltaTime;

    }

    public void StartSpinning(SpinningConfiguration config)
    {
        StartCoroutine(Spin(config));
    }

    IEnumerator Spin(SpinningConfiguration config)
    {
        yield return null;
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
            if (j > (config.stepsToDo - config.slowDownSteps) && j%config.visualizationFrequency == 0)
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
