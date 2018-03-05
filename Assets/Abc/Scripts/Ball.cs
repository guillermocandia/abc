using UnityEngine;

public class Ball : MonoBehaviour {

    private Rigidbody2D rb;

	void Start () {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.one * 3;   
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Block"))
        {
            collision.collider.GetComponent<Block>().ReceiveDamage(1);
        }
    }

}
