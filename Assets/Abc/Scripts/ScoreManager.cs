using UnityEngine;

public class ScoreManager : MonoBehaviour {

    private int score = 0;

    public int Score
    {
        get
        {
            return score;
        }
    }

    public void AddScore(int score)
    {
        this.score += score;
        Debug.Log("Score: " + this.score);
    }

}
