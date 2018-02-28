using UnityEngine;

[RequireComponent(typeof(PaddleController))]
public class PaddleControllerInput : MonoBehaviour {

    private PaddleController paddleController;

    private void Start()
    {
        this.paddleController = GetComponent<PaddleController> ();
    }

    void FixedUpdate () {
        float move = Input.GetAxis("Horizontal");
        this.paddleController.move = move;
	}
}
