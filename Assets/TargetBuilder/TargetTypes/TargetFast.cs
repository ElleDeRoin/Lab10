using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFast : TargetBuilder
{
    public TargetFast()
    {
        target = new Target("Fast Target");
    }

    public override void BuildSpeed()
    {
        target.SetSpeed(10f); 
    }

    public override void BuildSize()
    {
        target.SetSize(0.5f);
    }

    public override void BuildPointVal()
    {
        target.SetPointVal(7);
    }

    public TargetObject InstantiateFastTarget()
    {
        TargetObject newTarget = ScriptableObject.CreateInstance<TargetObject>();

        newTarget.name = target.name;
        newTarget.targetPrefab = GameObject.Find("TargetManager").GetComponent<TargetManager>().GetFastTargetPrefab(); // gets the prefab, still need to instantiate
        newTarget.size = newTarget.size * target.GetSize();
        newTarget.pointValue = target.GetPointValue();
        newTarget.speed = target.GetSpeed();

        return newTarget;
    }

}
