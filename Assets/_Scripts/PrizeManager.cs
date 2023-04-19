using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PrizeManager : MonoBehaviour
{
    private static PrizeManager _instance;

    public static PrizeManager Instance => _instance;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
    }



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
