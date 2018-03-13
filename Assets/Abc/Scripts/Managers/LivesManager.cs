using System;
using UnityEngine;


public class LivesManager : MonoBehaviour
{
    [SerializeField] private int initialLives = 5;

    public event Action<int> OnLivesChange;

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
            InvokeOnLivesChange(lives);
        }
    }

    private void Start()
    {
        lives = initialLives;
        InvokeOnLivesChange(lives);
    }

    void InvokeOnLivesChange(int lives)
    {
        if (OnLivesChange != null)
        {
            OnLivesChange.Invoke(lives);
        }
    }
    
}
