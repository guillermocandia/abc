using System;
using UnityEngine;

[RequireComponent(typeof(PaddleController))]
public class PaddleControllerInput : MonoBehaviour {

    private PaddleController paddleController;
    //public delegate void PressJumpDelegate();
    //public PressJumpDelegate OnPressJump;
    public event Action OnPressJump;

    private void Awake()
    {
        paddleController = GetComponent<PaddleController>();
    }

    void FixedUpdate () {
        float move = Input.GetAxis("Horizontal");
        paddleController.move = move;

        bool launch = Input.GetButtonDown("Jump");
        if (launch)
        {
            if (OnPressJump != null)
            {
                OnPressJump.Invoke();
            }
        }
    }
}
