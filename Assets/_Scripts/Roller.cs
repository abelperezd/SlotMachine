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

    public void StartSpinning(float spinningDuration, float spinVelocity, float slowDownDuration)
    {
        StartCoroutine(Spin(spinningDuration, spinVelocity, slowDownDuration));
    }
    IEnumerator Spin(float spinningDuration, float spinVelocity, float slowDownDuration)
    {

        float counter = 0;

        while (counter < spinningDuration)
        {
            counter += Time.deltaTime;
            transform.position += Vector3.up * Time.deltaTime * spinVelocity;// * vel;
            yield return null;
        }

        /*
        float slowDownFactor = 1;


        counter = 0;
        while (counter < .37f)
        {
            counter += Time.deltaTime;
            slowDownFactor -= Time.deltaTime / .37f;
            transform.position += Vector3.up * Time.deltaTime * spinVelocity * slowDownFactor;// * vel;
            yield return null;
        }
        */
    }
}
