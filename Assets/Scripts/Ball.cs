using UnityEngine;

public class Ball : MonoBehaviour {

    private Rigidbody2D rb;

	void Start () {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(3, 5);    
    }
	
}
