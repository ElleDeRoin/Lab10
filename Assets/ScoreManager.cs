using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ScoreManager
{
    public static event Action<int> OnScoreChanged;

    private int _currentScore = 0;

    public int CurrentScore => _currentScore;

    //Add to the score
    public void AddScore(int points)
    {
        if (points <= 0) return;

        _currentScore += points;

        OnScoreChanged?.Invoke(_currentScore);
    }
    //Reset the score when player dies 
    public void ResetScore()
    {
        _currentScore = 0;
        OnScoreChanged?.Invoke(_currentScore);
    }
}