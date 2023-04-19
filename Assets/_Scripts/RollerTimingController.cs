using System;
using UnityEngine;

[Serializable]
public class RollerTimingController
{
    [Tooltip("Figures/second")]
    [SerializeField] private int _spinVelocity = 8;

    [Tooltip("Seconds")]
    [SerializeField] private float _delayBetweenRollers = .5f;


    [SerializeField] private float _minSpinningDuration = 2;

    [SerializeField] private float _maxSpinningDuration = 4;
}
