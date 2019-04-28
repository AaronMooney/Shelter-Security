using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/*
 * Aaron Mooney
 * 
 * PickupAndPlace script that picks up and carries turrets after purchase and places them on a valid location on the ground.
 * 
 * */

public class PickupAndPlace : MonoBehaviour {

    public GameObject playerCamera;
    public bool carrying = false;

    // Distance in front of the player that a turret is carried
    public float distance = 3;

    private GameObject selectedTurretMesh;
    private GameObject selectedTurret;
    private bool canPlace = false;
   
	
	// Update is called once per frame
	void Update () {

        // Carry a turret until asked to drop
		if (carrying)
        {
            Carry(selectedTurretMesh);

            // If left mouse is clicked, spawn a turret in 0.2 seconds.
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Invoke("DropTurret",0.2f);
            }
        }
	}

    // Carry a turret mesh at a distance in front of the player, keeping the turret on the ground.
    public void Carry(GameObject g)
    {
        Vector3 carryPos = playerCamera.transform.position + playerCamera.transform.forward * distance;
        g.transform.position = new Vector3(carryPos.x, 0f, carryPos.z);
    }

    // Spawn a turret at the mesh position and destroy the mesh
    private void DropTurret()
    {
        CheckPlacement();
        if (canPlace) {
            Vector3 pos = selectedTurretMesh.transform.position;
            Instantiate(selectedTurret, pos, Quaternion.identity);
            //UpdateEnemyPaths();
            Destroy(selectedTurretMesh);
            carrying = false;
        }
    }

    // Pick up a turret and set carrying to true
    public void Pickup(GameObject mesh, GameObject turret)
    {
        carrying = true;
        selectedTurretMesh = (GameObject)Instantiate(mesh);
        selectedTurret = turret;

    }

    // Check if the mesh position is a valid place to spawn a turret
    private void CheckPlacement()
    {
        // Create an array containing all turrets
        GameObject[] turrets = GameObject.FindGameObjectsWithTag("Turret");
        GameObject[] antiairs = GameObject.FindGameObjectsWithTag("AntiAir");

        GameObject[] objs = turrets.Concat(antiairs).ToArray();

        // location is valid if no turrets exist yet
        if (objs.Length == 0) canPlace = true;
        
        // loop through all turrets and if the distance between the current position and another turret is greater than 10 the location is valid
        foreach (GameObject g in objs)
        {
            if (Vector3.Distance(selectedTurretMesh.transform.position, g.transform.position) > 10)
                canPlace = true;
            else
                canPlace = false;
        }
    }


    // Method that updated enemy paths, currently unused due to a bug with some enemies freezing.
    private void UpdateEnemyPaths()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length > 0 || GameObject.FindGameObjectsWithTag("Aerial").Length > 0)
        {
            GameObject[] groundUnits = GameObject.FindGameObjectsWithTag("Enemy");
            GameObject[] drones = GameObject.FindGameObjectsWithTag("Aerial");

            GameObject[] enemies = groundUnits.Concat(drones).ToArray();

            // loop through all enemies and update their path
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].GetComponent<EnemyAI>().UpdatePath();
            }
        }
    }
}
