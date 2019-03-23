using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour {

    private string mouseXInput = "Mouse X";
    private string mouseYInput = "Mouse Y";
    public float amount = 0.055f;
    public float max = 0.09f;
    float smooth = 3;
    Vector3 pos;

	void Start () {
        pos = transform.localPosition;
	}

	void Update () {

        float factorX = -Input.GetAxis(mouseXInput) * amount;
        float factorY = -Input.GetAxis(mouseYInput) * amount;

        if (factorX > max) factorX = max;
        if (factorX < -max) factorX = -max;
        if (factorY > max) factorY = max;
        if (factorY < -max) factorY = -max;

        Vector3 final = new Vector3(pos.x + factorX, pos.y + factorY, pos.z);
        transform.localPosition = Vector3.Lerp(transform.localPosition, final, Time.deltaTime * smooth);
    }
}
