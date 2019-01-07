using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class StartConfig : MonoBehaviour {

    public bool isTopDownView;
	// Use this for initialization
	void Start () {
        isTopDownView = false;

        this.gameObject.AddComponent<VRTK_HeightAdjustTeleport>();
	}

    private void Update()
    {
    }
}
