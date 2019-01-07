using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public int maxHealth = 100;
    public int currentHealth;
    BoxCollider boxCollider;
    bool isDead;

    void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        currentHealth = maxHealth;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        print("hit");
        if (isDead)
            return;

        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;

        boxCollider.isTrigger = true;

        Destroy(gameObject);
    }
}
