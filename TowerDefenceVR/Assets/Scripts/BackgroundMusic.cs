using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Aaron Mooney
 * 
 * BackgroundMusic script that implements a singleton pattern.
 * This script is attached to a game object and is set to not destroy on scene changes.
 * The singleton pattern ensures that only one instance of the music can play at any one time.
 * 
 * */
public class BackgroundMusic : MonoBehaviour {

    private static BackgroundMusic instance = null;

    public static BackgroundMusic Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        // Check if instance exists and that it isnt this one
        if (instance != null && instance != this) {
            // If it exists destroy it
            Destroy(this.gameObject);
            return;
        } else {
            // If it doesnt exist then set it to this
            instance = this;
        }
        // Set this object to not be destroyed on scene changes.
        DontDestroyOnLoad(this.gameObject);
    }
}
