using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is the builder interface.

public abstract class TargetBuilder
{
    protected Target target;

    // gets target instance
    public Target Target
    {
        get { return target; }
    }

    // abstract build methods
    public abstract void BuildSpeed();
    public abstract void BuildSize();
    public abstract void BuildPointVal();

}
