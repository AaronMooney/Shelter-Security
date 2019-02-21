using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class StartConfig : MonoBehaviour {

    public bool isTopDownView;
    // Use this for initialization


    private void Awake()
    {
        //isTopDownView = false;

        //this.gameObject.AddComponent<VRTK_BasicTeleport>();
        //if (VRConfig.VREnabled)
        //{
        //    GameObject.Find("FPSController").gameObject.SetActive(false);
        //    GameObject.Find("[VRTK_SDKManager]").gameObject.SetActive(true);
        //    GameObject.Find("GunVR").gameObject.SetActive(true);
        //    GameObject.Find("VRTKScripts").gameObject.SetActive(true);

        //} else
        //{
        //    GameObject.Find("FPSController").gameObject.SetActive(true);
        //    GameObject.Find("[VRTK_SDKManager]").gameObject.SetActive(false);
        //    GameObject.Find("GunVR").gameObject.SetActive(false);
        //    GameObject.Find("VRTKScripts").gameObject.SetActive(false);
        //}
    }

    void Start () {

        Debug.Log(VRConfig.VREnabled);
        if (VRConfig.VREnabled)
        {
            GameObject.Find("FPSController").gameObject.SetActive(false);
            GameObject.Find("[VRTK_SDKManager]").gameObject.SetActive(true);
            GameObject.Find("GunVR").gameObject.SetActive(true);
            GameObject.Find("VRTKScripts").gameObject.SetActive(true);

        }
        else
        {
            GameObject.Find("FPSController").gameObject.SetActive(true);
            GameObject.Find("[VRTK_SDKManager]").gameObject.SetActive(false);
            GameObject.Find("GunVR").gameObject.SetActive(false);
            GameObject.Find("VRTKScripts").gameObject.SetActive(false);
        }
    }

    private void Update()
    {
    }
}
