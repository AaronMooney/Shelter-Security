using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleScope : MonoBehaviour {

    [SerializeField] private GameObject scope;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (GetComponentInParent<VRTK.VRTK_InteractableObject>().IsGrabbed())
        {
            scope.SetActive(true);
        } else
        {
            scope.SetActive(false);
        }
	}
}
