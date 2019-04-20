using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour {

    public bool roundActive = false;
    public GameObject spawner;
    public GameObject shopPanel;
    public bool shopActive = false;

    public int round = 1;

    private void Start()
    {
        LockCursor();
    }

    private void Update()
    {

        
        if (shopActive)
        {
            UnlockCursor();
        }
        else
        {
            LockCursor();
        }


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

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleShop();
        }

        

        spawner.SetActive(roundActive);
    }

    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void UnlockCursor()
    {
        if (Cursor.lockState != CursorLockMode.Confined)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
            
    }

    public void EndRound()
    {
        roundActive = false;
    }

    public void ToggleShop()
    {
        shopActive = !shopActive;
        shopPanel.SetActive(shopActive);
    }
}
