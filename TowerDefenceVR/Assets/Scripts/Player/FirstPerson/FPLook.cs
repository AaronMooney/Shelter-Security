using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPLook : MonoBehaviour {

    private string mouseXInput = "Mouse X";
    private string mouseYInput = "Mouse Y";
    private float mouseSensitivity = 150;
    [SerializeField] private Camera playerCam;

    private float xAxisClamp;

    private void Awake()
    {
        xAxisClamp = 0.0f;
    }

    private void Update()
    {
        if (!GetComponent<PlayerActions>().shopActive)
            CameraRotate();
        
    }

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
