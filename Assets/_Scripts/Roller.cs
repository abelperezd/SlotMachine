using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartSpinning(float spinningDuration, float spinVelocityInUnits, float slowDownDuration)
    {
        StartCoroutine(Spin(spinningDuration, spinVelocityInUnits, slowDownDuration));
    }
    IEnumerator Spin(float spinningDuration, float spinVelocityInUnits, float slowDownDuration)
    {
        float counter = 0;

        while (counter < spinningDuration)
        {
            counter += Time.deltaTime;
            transform.position += Vector3.up * Time.deltaTime * spinVelocityInUnits;
            yield return null;
        }


        float slowDownFactor = 1;


        counter = 0;

        float unitsIncreased = 0;
        while (counter < slowDownDuration)
        {
            counter += Time.deltaTime;
            slowDownFactor -= Time.deltaTime / slowDownDuration;
            unitsIncreased += Time.deltaTime * spinVelocityInUnits * slowDownFactor;
            transform.position += Vector3.up * Time.deltaTime * spinVelocityInUnits * slowDownFactor;// * vel;
            yield return null;
        }
        Debug.Log("Figures: " + unitsIncreased);

    }
}
