using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Aaron Mooney
 * 
 * Health script that manages the health of a friendly object
 * 
 * */

public class Health : MonoBehaviour {

    [Header("Stats")]
    public int health;
    public int maxHealth = 500;


	// Use this for initialization
	private void Start () {
        health = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
        if (health > maxHealth) health = maxHealth;
        if (health <= 0) Die();
	}

    // Take damage and reduce health
    public void TakeDamage(int amount)
    {
        health -= amount;
    }

    // Die and destroy self
    public void Die()
    {
        GameObject.Find("LoseGame").GetComponent<LoseGame>().Lose();
        Destroy(gameObject);
    }
}
