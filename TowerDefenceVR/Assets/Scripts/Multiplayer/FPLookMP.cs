using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
/*
 * Aaron Mooney
 * 
 * FPLookMP script that deals with camera rotation with the mouse for multiplayer
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
public class FPLookMP : NetworkBehaviour {

    // ** ACACIA DEVELOPER ** //
    private string mouseXInput = "Mouse X";
    private string mouseYInput = "Mouse Y";
    private float mouseSensitivity = 150;
    [SerializeField] private Camera playerCam;

    private float xAxisClamp;

    private void Awake()
    {
        LockCursor();
        xAxisClamp = 0.0f;
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;

    }

    // ** AARON MOONEY ** //
    private void Update()
    {
        // return if not local player
        if (!isLocalPlayer) return;
        CameraRotate();
        if (Application.isFocused) LockCursor();
    }
    // ** AARON MOONEY ** //

    private void CameraRotate()
    {
        float mouseX = Input.GetAxis(mouseXInput);
        float mouseY = Input.GetAxis(mouseYInput);

        xAxisClamp += mouseY;

        //Clamps the x axis to avoid camera flipping when looking vertically up or down
        if (xAxisClamp > 90.0f)
        {
            xAxisClamp = 90.0f;
            mouseY = 0.0f;
            ClampXAxisRotationToValue(270.0f);
        }
        else if (xAxisClamp < -90.0f)
        {
            xAxisClamp = -90.0f;
            mouseY = 0.0f;
            ClampXAxisRotationToValue(90.0f);
        }

        playerCam.transform.Rotate(Vector3.left * mouseY);
        transform.Rotate(Vector3.up * mouseX);
    }

    //function to clamp the x axis rotation to avoid camera rotation exceeding clamp
    private void ClampXAxisRotationToValue(float value)
    {
        Vector3 eulerRotation = transform.eulerAngles;
        eulerRotation.x = value;
        playerCam.transform.eulerAngles = eulerRotation;
    }
}
// ** ACACIA DEVELOPER END ** //
