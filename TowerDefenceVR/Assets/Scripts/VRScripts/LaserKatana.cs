using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Aaron Mooney
 * 
 * LaserKatana script that handles collision with the sword
 * 
 * */
public class LaserKatana : MonoBehaviour {

	public int damage = 30;

    // If an enemy collides with this deal damage
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyHealth>().TakeDamage(damage);
        }
    }
}
