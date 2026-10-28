using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLarge : TargetBuilder
{
    public TargetLarge()
    {
        target = new Target("Large Target");
    }

    public override void BuildSpeed()
    {
        target.SetSpeed(3f);
    }

    public override void BuildSize()
    {
        target.SetSize(1.5f);
    }

    public override void BuildPointVal()
    {
        target.SetPointVal(3);
    }

    public TargetObject InstantiateLargeTarget()
    {
        TargetObject newTarget = ScriptableObject.CreateInstance<TargetObject>();

        newTarget.name = target.name;
        newTarget.targetPrefab = GameObject.Find("TargetManager").GetComponent<TargetManager>().GetLargeTargetPrefab(); // gets the prefab, still need to instantiate
        newTarget.size = newTarget.size * target.GetSize();
        newTarget.pointValue = target.GetPointValue();
        newTarget.speed = target.GetSpeed();

        return newTarget;
    }
}
