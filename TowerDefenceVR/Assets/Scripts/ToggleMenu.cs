using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ToggleMenu : MonoBehaviour {

    public GameObject menuCanvas;
    public bool menuActive;
	
	// Update is called once per frame
	private void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
        {
            menuActive = !menuActive;
        }

        menuCanvas.SetActive(menuActive);

    }

    public void Resume()
    {
        menuActive = false;
    }

    public void Quit()
    {
        SceneManager.LoadScene("Menu");
    }
}
