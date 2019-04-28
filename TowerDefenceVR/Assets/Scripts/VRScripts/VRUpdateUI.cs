using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;
/*
 * Aaron Mooney
 * 
 * VRUpdateUI script that handles all UI updates while playing in VR.
 * */
public class VRUpdateUI : MonoBehaviour
{
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
    public VRTK_ControllerEvents controllerEvents;
    public VRWaveManager waveManager;
    public GameObject shop;
    public GameObject gate1;
    public GameObject gate2;
    public GameObject gate3;

    // Update is called once per frame
    void Update()
    {
        // If a round is in progress remove the begin wave text and update the enemies alive text.
        // Otherwise disable the enemies alive text and enable the begin wave text.
        if (waveManager.roundActive)
        {
            beginWaveText.enabled = false;
            enemiesRemainingText.enabled = true;
            int enemiesAlive = GameObject.FindGameObjectsWithTag("Enemy").Length + GameObject.FindGameObjectsWithTag("Aerial").Length;
            enemiesRemainingText.text = "Enemies Alive: " + enemiesAlive;
        }
        else
        {
            beginWaveText.enabled = true;
            beginWaveText.text = "Interact with console to start wave " + (waveManager.round);
            enemiesRemainingText.enabled = false;
        }

        // Update wave number, ammo and coins
        waveNumber.text = "Wave: " + (waveManager.round - 1);
        if (controllerEvents.GetComponent<VRTK_InteractGrab>().GetGrabbedObject() != null)
        {
            GameObject currentWeapon = controllerEvents.gameObject.GetComponent<VRTK_InteractGrab>().GetGrabbedObject();
            if (controllerEvents.GetComponent<VRTK_InteractGrab>().GetGrabbedObject().GetComponent<WeaponShoot>() != null)
                ammoRemainingText.text = currentWeapon.GetComponentInChildren<WeaponShoot>().currentAmmo.ToString();
        }

        coinAmountText.text = shop.GetComponent<VRShop>().coinBalance.ToString();

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
