using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TransformSaver))]
public class AutoSaveID : MonoBehaviour
{
    private static Dictionary<string, int> counters = new Dictionary<string,int>();

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void ResetCounters() { counters.Clear();}

    private void Awake() {
        string key = gameObject.name;
        if (!counters.ContainsKey(key)) counters[key] = 0;
        GetComponent<TransformSaver>().SaveID = key + "_" + counters[key]++;
    }
}
