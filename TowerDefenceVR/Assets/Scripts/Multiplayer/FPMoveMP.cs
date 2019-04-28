using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
/*
 * Aaron Mooney
 * 
 * FPMoveMP script that handles player movement in multiplayer
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
 * 
 * */


public class FPMoveMP : NetworkBehaviour
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

    void Awake ()
    {
        charController = GetComponent<CharacterController>();
	}
    // ** AARON MOONEY ** //
    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
    }

    void Update ()
    {
        if (!isLocalPlayer) return;

        PlayerMove();

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = 9;
        }
        else
        {
            moveSpeed = 6;
        }
	}
    // ** AARON MOONEY ** //

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
// ** ACACIA DEVELOPER END ** //
