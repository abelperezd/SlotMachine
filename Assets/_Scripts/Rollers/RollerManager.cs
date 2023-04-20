using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> Parameters needed to setup a roller </summary>
public struct SpinningConfiguration
{
    public int stepsToDo; //total amount of steps we will do
    public float velocity; //units: units/step
    public float duration; //total duration of the spinning
    public int slowDownSteps; //how many steps will be to slow down
    public int extraSubstepsFactor; //how many steps we will add every slowing down
    public int visualizationFrequency; //how many steps we do for every figure (steps/figure)
}

/// <summary> Class in charge of setting up and starting the rollers </summary>
public class RollerManager : MonoBehaviour
{
    #region Fields

    [SerializeField] private RollerRandomDurationGenerator _durationGenerator;

    [Space(10)]
    
    [SerializeField] private List<Roller> _rollers;

    [Space(10)]

    [Range(1, 30)]
    [Tooltip("Figures/second")]
    [SerializeField] private int _spinVelocity = 8;

    [Range(0, 1)]
    [Tooltip("Seconds")]
    [SerializeField] private float _delayBetweenRollers = .5f;

    [Range(0, 1)]
    [Tooltip("Percentage of the total time that the roller will be slowing down")]
    [SerializeField] private float _slowDownPercentage = .2f;

    [Range(1, 15)]
    [Tooltip("Steps we do for every figure")]
    [SerializeField] private int _visualizationFrequency = 8;

    #endregion

    #region Unity Callbacks

    private void Start()
    {
        GameManager.Instance.OnGameStarted += StartSpinningRollers;
    }

    #endregion

    #region Methods

    private void StartSpinningRollers()
    {
        StartCoroutine(nameof(SpinRollers));
    }

    IEnumerator SpinRollers()
    {
        float baseDuration = _durationGenerator.GetSpinBaseDuration();
        foreach (Roller r in _rollers)
        {
            float currentRollerDuration = _durationGenerator.GetExtraRandomDuration(baseDuration, _spinVelocity, _delayBetweenRollers);
            SpinningConfiguration configuration = GetSpinningConfiguration(currentRollerDuration);
            r.StartSpinning(configuration);
            yield return new WaitForSeconds(_delayBetweenRollers);
        }
    }

    private SpinningConfiguration GetSpinningConfiguration(float spinDuration)
    {
        int figuresToSee = (int)(_spinVelocity * spinDuration); //units: figures

        int stepsToDo = figuresToSee * _visualizationFrequency; //units: steps

        float velInUnitsPerStep = _spinVelocity * Figure.FIGURE_SIZE * spinDuration / (float)stepsToDo; //units: units/step

        int slowDownFigures = Mathf.CeilToInt(figuresToSee * _slowDownPercentage); //units: figures

        int slowDownSteps = slowDownFigures * _visualizationFrequency; //units: steps

        //to stop faster for higher velocities
        int extraSubstepsFactor = Mathf.Max(1, _spinVelocity / slowDownFigures); 

        return new SpinningConfiguration
        {
            stepsToDo = stepsToDo,
            velocity = velInUnitsPerStep,
            duration = spinDuration,
            slowDownSteps = slowDownSteps,
            extraSubstepsFactor = extraSubstepsFactor,
            visualizationFrequency = _visualizationFrequency
        };
    }

    #endregion
}
