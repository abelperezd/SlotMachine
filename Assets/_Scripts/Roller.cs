using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roller : MonoBehaviour
{

    private Vector3 _startPosition;
    [SerializeField] private float _yRestartPosition = .75f;

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

    public void StartSpinning(int spins, float duration)
    {
        StartCoroutine(Spin(spins, duration));
    }

    public int stepsPerFigure;
    IEnumerator Spin(int spinsssss, float durationn)
    {
        float figureSize = 2.2f; // units/fig

        float duration = 5; //  sec

        int vel = 8; // fig/sec

        int figuresToSee = (int)(vel * duration); //    figs

        //int stepsPerFigure = 22; //  steps/figure

        int stepsToDo = figuresToSee * stepsPerFigure; //   steps

        float velInUnitsPerStep = vel * (float)figureSize * (float)duration / (float)stepsToDo; // units/step

        countTime = true;

        float slowDownPercentage = 0.2f;

        int slowDownFigures = (int)Mathf.Ceil(vel * slowDownPercentage);

        int lastSteps = slowDownFigures * stepsPerFigure;

        int extraSubSteps = 1;

        int extraSubstepsFactor = vel / slowDownFigures;


        for (int j = 0; j < stepsToDo; j++)
        {
            for (int i = 0; i < extraSubSteps; i++)
            {
                TryRestart();
                transform.position -= Vector3.up * velInUnitsPerStep / extraSubSteps;
                //yield return null;
                yield return new WaitForSeconds(duration / stepsToDo / extraSubSteps);
            }
            if (j > (stepsToDo - lastSteps))
                extraSubSteps += extraSubstepsFactor;
        }
        countTime = false;
    }

    private void TryRestart()
    {
        if (transform.position.y <= _yRestartPosition)
            transform.position = _startPosition - Vector3.up * (Mathf.Abs(transform.position.y - _yRestartPosition));
    }
}
