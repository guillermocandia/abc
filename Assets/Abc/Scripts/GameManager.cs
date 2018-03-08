using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [Header("Paddle Options")]
    [SerializeField] private GameObject paddle;

    [Header("Ball Options")]
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private float ballInitialSpeed = 3.0f;
    [SerializeField] private Vector2 ballInitialDirection = Vector2.one;

    [Header("Score Manager")]
    [SerializeField] private ScoreManager scoreManager;

    [Header("Game Options")]
    public int lives = 5;

    private SpriteRenderer paddleSpriteRenderer;
    private SpriteRenderer ballSpriteRenderer;
    private PaddleControllerInput paddleControllerInput;

    private List<GameObject> balls = new List<GameObject>();
    private List<GameObject> blocks = new List<GameObject>();

    private bool isGameRunning = false;

    private void Awake()
    {
        paddleSpriteRenderer = paddle.GetComponent<SpriteRenderer>();
        paddleControllerInput = paddle.GetComponent<PaddleControllerInput>();
    }

    private void Start()
    {
        StartCoroutine("StartGame");
    }

    IEnumerator StartGame()
    {
        yield return null;
        GetBlocks();
        paddleControllerInput.OnPressJump += LaunchBalls;
        StartCoroutine("SpawnBall");
        isGameRunning = true;
    }

    void StopGame()
    {
        isGameRunning = false;
        paddleControllerInput.OnPressJump -= LaunchBalls;
        foreach(GameObject ball in balls)
        {
            ball.GetComponent<Ball>().OnBallDestroyed -= BallDestroyed;
            Destroy(ball);
        }
    }

    // Spawn ball in paddle
    IEnumerator SpawnBall()
    {
        yield return null;
        if (!isGameRunning)
        {
            StopCoroutine("SpawnBall");
        }
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

        if (isGameRunning)
        {
            lives--;
            if (lives <= 0)
            {
                StopGame();
            }
        }
        StartCoroutine("SpawnBall");
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
        Debug.Log(blocks.Count);
    }

    void BlockDestroyed(GameObject block)
    {
        block.GetComponent<Block>().OnBlockDestroyed -= BlockDestroyed;
        blocks.Remove(block);
        if(isGameRunning)
        {
            Debug.Log(blocks.Count);
            scoreManager.AddScore(1);

            if(blocks.Count <= 0)
            {
                StopGame();
                return;
            }  
        }
    }

}
