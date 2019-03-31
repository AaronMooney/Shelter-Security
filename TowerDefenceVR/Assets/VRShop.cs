using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class VRShop : MonoBehaviour {

    public GameObject rightController;
    public GameObject cannonMesh;
    public GameObject gatlingMesh;
    public GameObject missileLauncherMesh;
    public GameObject beamCannonMesh;
    public GameObject shockwaveMesh;
    public GameObject disruptionMesh;
    public GameObject selectedTurret;

    public GameObject cannon;

    public bool isCanonPurchased = false;
    public VRTK_RadialMenu menu;
    public Sprite image;
    private bool canPlace = false;

    private GameObject surfacePlotInstance;
    private VRTK_Pointer pointer;

    // Use this for initialization
    void Start () {
        pointer = rightController.GetComponent<VRTK_Pointer>();

    }
	
	// Update is called once per frame
	void Update () {
        
	}

    public void isPurchased()
    {
        if (isCanonPurchased)
        {
            menu.buttons[0].ButtonIcon = image;
            menu.RegenerateButtons();
        }
    }

    private void TogglePointer(GameObject g)
    {
        pointer.enabled = false;
        pointer.pointerRenderer.enabled = false;
        rightController.GetComponent<VRTK_BezierPointerRenderer>().customCursor = g;
        pointer.enabled = true;
        pointer.pointerRenderer.enabled = true;
    }

    public void SetCursorCanon()
    {
        if (isCanonPurchased)
        {
            TogglePointer(cannonMesh);
            selectedTurret = cannon;
        }
    }

    public void SetCursorGatling()
    {
        TogglePointer(gatlingMesh);
    }

    public void SetCursorBeamCanon()
    {
        TogglePointer(beamCannonMesh);
    }

    public void SetCursorMissile()
    {
        TogglePointer(missileLauncherMesh);
    }

    public void SetCursorShockwave()
    {
        TogglePointer(shockwaveMesh);
    }

    public void SetCursorDisrupt()
    {
        TogglePointer(disruptionMesh);
    }

    public void SpawnTurret()
    {
        if (selectedTurret != null)
        {

            GameObject[] objs = GameObject.FindGameObjectsWithTag("Turret");
            surfacePlotInstance = new GameObject();
            surfacePlotInstance.transform.position = pointer.pointerRenderer.GetDestinationHit().point;

            foreach (GameObject g in objs)
            {
                Debug.Log(g.name);
                if (Vector3.Distance(surfacePlotInstance.transform.position, g.transform.position) > 10)
                    canPlace = true;
                else
                    canPlace = false;
            }

            if (canPlace)
            {
                Vector3 newPos = new Vector3(surfacePlotInstance.transform.position.x, 0, surfacePlotInstance.transform.position.z);
                Instantiate(selectedTurret, newPos, Quaternion.identity);
            } else
            {
                Debug.Log("Turret too close");
            }
            Destroy(surfacePlotInstance);
        }
    }
}
