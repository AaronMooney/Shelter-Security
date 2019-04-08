using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    public int health;


	// Use this for initialization
	private void Start () {
        health = 100;
	}
	
	// Update is called once per frame
	void Update () {
        if (health > 100) health = 100;
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
