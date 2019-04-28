using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/*
 * Aaron Mooney
 * 
 * LoseGame script that handles the event where the player loses the game
 * */
public class LoseGame : MonoBehaviour {

    [Header("VR enabled objects")]
    public GameObject losePanel;
    public GameObject losePanelVR;

    public void Lose()
    {
        if (!VRConfig.VREnabled)
            losePanel.SetActive(true);
        else
            losePanelVR.SetActive(true);

        Invoke("ReturnToMenu", 5f);
    }

    private void ReturnToMenu()
    {
        UnlockCursor();
        SceneManager.LoadScene("Menu");
    }

    // Show cursor and unlock it
    public void UnlockCursor()
    {
        if (Cursor.lockState != CursorLockMode.Confined)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }

    }
}
