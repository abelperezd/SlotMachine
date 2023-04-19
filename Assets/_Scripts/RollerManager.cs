using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollerManager : MonoBehaviour
{
    private const float FIGURE_SIZE = 2.2f;

    [SerializeField] private List<Roller> _rollers;

    [Tooltip("Figures/second")]
    [SerializeField] private int _spinVelocity = 8;

    [Tooltip("Steps/figure")]
    [SerializeField] private int _resolution = 8;

    [Tooltip("Seconds")]
    [SerializeField] private float _delayBetweenRollers = .5f;

    [SerializeField] private float _slowDownDuration = 2f;

    [SerializeField] private float _minSpinningDuration = 2;

    [SerializeField] private float _maxSpinningDuration = 4;

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
            (spins, duration) = GetSpinsAndDuration();
            r.StartSpinning(spins, duration);
            yield return new WaitForSeconds(_delayBetweenRollers);
        }
    }

    private void Start()
    {
        //Debug.Log("Slow down duration: " + SlowDownDuration);
        StartSpinningRollers();
    }

    private (int, float) GetSpinsAndDuration()
    {
        float randomDuration = Random.Range(_minSpinningDuration, _maxSpinningDuration);
        //        Debug.Log("randomDuration: " + randomDuration);

        int spins = Mathf.RoundToInt(_spinVelocity * randomDuration);
        //Debug.Log("spin velocity: " + SpinVelocity);

        //Debug.Log("spins: " + spins);

        float spinDuration = (float)spins / _spinVelocity;
        //      Debug.Log("spin duration: " + spinDuration);
        return (spins, spinDuration);
    }

    private float GetSlowDownDuration()
    {
        int spins = Mathf.RoundToInt(_spinVelocity * _slowDownDuration);
        Debug.Log("spins " + spins);
        float spinDuration = (float)spins / _spinVelocity;
        // Debug.Log("spin duration " + spinDuration);
        return spinDuration;
    }

}
