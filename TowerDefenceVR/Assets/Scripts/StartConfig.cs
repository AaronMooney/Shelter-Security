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
    public GameObject VRShopScripts;
    public GameObject shopScripts;
    public GameObject canvas;
    public GameObject VRcanvas;
    public GameObject noVRPlayer;
    public GameObject VRWaveConsole;
    public GameObject VRBoundaries;
    public GameObject boundaries;
    public GameObject VRPrices;

    void Start () {

        

        Debug.Log(VRConfig.VREnabled);
        if (VRConfig.VREnabled)
        {
            noVRPlayer.SetActive(false);
            canvas.SetActive(false);
            updateUI.SetActive(false);
            boundaries.SetActive(false);
            shopScripts.SetActive(false);
            VRcanvas.SetActive(true);
            SDKManager.SetActive(true);
            VRUpdateUI.SetActive(true);
            controllers.SetActive(true);
            teleport.SetActive(true);
            VRShopScripts.SetActive(true);
            armory.SetActive(true);
            VRWaveConsole.SetActive(true);
            VRBoundaries.SetActive(true);
            VRPrices.SetActive(true);

        }
        else
        {
            boundaries.SetActive(true);
            noVRPlayer.SetActive(true);
            canvas.SetActive(true);
            updateUI.SetActive(true);
            shopScripts.SetActive(true);
            VRcanvas.SetActive(false);
            SDKManager.SetActive(false);
            VRUpdateUI.SetActive(false);
            controllers.SetActive(false);
            teleport.SetActive(false);
            VRShopScripts.SetActive(false);
            armory.SetActive(false);
            VRWaveConsole.SetActive(false);
            VRBoundaries.SetActive(false);
            VRPrices.SetActive(false);

        }
    }

    private void Update()
    {
    }
}
