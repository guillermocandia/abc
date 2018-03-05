using UnityEngine;

public class PaddleController : MonoBehaviour {

    [Header("Paddle Options")]
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private Collider2D leftMargin;
    [SerializeField] private Collider2D rightMargin;

    [Header("Ball Options")]
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private float ballInitialSpeed = 3.0f;
    [SerializeField] private Vector2 ballInitialDirection = Vector2.one;

    private SpriteRenderer spriteRenderer;
    private SpriteRenderer ballSpriteRenderer;

    [HideInInspector] public float move = 0.0f;
    private bool isBallAttached = false;
    private GameObject ball;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        ballSpriteRenderer = ballPrefab.GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        SpawnBall();
    }


    void Update()
    {
        Move();
    }

    private void Move()
    { 
        Vector3 target = transform.position;
        target.x += move * speed * Time.deltaTime;

        Vector2 leftPoint = new Vector2(target.x - spriteRenderer.bounds.size.x/2, target.y);

        if (leftMargin.OverlapPoint(leftPoint))
        {
            return;
        }

        Vector2 rightPoint = new Vector2(target.x + spriteRenderer.bounds.size.x / 2, target.y);

        if (rightMargin.OverlapPoint(rightPoint))
        {
            return;
        }

        transform.position = target;
    }

    void SpawnBall()
    {
        float offsetY = spriteRenderer.bounds.size.y / 2 +
            ballSpriteRenderer.bounds.size.y / 2;

        Vector2 spawnPoint = (Vector2) transform.position + Vector2.up * offsetY;
        ball = Instantiate(ballPrefab, spawnPoint, Quaternion.identity, this.transform);
        isBallAttached = true;
    }

    public void LaunchBall()
    {
        if (ball && isBallAttached)
        {
            Vector2 direction = ballInitialDirection;
            if (move != 0)
            {
                direction.x *= move > 0 ? 1 : -1;
            }

            ball.transform.SetParent(null);
            isBallAttached = false;
            ball.GetComponent<Ball>().AddVelocity(direction, ballInitialSpeed);
        }
    }
    
}
