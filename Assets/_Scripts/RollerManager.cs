using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SpinningConfiguration
{
    public int stepsToDo;
    public float velInUnitsPerStep;
    public float duration;
    public int slowDownSteps;
    public int extraSubstepsFactor;
    public int visualizationFrequency;
}

public class RollerManager : MonoBehaviour
{
    // units/figure
    private const float FIGURE_SIZE = 2.2f;

    [SerializeField] private List<Roller> _rollers;

    [Tooltip("Figures/second")]
    [SerializeField] private int _spinVelocity = 8;

    [Tooltip("Seconds")]
    [SerializeField] private float _delayBetweenRollers = .5f;


    [SerializeField] private float _minSpinningDuration = 2;

    [SerializeField] private float _maxSpinningDuration = 4;

    [Range(0, 1)]
    [Tooltip("Max duration difference there can be between 2 consecutive rollers. " +
        "It will be clamped with the Delay Between Rollers")]
    [SerializeField] private float _maxSpinningDurationDelayBetweenConsecutiveRollers = .1f;

    [Range(0, 1)]
    [Tooltip("Percentage of the total time that the roller will be slowing down")]
    [SerializeField] private float _slowDownPercentage = .2f;

    [Tooltip("Steps we do for every figure")]
    [SerializeField] private int _visualizationFrequency = 8;

    private void StartSpinningRollers()
    {
        StartCoroutine(nameof(SpinRollers));
    }

    IEnumerator SpinRollers()
    {
        foreach (Roller r in _rollers)
        {
            int spins;
            float duration;
            SpinningConfiguration configuration = GetSpinningConfiguration(GetSpinDuration());
            r.StartSpinning(configuration);
            yield return new WaitForSeconds(_delayBetweenRollers);
        }
    }

    private void Start()
    {
        //Debug.Log("Slow down duration: " + SlowDownDuration);
        StartSpinningRollers();
    }

    private float GetSpinDuration()
    {
        float randomDuration = Random.Range(_minSpinningDuration, _maxSpinningDuration) +
            Mathf.Clamp(Random.Range(0, _maxSpinningDurationDelayBetweenConsecutiveRollers), 0, _delayBetweenRollers);
        //Debug.Log("randomDuration: " + randomDuration);
        int spins = Mathf.RoundToInt(_spinVelocity * randomDuration);
        return (float)spins / _spinVelocity;
    }

    private SpinningConfiguration GetSpinningConfiguration(float spinDuration)
    {

        int figuresToSee = (int)(_spinVelocity * spinDuration); //    figs

        int stepsToDo = figuresToSee * _visualizationFrequency; //   steps

        float velInUnitsPerStep = _spinVelocity * FIGURE_SIZE * spinDuration / (float)stepsToDo; // units/step

        int slowDownFigures = (int)Mathf.Ceil(figuresToSee * _slowDownPercentage);

        int slowDownSteps = slowDownFigures * _visualizationFrequency;

        int extraSubstepsFactor = _spinVelocity / slowDownFigures;

        return new SpinningConfiguration
        {
            stepsToDo = stepsToDo,
            velInUnitsPerStep = velInUnitsPerStep,
            duration = spinDuration,
            slowDownSteps = slowDownSteps,
            extraSubstepsFactor = extraSubstepsFactor,
            visualizationFrequency = _visualizationFrequency
        };
    }

}
