using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour {

    public bool roundActive = false;
    public GameObject spawner;

    public int round = 1;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log(roundActive);
            if (!roundActive)
            {
                roundActive = !roundActive;
                spawner.GetComponent<SpawnWave>().SetRoundLimit(round);
                round++;
            }
        }

        spawner.SetActive(roundActive);
    }

    public void EndRound()
    {
        roundActive = false;
    }
}
