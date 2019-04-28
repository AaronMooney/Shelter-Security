using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Aaron Mooney
 * 
 * FPMove script that handles player movement
 * Script is adapted from the tutorial "[Unity C#] First Person Controller (E01: Basic FPS Controller and Jumping)" by Acacia Developer
 * link at https://www.youtube.com/watch?v=n-KX8AeGK7E
 * 
 * parts taken from the tutorial are marked with 
 * // ** ACACIA DEVELOPER ** //
 *       his code here...
 *       any modifications within his code are marked with
 *       // ** AARON MOONEY ** //
 *       my code here
 *       // ** AARON MOONEY END ** //
 * // ** ACACIA DEVELOPER END ** //
 * */
public class FPMove : MonoBehaviour
{
    // ** ACACIA DEVELOPER ** //
    private string horizontalInput = "Horizontal";
    private string verticalIput = "Vertical";
    [SerializeField] private float moveSpeed = 6;
    private CharacterController charController;
    private bool isJumping;
    [SerializeField] private AnimationCurve jumpFallOff;
    [SerializeField] private float jumpMultiplier;
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;

    private void Awake ()
    {
        charController = GetComponent<CharacterController>();
	}

    private void Update ()
    {

        PlayerMove();

        // ** AARON MOONEY ** //
        // Sprint when holding left shift
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = 9;
        }
        else
        {
            moveSpeed = 6;
        }
        // ** AARON MOONEY END ** //

	}

    // Move the player
    private void PlayerMove()
    {
        float horizontalIn = Input.GetAxis(horizontalInput) * moveSpeed;
        float verticalIn = Input.GetAxis(verticalIput) * moveSpeed;

        Vector3 forwardMove = transform.forward * verticalIn;
        Vector3 rightMove = transform.right * horizontalIn;

        charController.SimpleMove(forwardMove + rightMove);
        CheckJump();
    }

    // Jump
    private void CheckJump()
    {
        if (Input.GetKeyDown(jumpKey) && !isJumping)
        {
            isJumping = true;
            StartCoroutine(Jump());
        }

    }

    // Jump coroutine
    private IEnumerator Jump()
    {
        // removes character jitter when jumping beside an object
        charController.slopeLimit = 90.0f;
        float timeInAir = 0.0f;

        // do while character is not on the ground and not hitting a ceiling
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
    // ** ACACIA DEVELOPER END ** //
}
