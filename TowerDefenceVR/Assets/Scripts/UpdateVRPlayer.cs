using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateVRPlayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
	}
}
