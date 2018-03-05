using UnityEngine;

[RequireComponent(typeof(PaddleController))]
public class PaddleControllerInput : MonoBehaviour {

    private PaddleController paddleController;

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
            paddleController.LaunchBall();
        }
    }
}
