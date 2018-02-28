using UnityEngine;

public class PaddleController : MonoBehaviour {

    [SerializeField] private float speed = 10.0f;
    [SerializeField] private Collider2D leftMargin;
    [SerializeField] private Collider2D rightMargin;

    private SpriteRenderer spriteRenderer;

    [HideInInspector] public float move = 0.0f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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

        if(leftMargin.OverlapPoint(leftPoint))
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
    
}
