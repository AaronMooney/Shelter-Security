using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class StartConfig : MonoBehaviour {


    public GameObject SDKManager;
    public GameObject updateUI;
    public GameObject VRUpdateUI;
    public GameObject controllers;
    public GameObject teleport;
    public GameObject armory;
    public GameObject VRShop;
    public GameObject canvas;
    public GameObject VRcanvas;
    public GameObject noVRPlayer;

    void Start () {

        

        Debug.Log(VRConfig.VREnabled);
        if (VRConfig.VREnabled)
        {
            noVRPlayer.SetActive(false);
            canvas.SetActive(false);
            updateUI.SetActive(false);
            VRcanvas.SetActive(true);
            SDKManager.SetActive(true);
            VRUpdateUI.SetActive(true);
            controllers.SetActive(true);
            teleport.SetActive(true);
            VRShop.SetActive(true);
            armory.SetActive(true);

        }
        else
        {
            noVRPlayer.SetActive(true);
            canvas.SetActive(true);
            updateUI.SetActive(true);
            VRcanvas.SetActive(false);
            SDKManager.SetActive(false);
            VRUpdateUI.SetActive(false);
            controllers.SetActive(false);
            teleport.SetActive(false);
            VRShop.SetActive(false);
            armory.SetActive(false);

        }
    }

    private void Update()
    {
    }
}
