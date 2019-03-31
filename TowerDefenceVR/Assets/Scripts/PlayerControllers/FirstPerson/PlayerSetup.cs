using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour {

    [SerializeField] private Behaviour[] components;
    [SerializeField] private GameObject model;
    private Camera lobbyCam;

    private void Start()
    {
        lobbyCam = Camera.main;
        if (!isLocalPlayer)
        {
            model.layer = 10;
            Transform [] children = model.GetComponentsInChildren<Transform>();
            foreach (Transform go in children)
            {
                go.gameObject.layer = 10;
            }
            for (int i = 0; i < components.Length; i++)
            {
                components[i].enabled = false;
            }
        } else
        {
            if (lobbyCam != null){
                lobbyCam.gameObject.SetActive(false);
            }
        }
        
    }



    private void OnDisable()
    {
        if (lobbyCam != null){
            lobbyCam.gameObject.SetActive(true);
        }
    }


}
