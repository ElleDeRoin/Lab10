using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SavingService
{
    private const string OBJECTS_KEY = "objects";
    private const string SAVEID_KEY = "$saveID";

    public static void SaveGame(string fileName)
    {
        var result = new JsonData();
        var allSaveableObjects = Object.FindObjectsOfType<MonoBehaviour>().OfType<ISaveable>();
        if (allSaveableObjects.Count() > 0)
        {
            var savedObjects = new JsonData();
            foreach (var saveableObject in allSaveableObjects)
            {
                var data = saveableObject.SavedData;
                if (data.IsObject)
                {
                    data[SAVEID_KEY] = saveableObject.SaveID;
                    savedObjects.Add(data);
                }
                else
                {
                    var behavior = saveableObject as MonoBehaviour;
                    Debug.LogWarningFormat(behavior, "{0}'s save data is not a dictionary. The object was not saved.", behavior.name);
                }
            }
            result[OBJECTS_KEY] = savedObjects;
        }
        else { Debug.LogWarningFormat("The scene did not include any saveable objects"); }

        var outputPath = Path.Combine(Application.persistentDataPath, fileName);
        var writer = new JsonWriter();
        writer.PrettyPrint = true;
        result.ToJson(writer);
        File.WriteAllText(outputPath, writer.ToString());
        Debug.LogFormat("Wrote saved game to {0}", outputPath);

        result = null;
        System.GC.Collect();
    }

    public static void LoadGame(string fileName)
    {
        var dataPath = Path.Combine(Application.persistentDataPath, fileName);
        if (!File.Exists(dataPath)) { Debug.LogErrorFormat("No file exists at {0}", dataPath); return; }
        var text = File.ReadAllText(dataPath);
        var data = JsonMapper.ToObject(text);
        if (data == null || !data.IsObject) { Debug.LogErrorFormat("Data at {0} is not a JSON object", dataPath); return; }

        if (data.ContainsKey(OBJECTS_KEY))
        {
            var objects = data[OBJECTS_KEY];
            void LoadObjectsAfterSceneLoad(UnityEngine.SceneManagement.Scene scene, LoadSceneMode loadSceneMode)
            {
                var allLoadableObjects = Object.FindObjectsOfType<MonoBehaviour>().OfType<ISaveable>().ToDictionary(o => o.SaveID, o => o);
                var objectsCount = objects.Count;
                for (int i = 0; i < objectsCount; i++)
                {
                    var objectData = objects[i];
                    var saveID = (string)objectData[SAVEID_KEY];
                    if (allLoadableObjects.ContainsKey(saveID))
                    {
                        var loadableObject = allLoadableObjects[saveID];
                        loadableObject.LoadFromData(objectData);
                    }
                }
                SceneManager.sceneLoaded -= LoadObjectsAfterSceneLoad;
                //LoadObjectsAfterSceneLoad = null;
                System.GC.Collect();
                //Debug.Log(data[OBJECTS_KEY]);
            }
            SceneManager.sceneLoaded += LoadObjectsAfterSceneLoad;
        }
        return;
    }

    /*
    private static void LoadObjectsAfterSceneLoad(UnityEngine.SceneManagement.Scene scene, LoadSceneMode loadSceneMode)
    {
        var allLoadableObjects = Object.FindObjectsOfType<MonoBehaviour>().OfType<ISaveable>().ToDictionary(o => o.SaveID, o => o);
        var objectsCount = objects.Count();
        for (int i = 0; i < objectsCount; i++)
        {
            var objectData = objects[i];
            var saveID = (string)objectData[SAVEID_KEY];
            if (allLoadableObjects.ContainsKey(saveID))
            {
                var loadableObject = allLoadableObjects[saveID];
                loadableObject.LoadFromData(objectData);
            }
        }
        SceneManager.sceneLoaded -= LoadObjectsAfterSceneLoad;
        LoadObjectsAfterSceneLoad = null;
        System.GC.Collect();
    }
    */
}
