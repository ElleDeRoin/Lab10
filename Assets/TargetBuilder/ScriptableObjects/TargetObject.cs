using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;

[CreateAssetMenu(fileName = "Target", menuName = "Targets/Target")]

public class TargetObject : ScriptableObject
{
    public string type;
    public GameObject targetPrefab;
    public float speed;
    public float size = 1; // will be multiplied by modifier in other scripts
    public int pointValue;
}
