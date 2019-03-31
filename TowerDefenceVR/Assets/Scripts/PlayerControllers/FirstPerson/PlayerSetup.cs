using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour {

    [SerializeField]
    Behaviour[] components;

    private void Start()
    {
        if (!isLocalPlayer)
        {
            gameObject.layer = 10;
            for (int i = 0; i < components.Length; i++)
            {
                components[i].enabled = false;
            }
        }
    }
}
