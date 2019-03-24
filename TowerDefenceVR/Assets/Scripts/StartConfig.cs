using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class StartConfig : MonoBehaviour {


    public GameObject SDKManager;
    public GameObject weaponHolder;
    public GameObject scripts;
    public GameObject crosshair;
    public GameObject noVRPlayer;

    void Start () {

        Debug.Log(VRConfig.VREnabled);
        if (VRConfig.VREnabled)
        {
            noVRPlayer.SetActive(false);
            crosshair.SetActive(false);
            //GameObject.Find("[VRTK_SDKManager]").gameObject.SetActive(true);
            SDKManager.SetActive(true);
            weaponHolder.gameObject.SetActive(true);
            scripts.gameObject.SetActive(true);

        }
        else
        {
            noVRPlayer.SetActive(true);
            crosshair.SetActive(true);
            //GameObject.Find("[VRTK_SDKManager]").gameObject.SetActive(false);
            SDKManager.SetActive(false);
            weaponHolder.SetActive(false);
            scripts.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
    }
}
