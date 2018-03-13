using UnityEngine;
using TMPro;


public class UIController : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private TextMeshProUGUI scoreText;

    private GameObject gameManager;
    private LivesManager livesManager;
    private ScoreManager scoreManager;


    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        livesManager = gameManager.GetComponent<LivesManager>();
        scoreManager = gameManager.GetComponent<ScoreManager>();

        livesManager.OnLivesChange += UpdateLives;
        scoreManager.OnScoreChange += UpdateScore;

    }

    void UpdateLives(int lives)
    {
        livesText.text = lives.ToString();
    }

    void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }
}
