using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using VRTK.UnityEventHelper;

/*
 * Aaron Mooney
 * 
 * VRWaveManager script that starts each round
 * */
public class VRWaveManager : MonoBehaviour {

    public bool roundActive = false;
    public GameObject spawner;
    public int round = 1;

    private VRTK_Button_UnityEvents buttonEvents;

    // Initialise button and add a listener to the OnPushed event
    private void Start()
    {
        buttonEvents = GetComponent<VRTK_Button_UnityEvents>();
        if (buttonEvents == null)
        {
            buttonEvents = gameObject.AddComponent<VRTK_Button_UnityEvents>();
        }
        buttonEvents.OnPushed.AddListener(HandlePush);
    }

    private void Update()
    {
        // enable the spawner while a round is active
        spawner.SetActive(roundActive);
    }

    // Called when button is pushed
    private void HandlePush(object sender, Control3DEventArgs e)
    {
        // Set round active and start spawning enemies
        if (!roundActive)
        {
            roundActive = !roundActive;
            spawner.GetComponent<SpawnWave>().SetRoundLimit(round);
            round++;
        }
    }

    // Ends the round
    public void EndRound()
    {
        roundActive = false;
    }
}
