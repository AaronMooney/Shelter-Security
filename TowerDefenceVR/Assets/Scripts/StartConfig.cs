using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class StartConfig : MonoBehaviour {


    public GameObject SDKManager;
    //public GameObject playArea;
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
            VRcanvas.SetActive(true);
            SDKManager.SetActive(true);
            //playArea.gameObject.SetActive(true);
            controllers.gameObject.SetActive(true);
            teleport.gameObject.SetActive(true);
            VRShop.gameObject.SetActive(true);
            armory.gameObject.SetActive(true);

        }
        else
        {
            noVRPlayer.SetActive(true);
            canvas.SetActive(true);
            VRcanvas.SetActive(false);
            SDKManager.SetActive(false);
            //playArea.gameObject.SetActive(false);
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
