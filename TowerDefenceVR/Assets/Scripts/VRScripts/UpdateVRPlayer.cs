using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Aaron Mooney
 * 
 * UpdateVRPlayer that keeps the player at the y=0 position at all times to avoid issue where height changes when teleporting
 * */
public class UpdateVRPlayer : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
	}
}
