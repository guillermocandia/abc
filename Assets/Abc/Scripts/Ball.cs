using UnityEngine;

public class Ball : MonoBehaviour {

    private Rigidbody2D rb;

	void Start () {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, 5);    
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Block"))
        {
            collision.GetComponent<Block>().ReceiveDamage(1);
        }
    }

}
