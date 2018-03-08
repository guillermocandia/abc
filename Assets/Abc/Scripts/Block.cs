using System;
using UnityEngine;

public class Block : MonoBehaviour {

    [SerializeField] private int health;

    public event Action<GameObject> OnBlockDestroyed;

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
