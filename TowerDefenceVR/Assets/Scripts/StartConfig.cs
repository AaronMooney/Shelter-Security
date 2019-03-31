using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using UnityEngine.Networking;

public class StartConfig : NetworkBehaviour {


    public GameObject SDKManager;
    public GameObject playArea;
    public GameObject controllers;
    public GameObject teleport;
    public GameObject armory;
    public GameObject VRShop;
    public GameObject crosshair;
    public GameObject noVRPlayer;

    private NetworkClient myClient;

    void Start () {

        

        Debug.Log(VRConfig.VREnabled);
        if (VRConfig.VREnabled)
        {
            //noVRPlayer.SetActive(false);
            crosshair.SetActive(false);
            SDKManager.SetActive(true);
            playArea.gameObject.SetActive(true);
            controllers.gameObject.SetActive(true);
            teleport.gameObject.SetActive(true);
            VRShop.gameObject.SetActive(true);
            armory.gameObject.SetActive(true);

        }
        else
        {
            //noVRPlayer.SetActive(true);
            crosshair.SetActive(true);

            SDKManager.SetActive(false);
            playArea.gameObject.SetActive(false);
            controllers.gameObject.SetActive(false);
            teleport.gameObject.SetActive(false);
            VRShop.gameObject.SetActive(false);
            armory.gameObject.SetActive(false);

        }
    }

    private void Update()
    {
    }
}
