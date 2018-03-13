using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public event Action<int> OnScoreChange;

    private int score = 0;

    public int Score
    {
        get
        {
            return score;
        }

        set
        {
            score = value;
            if (OnScoreChange != null)
            {
                OnScoreChange.Invoke(score);
            }
        }
    }
}
