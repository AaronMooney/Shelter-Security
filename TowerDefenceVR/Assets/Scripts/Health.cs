using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

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

    public void TakeDamage(int amount)
    {
        health -= amount;
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
