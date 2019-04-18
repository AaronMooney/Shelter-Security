using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    public int health;


	// Use this for initialization
	private void Start () {
        health = 500;
	}
	
	// Update is called once per frame
	void Update () {
        if (health > 500) health = 500;
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
