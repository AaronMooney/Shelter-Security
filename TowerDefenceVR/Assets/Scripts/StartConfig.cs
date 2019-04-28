using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

/*
 * Aaron Mooney
 * 
 * StartConfig script that enables or disables game objects based on whether VR is enabled or not.
 * */
public class StartConfig : MonoBehaviour {

    [Header("Non VR Fields")]
    public GameObject updateUI;
    public GameObject canvas;
    public GameObject shopScripts;
    public GameObject noVRPlayer;
    public GameObject boundaries;

    [Header("VR Fields")]
    public GameObject SDKManager;
    public GameObject VRUpdateUI;
    public GameObject controllers;
    public GameObject teleport;
    public GameObject armory;
    public GameObject VRShopScripts;
    public GameObject VRCanvas;
    public GameObject VRWaveConsole;
    public GameObject VRBoundaries;
    public GameObject VRPrices;

    private void Start () {
        if (VRConfig.VREnabled)
        {
            noVRPlayer.SetActive(false);
            canvas.SetActive(false);
            updateUI.SetActive(false);
            boundaries.SetActive(false);
            shopScripts.SetActive(false);

            VRCanvas.SetActive(true);
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

            VRCanvas.SetActive(false);
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
}
