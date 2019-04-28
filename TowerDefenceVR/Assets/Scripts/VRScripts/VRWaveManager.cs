using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using VRTK.UnityEventHelper;

public class VRWaveManager : MonoBehaviour {

    public bool roundActive = false;
    public GameObject spawner;
    public int round = 1;

    private VRTK_Button_UnityEvents buttonEvents;

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
        spawner.SetActive(roundActive);
    }

    private void HandlePush(object sender, Control3DEventArgs e)
    {
        VRTK_Logger.Info("Pushed");

        if (!roundActive)
        {
            roundActive = !roundActive;
            spawner.GetComponent<SpawnWave>().SetRoundLimit(round);
            round++;
        }
    }

    public void EndRound()
    {
        roundActive = false;
    }
}
