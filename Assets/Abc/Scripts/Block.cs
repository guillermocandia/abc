using System;
using UnityEngine;

public class Block : MonoBehaviour {

    [SerializeField] private int initialHealth;
    private int health;

    public event Action<GameObject> OnBlockDestroyed;

    public int InitialHealth
    {
        get
        {
            return initialHealth;
        }
    }

    private void Start()
    {
        health = InitialHealth;
    }

    public void ReceiveDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        if (OnBlockDestroyed != null)
        {
            OnBlockDestroyed.Invoke(this.gameObject);
        }
    }

}
