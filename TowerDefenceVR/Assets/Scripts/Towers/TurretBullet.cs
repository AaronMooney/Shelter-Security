using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Aaron Mooney
 * 
 * TurretBullet script that handles events relating to bullets shot from turrets
 * */
public class TurretBullet : MonoBehaviour {

    private Transform target;

    [Header("BulletStats")]
    public float speed = 70f;
    public GameObject impactEffect;
    public float explosionRadius = 0;
    public float BulletDamage { get; set; }

    // Sets the target of a bullet
    public void Focus(Transform newTarget)
    {
        target = newTarget;
    }
	
	// Update is called once per frame
	private void Update () {
		
        // if there is no target, destroy the bullet
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = target.position - transform.position;

        // Test to see if a bullet has reached its target
        if (direction.magnitude <= speed * Time.deltaTime)
        {
            HitTarget();
            return;
        }

        // move bullet towards target
        transform.Translate(direction.normalized * (speed * Time.deltaTime), Space.World);
        transform.LookAt(target);

	}

    // Hit the target
    private void HitTarget()
    {
        // create impact
        GameObject instance = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(instance, 5f);
        Destroy(gameObject);

        // Explode or deal damage upon hitting an enemy
        if (target.gameObject.tag == "Enemy" || target.gameObject.tag == "Aerial")
        {
            if (explosionRadius > 0f)
            {
                Explode();
            }
            else
            {
                target.gameObject.GetComponent<EnemyHealth>().TakeDamage(BulletDamage);
            }
        }
    }

    // Explode method
    private void Explode()
    {
        // Deal damage to enemies within explosion radius
        Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider col in hits)
        {
            if (col.tag == "Enemy" || col.tag == "Aerial")
            {
                col.GetComponent<EnemyHealth>().TakeDamage(BulletDamage);
            }
        }
    }

    // Helper method that draws the range of the turret
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
