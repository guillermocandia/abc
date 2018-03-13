using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesManager : MonoBehaviour {

    [SerializeField] private int initialLives = 5;

    private int lives;

    public int Lives
    {
        get
        {
            return lives;
        }

        set
        {
            lives = value;
        }
    }

    private void Start()
    {
        lives = initialLives;
    }
}
