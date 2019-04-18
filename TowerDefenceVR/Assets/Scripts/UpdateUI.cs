using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateUI : MonoBehaviour {

    public Text waveNumber;
    public Text beginWaveText;
    public Text ammoRemainingText;
    public Text EnemiesRemainingText;
    public GameObject player;
    private PlayerActions playerActions;
    private bool VR;

	// Use this for initialization
	void Start () {
        playerActions = player.GetComponent<PlayerActions>();
        VR = VRConfig.VREnabled;
	}
	
	// Update is called once per frame
	void Update () {
		if (playerActions.roundActive)
        {
            beginWaveText.enabled = false;
            EnemiesRemainingText.enabled = true;
            int enemiesAlive = GameObject.FindGameObjectsWithTag("Enemy").Length + GameObject.FindGameObjectsWithTag("Aerial").Length;
            EnemiesRemainingText.text = "Enemies Alive: " + enemiesAlive;
        } else
        {
            beginWaveText.enabled = true;
            if (!VR)
                beginWaveText.text = "Press [ENTER] To Start Wave " + (playerActions.round);
            EnemiesRemainingText.enabled = false;
        }
        waveNumber.text = "Wave: " + (playerActions.round -1);
        GameObject currentWeapon = player.GetComponent<WeaponSelection>().currentWeapon;
        ammoRemainingText.text = currentWeapon.GetComponentInChildren<WeaponShoot>().currentAmmo.ToString();
	}
}
