using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Coordinates
{
    public int x { get; set; }
    public int y { get; set; }

    public Coordinates(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}


public class Figure : MonoBehaviour
{
    private Roller _roller;

    [field: SerializeField]
    public FigureType Type { get; private set; }

    public Coordinates coordinates;

    private int roller;

    void Awake()
    {
        _roller = GetComponentInParent<Roller>();
        coordinates.x = _roller.Position;
    }

    private void Start()
    {
        _roller.OnRollerStopped += RollerStopped;
    }

    private void OnDestroy()
    {
        _roller.OnRollerStopped -= RollerStopped;
    }

    private void RollerStopped()
    {
        int pos = CheckMyPosition();
        if (pos == -1)
            return;

        coordinates.y = pos;
        //Debug.Log("I'm selected: " + name);
        PrizeManager.Instance.AddFigureToCheck(this);
    }

    private int CheckMyPosition()
    {
        //Debug.Log(name + " -> original pos " + transform.position.y);
        int pos = Mathf.RoundToInt(transform.position.y);

        //Debug.Log(name + " -> pos: " + pos);
        if (pos == 3)
            return 0;
        if (pos == 1)
            return 1;
        if (pos == -1)
            return 2;
        return -1;
    }
}
