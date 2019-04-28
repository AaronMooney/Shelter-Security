using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * WeaponSway script that adds some sway when rotating
 * 
 * This script is from the tutorial "FPS Tutorial Series #05 - Weapon Sway (Position) - Unity 5" by Yaseen Mujahid
 * link: https://www.youtube.com/watch?v=hifCUD3dATs
 * 
 * parts taken from the tutorial are marked with 
 * // ** YASEEN MUJAHID ** //
 *       his code here...
 *       any modifications within his code are marked with
 *       // ** AARON MOONEY ** //
 *       my code here
 *       // ** AARON MOONEY END ** //
 * // ** YASEEN MUJAHID END ** //
 * */
public class WeaponSway : MonoBehaviour {

    // ** YASEEN MUJAHID ** //
    private string mouseXInput = "Mouse X";
    private string mouseYInput = "Mouse Y";
    public float amount = 0.055f;
    public float max = 0.09f;
    private float speed = 3;
    private Vector3 pos;

	void Start () {
        // Set starting position
        pos = transform.localPosition;
	}

	void Update () {

        float factorX = -Input.GetAxis(mouseXInput) * amount;
        float factorY = -Input.GetAxis(mouseYInput) * amount;

        if (factorX > max) factorX = max;
        if (factorX < -max) factorX = -max;
        if (factorY > max) factorY = max;
        if (factorY < -max) factorY = -max;

        // final position
        Vector3 final = new Vector3(pos.x + factorX, pos.y + factorY, pos.z);

        // lerp position to final position over time
        transform.localPosition = Vector3.Lerp(transform.localPosition, final, Time.deltaTime * speed);
    }
    // ** YASEEN MUJAHID END ** //
}
