﻿using System;
using UnityEngine;

public class Ball : MonoBehaviour {

    [Header("Options")]
    [SerializeField] private String deadZoneTag = "DeadZone";

    private Rigidbody2D rb;

    private float speed;

    void Start () {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector3.zero;   
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(deadZoneTag))
        {
            Destroy(this.gameObject);
            // TODO notify manager
            return;
        }

        if (collision.collider.CompareTag("Block"))
        {
            collision.collider.GetComponent<Block>().ReceiveDamage(1);
            return;
        }
    }

    public void AddVelocity(Vector3 direction, float speed)
    {
        rb.velocity = direction * speed;
    }

}
