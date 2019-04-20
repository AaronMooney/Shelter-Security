using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PickupAndPlace : MonoBehaviour {

    public GameObject playerCamera;
    public bool carrying = false;
    public float distance = 3;
    GameObject selectedTurretMesh;
    GameObject selectedTurret;
    private bool canPlace = false;
   
	
	// Update is called once per frame
	void Update () {
		if (carrying)
        {
            Carry(selectedTurretMesh);
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Invoke("DropTurret",0.2f);
            }
        }
	}

    public void Carry(GameObject g)
    {
        Vector3 carryPos = playerCamera.transform.position + playerCamera.transform.forward * distance;
        g.transform.position = new Vector3(carryPos.x, 0f, carryPos.z);
    }

    private void DropTurret()
    {
        CheckPlacement();
        if (canPlace) {
            Vector3 pos = selectedTurretMesh.transform.position;
            Instantiate(selectedTurret, pos, Quaternion.identity);
            Destroy(selectedTurretMesh);
            carrying = false;
        }
    }

    public void Pickup(GameObject mesh, GameObject turret)
    {
        carrying = true;
        selectedTurretMesh = (GameObject)Instantiate(mesh);
        selectedTurret = turret;

    }

    private void CheckPlacement()
    {
        GameObject[] turrets = GameObject.FindGameObjectsWithTag("Turret");
        GameObject[] antiairs = GameObject.FindGameObjectsWithTag("AntiAir");

        GameObject[] objs = turrets.Concat(antiairs).ToArray();
        if (objs.Length == 0) canPlace = true;
        Debug.Log("checking");
        foreach (GameObject g in objs)
        {
            Debug.Log(g.name);
            if (Vector3.Distance(selectedTurretMesh.transform.position, g.transform.position) > 10)
                canPlace = true;
            else
                canPlace = false;
        }
    }
}
