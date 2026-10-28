using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop
{
    public void Construct(TargetBuilder targetBuilder)
    {
        targetBuilder.BuildSpeed();
        targetBuilder.BuildSize();
        targetBuilder.BuildPointVal();
    }
}
