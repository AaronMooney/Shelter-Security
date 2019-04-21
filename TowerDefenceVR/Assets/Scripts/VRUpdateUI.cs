﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;

public class VRUpdateUI : MonoBehaviour
{

    public Text waveNumber;
    public Text beginWaveText;
    public Text ammoRemainingText;
    public Text enemiesRemainingText;
    public Text coinAmountText;
    public VRTK_ControllerEvents controllerEvents;
    public VRWaveManager waveManager;
    public GameObject shop;


    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
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
        waveNumber.text = "Wave: " + (waveManager.round - 1);
        if (controllerEvents.GetComponent<VRTK_InteractGrab>().GetGrabbedObject() != null)
        {
            GameObject currentWeapon = controllerEvents.gameObject.GetComponent<VRTK_InteractGrab>().GetGrabbedObject();
            ammoRemainingText.text = currentWeapon.GetComponentInChildren<WeaponShoot>().currentAmmo.ToString();
        }

        coinAmountText.text = shop.GetComponent<VRShop>().coinBalance.ToString();
    }
}
