using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TargetValuable : TargetBuilder
{
    public TargetValuable()
    {
        target = new Target("Valuable Target");
    }

    public override void BuildSpeed()
    {
        target.SetSpeed(6f);
    }

    public override void BuildSize()
    {
        target.SetSize(1f);
    }

    public override void BuildPointVal()
    {
        target.SetPointVal(15);
    }

    public TargetObject InstantiateValuableTarget()
    {
        TargetObject newTarget = ScriptableObject.CreateInstance<TargetObject>();

        newTarget.name = target.name;
        newTarget.targetPrefab = GameObject.Find("TargetManager").GetComponent<TargetManager>().GetValuableTargetPrefab(); // gets the prefab, still need to instantiate
        newTarget.size = newTarget.size * target.GetSize();
        newTarget.pointValue = target.GetPointValue();
        newTarget.speed = target.GetSpeed();

        return newTarget;
    }
}
