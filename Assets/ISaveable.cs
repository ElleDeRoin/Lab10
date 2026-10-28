using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public interface ISaveable
{
    string SaveID { get; }
    JsonData SavedData { get; }
    void LoadFromData(JsonData data);
}