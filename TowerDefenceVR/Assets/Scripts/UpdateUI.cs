using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Aaron Mooney
 * 
 * UpdateUI script that handles all UI updates while not playing in VR.
 * */

public class UpdateUI : MonoBehaviour {

    [Header("UI Text Attributes")]
    public Text waveNumber;
    public Text beginWaveText;
    public Text ammoRemainingText;
    public Text enemiesRemainingText;
    public Text coinAmountText;
    public Text gateOneText;
    public Text gateTwoText;
    public Text gateThreeText;

    [Header("Other Game Objects")]
    public GameObject player;
    public GameObject gate1;
    public GameObject gate2;
    public GameObject gate3;
    private PlayerActions playerActions;

	// Use this for initialization
	private void Start () {
        playerActions = player.GetComponent<PlayerActions>();
	}
	
	// Update is called once per frame
	private void Update () {

        // If a round is in progress remove the begin wave text and update the enemies alive text.
        // Otherwise disable the enemies alive text and enable the begin wave text.
		if (playerActions.roundActive)
        {
            beginWaveText.enabled = false;
            enemiesRemainingText.enabled = true;
            int enemiesAlive = GameObject.FindGameObjectsWithTag("Enemy").Length + GameObject.FindGameObjectsWithTag("Aerial").Length;
            enemiesRemainingText.text = "Enemies Alive: " + enemiesAlive;
        } else
        {
            beginWaveText.enabled = true;
            beginWaveText.text = "Press [ENTER] To Start Wave " + (playerActions.round);
            enemiesRemainingText.enabled = false;
        }

        // Update wave number, ammo and coins
        waveNumber.text = "Wave: " + (playerActions.round -1);
        GameObject currentWeapon = player.GetComponent<WeaponSelection>().currentWeapon;
        ammoRemainingText.text = currentWeapon.GetComponentInChildren<WeaponShoot>().currentAmmo.ToString();

        coinAmountText.text = player.GetComponent<PlayerActions>().coinBalance.ToString();

        // Update the health of each gate
        if (gate1 != null)
        {
            gateOneText.text = gate1.GetComponent<Health>().health + "/" + gate1.GetComponent<Health>().maxHealth;
        }
        if (gate2 != null)
        {
            gateTwoText.text = gate2.GetComponent<Health>().health + "/" + gate2.GetComponent<Health>().maxHealth;
        }
        if (gate3 != null)
        {
            gateThreeText.text = gate3.GetComponent<Health>().health + "/" + gate3.GetComponent<Health>().maxHealth;
        }
    }
}
