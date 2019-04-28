using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserKatana : MonoBehaviour {

	public int damage = 30;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            collision.collider.GetComponent<EnemyHealth>().TakeDamage(damage);
            Debug.Log("sword");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyHealth>().TakeDamage(damage);
            Debug.Log("sword trigger");
        }
    }
}
