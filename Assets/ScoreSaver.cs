using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

// This script saves the player's current score to a binary file, and loads the score on player request
public class ScoreSaver : MonoBehaviour
{
    public string binaryPath;

    private void Awake()
    {
        binaryPath = Application.persistentDataPath + "/score.dat";
    }

    [Serializable]
    public class ScoreData
    {
        public int score;
    }

    public void SaveScore(int playerScore)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(binaryPath, FileMode.Create);

        ScoreData data = new ScoreData();
        data.score = playerScore;

        formatter.Serialize(stream, data);
        stream.Close();
        Debug.Log($"Saved score to {binaryPath}");

    }

    public int LoadScore()
    {
        if (File.Exists(binaryPath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(binaryPath, FileMode.Open);

            ScoreData data = (ScoreData)formatter.Deserialize(stream);
            stream.Close();

            Debug.Log($"Loaded score: {data.score}");
            return data.score;
        }
        else
        {
            Debug.Log("Binary save file not found.");
            return 0;
        }
    }
}
