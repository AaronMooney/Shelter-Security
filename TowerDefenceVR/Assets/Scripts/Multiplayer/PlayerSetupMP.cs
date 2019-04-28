using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
/*
 * Aaron Mooney
 * 
 * PlayerSetupMP script that initialises a player upon entering the game
 * */
public class PlayerSetupMP : NetworkBehaviour {

    [SerializeField] private Behaviour[] components;
    [SerializeField] private GameObject model;
    private Camera lobbyCam;

    private void Start()
    {
        lobbyCam = Camera.main;
        // set player that is not the local player to the Player2 layer to that their model can be seen by the local player
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

    // Toggle the lobby camera
    private void OnDisable()
    {
        if (lobbyCam != null){
            lobbyCam.gameObject.SetActive(true);
        }
    }


}
