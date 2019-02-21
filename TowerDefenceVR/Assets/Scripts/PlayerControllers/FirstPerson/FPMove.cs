using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPMove : MonoBehaviour {

    private string horizontalInput = "Horizontal";
    private string verticalIput = "Vertical";
    [SerializeField] private float moveSpeed = 6;

    private CharacterController charController;
    private bool isJumping;
    [SerializeField] private AnimationCurve jumpFallOff;
    [SerializeField] private float jumpMultiplier;
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;

    void Awake ()
    {
        charController = GetComponent<CharacterController>();
	}
	
	void Update ()
    {
        PlayerMove();
	}

    private void PlayerMove()
    {
        float horizontalIn = Input.GetAxis(horizontalInput) * moveSpeed;
        float verticalIn = Input.GetAxis(verticalIput) * moveSpeed;

        Vector3 forwardMove = transform.forward * verticalIn;
        Vector3 rightMove = transform.right * horizontalIn;

        charController.SimpleMove(forwardMove + rightMove);
        CheckJump();
    }

    private void CheckJump()
    {
        if (Input.GetKeyDown(jumpKey) && !isJumping)
        {
            isJumping = true;
            StartCoroutine(Jump());
        }

    }

    private IEnumerator Jump()
    {
        charController.slopeLimit = 90.0f;
        float timeInAir = 0.0f;

        do
        {
            float jumpForce = jumpFallOff.Evaluate(timeInAir);
            charController.Move(Vector3.up * jumpForce * jumpMultiplier * Time.deltaTime);
            timeInAir += Time.deltaTime;
            yield return null;
        } while (!charController.isGrounded && charController.collisionFlags != CollisionFlags.Above);

        charController.slopeLimit = 45.0f;
        isJumping = false;
    }
}
