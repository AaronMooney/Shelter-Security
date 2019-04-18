using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour {

    public bool roundActive = false;
    public GameObject spawner;

    private int roundLimit = 1;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log(roundActive);
            Debug.Log(roundLimit);
            if (!roundActive)
            {
                roundActive = !roundActive;
                spawner.GetComponent<SpawnWave>().SetRoundLimit(roundLimit);
                roundLimit ++;
            }
        }

        spawner.SetActive(roundActive);
    }

    public void EndRound()
    {
        roundActive = false;
    }
}
