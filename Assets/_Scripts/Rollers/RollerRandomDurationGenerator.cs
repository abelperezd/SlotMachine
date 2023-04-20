using System;
using UnityEngine;

/// <summary> Get the duration for the rollers </summary>
[Serializable]
public class RollerRandomDurationGenerator
{
    #region Fields

    [Range(.5f, 10)]
    [SerializeField] private float _minSpinningDuration = 2;

    [Range(.5f, 10)]
    [SerializeField] private float _maxSpinningDuration = 4;

    [Range(0, 1)]
    [Tooltip("Max duration difference there can be between 2 consecutive rollers. " +
        "It will be clamped with the Delay Between Rollers (RollManager)")]
    [SerializeField] private float _maxSpinningDurationDelayBetweenConsecutiveRollers = .1f;

    #endregion

    #region Methods

    /// <summary> For the next play, get the minimum duration each roller will have </summary>
    internal float GetSpinBaseDuration()
    {
        return UnityEngine.Random.Range(_minSpinningDuration, _maxSpinningDuration);
    }

    /// <summary> Add an extra random duration to a roller to create randomness </summary>
    internal float GetExtraRandomDuration(float dur, int spinVelocity, float delayBetweenRollers)
    {
        //clamp to avoid latter rollers from finishing before a previous one
        dur += Mathf.Clamp(UnityEngine.Random.Range(0, _maxSpinningDurationDelayBetweenConsecutiveRollers), 0, delayBetweenRollers);

        //recompute the duration to be sure it will finish in a correct position.
        int spins = Mathf.RoundToInt(spinVelocity * dur);
        return (float)spins / spinVelocity;
    }

    #endregion
}
