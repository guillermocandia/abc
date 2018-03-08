using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField] private GameObject paddle;

    [Header("Ball Options")]
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private float ballInitialSpeed = 3.0f;
    [SerializeField] private Vector2 ballInitialDirection = Vector2.one;

    private SpriteRenderer paddleSpriteRenderer;
    private SpriteRenderer ballSpriteRenderer;
    private PaddleControllerInput paddleControllerInput;

    private List<GameObject> balls = new List<GameObject>();

    private bool isGameRunning = false;

    private void Awake()
    {
        paddleSpriteRenderer = paddle.GetComponent<SpriteRenderer>();
        paddleControllerInput = paddle.GetComponent<PaddleControllerInput>();
    }

    private void Start()
    {
        StartGame();
        SpawnBall();
    }

    void StartGame()
    {
        isGameRunning = true;
        paddleControllerInput.OnPressJump += LaunchBalls;
    }

    void StopGame()
    {
        isGameRunning = false;
        paddleControllerInput.OnPressJump -= LaunchBalls;
    }

    // Spawn ball in paddle
    void SpawnBall()
    {
        if (!isGameRunning)
        {
            return;
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
        if(isGameRunning)
        {
            SpawnBall();
        }
    }

    private void OnDestroy()
    {
        StopGame();
    }

}
