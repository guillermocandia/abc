using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    [Header("Paddle Options")]
    [SerializeField] private GameObject paddle;

    [Header("Ball Options")]
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private float ballInitialSpeed = 3.0f;
    [SerializeField] private Vector2 ballInitialDirection = Vector2.one;

    private SpriteRenderer paddleSpriteRenderer;
    private SpriteRenderer ballSpriteRenderer;
    private PaddleControllerInput paddleControllerInput;

    private List<GameObject> balls = new List<GameObject>();
    private List<GameObject> blocks = new List<GameObject>();

    private bool isGameRunning = false;

    private GameObject gameManager;
    private ScoreManager scoreManager;
    private LivesManager livesManager;

    private bool win = false;


    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        scoreManager = gameManager.GetComponent<ScoreManager>();
        livesManager = gameManager.GetComponent<LivesManager>();
        paddleSpriteRenderer = paddle.GetComponent<SpriteRenderer>();
        paddleControllerInput = paddle.GetComponent<PaddleControllerInput>();
        scoreManager.Score += 0;
        livesManager.Lives += 0;
        StartCoroutine("StartGame");
    }

    public IEnumerator StartGame()
    {
        yield return null;
        GetBlocks();
        paddleControllerInput.OnPressJump += LaunchBalls;
        paddle.SetActive(true);
        isGameRunning = true;
        StartCoroutine("SpawnBall");
        win = false;
    }

    public void StopGame()
    {
        if (win)
        {
            int sceneIndex = SceneManager.GetActiveScene().buildIndex;
            //if()
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            return;
        }

        isGameRunning = false;
        if(paddle)
        {
            paddle.SetActive(false);
        }
        if(paddleControllerInput)
        {
            paddleControllerInput.OnPressJump -= LaunchBalls;
        }
       
        foreach(GameObject ball in balls)
        {
            if(ball)
            {
                ball.GetComponent<Ball>().OnBallDestroyed -= BallDestroyed;
                Destroy(ball);
            }       
        }
        foreach (GameObject block in blocks)
        {
            Destroy(block);
        }

       
    }

    // Spawn ball in paddle
    IEnumerator SpawnBall()
    {
        if (!isGameRunning || !paddle)
        {
            StopCoroutine("SpawnBall");
        }
        yield return null;

        GameObject ball = Instantiate(ballPrefab, Vector2.zero, Quaternion.identity, paddle.transform);
        ballSpriteRenderer = ball.GetComponent<SpriteRenderer>();

        float offsetY = paddleSpriteRenderer.bounds.size.y / 2 +
            ballSpriteRenderer.bounds.size.y / 2;

        Vector2 spawnPoint = (Vector2) paddle.transform.position + Vector2.up * offsetY;
        ball.transform.position = spawnPoint;
        ball.GetComponent<Ball>().OnBallDestroyed += BallDestroyed;
        balls.Add(ball);
    }

    void LaunchBalls()
    {
        foreach (var ball in balls)
        {
            if(ball.transform.parent == paddle.transform)
            {
                ball.GetComponent<Ball>().AddVelocity(ballInitialDirection, ballInitialSpeed);
                ball.transform.SetParent(null);
            }
        }
        //Vector2 direction = ballInitialDirection;
        //if (move != 0)
        //{
        //    direction.x *= move > 0 ? 1 : -1;
        //}
        // TODO 
    }

    void BallDestroyed(GameObject ball)
    {
        ball.GetComponent<Ball>().OnBallDestroyed -= BallDestroyed;
        balls.Remove(ball);

        if (isGameRunning && !win)
        {
            livesManager.Lives--;
            if (livesManager.Lives <= 0)
            {
                StopGame();
                return;
            }
            else
            {
                if (this)
                    StartCoroutine("SpawnBall");
            }
        }
        
    }

    private void OnDestroy()
    {
        StopGame();
    }

    private void GetBlocks()
    {
        blocks.AddRange(GameObject.FindGameObjectsWithTag("Block"));
        foreach(GameObject block in blocks)
        {
            block.GetComponent<Block>().OnBlockDestroyed += BlockDestroyed;
        }
    }

    void BlockDestroyed(GameObject block)
    {
        Block blockController = block.GetComponent<Block>();
        blockController.OnBlockDestroyed -= BlockDestroyed;
        blocks.Remove(block);
        if(isGameRunning)
        {
            scoreManager.Score += blockController.InitialHealth;

            if (blocks.Count <= 0)
            {
                win = true;
                StopGame();
                return;
            }
        }
    }

}
