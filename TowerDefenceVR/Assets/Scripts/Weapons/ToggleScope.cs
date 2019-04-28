using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Aaron Mooney
 * 
 * ToggleScope script that enables the scope on VR weapons that are picked up
 * */
public class ToggleScope : MonoBehaviour {

    [SerializeField] private GameObject scope;

	
	// Update is called once per frame
	void Update () {
        // Check to see if the weapon is currently grabbed
		if (GetComponentInParent<VRTK.VRTK_InteractableObject>().IsGrabbed())
        {
            scope.SetActive(true);
        } else
        {
            scope.SetActive(false);
        }
	}
}
