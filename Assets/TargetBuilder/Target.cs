using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target 
{
    private float speed;
    private float size;
    private int pointValue;

    public string name { get; }

    public Target(string name)
    {
        this.name = name;
    }
    
    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public void SetSize(float sizeMultiplier)
    {
        this.size = sizeMultiplier;
    }

    public void SetPointVal(int pointValue)
    {
        this.pointValue = pointValue;
    }

    public float GetSize()
    {
        return this.size;
    }

    public int GetPointValue()
    {
        return this.pointValue;
    }

    public float GetSpeed()
    {
        return this.speed;
    }

    //public void InstantiateTarget()
    //{

    //}
}
