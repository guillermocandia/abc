using UnityEngine;

public class Block : MonoBehaviour {

    [SerializeField] private int health;

    public void ReceiveDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    
}
