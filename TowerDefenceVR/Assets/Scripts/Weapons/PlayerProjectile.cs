using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Aaron Mooney
 * 
 * PlayerProjectile script that is attached to any player projectiles such as bullets and rockets.
 * */
public class PlayerProjectile : MonoBehaviour {


    public enum ProjectileType {Rocket, Bullet};

    [Header("Type and Components")]
    public ProjectileType projectileType;
    public AudioSource audioSource;
    public Rigidbody rb;
    public GameObject impactEffect;

    // Damage property used by shooting script
    public float BulletDamage { get; set; }

    [Header("Numeric Fields")]
    public float explosionRadius = 4;
    
    private bool hasHit = false;

    void FixedUpdate()
    {
        // Destroy the projectile in 5 seconds
        Destroy(gameObject, 5f);
    }

    // Check for collisions
    private void OnCollisionEnter(Collision collision)
    {
        // check if it has not already hit and if the tag of the collider is not Weapons or Player
        if (!hasHit && collision.collider.tag != "Weapons" && collision.collider.tag != "Player")
        {
            // Create the hit effect and destroy it
            GameObject instance = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(instance, 5f);

            // Explode if the projectile is a rocket
            if (projectileType == ProjectileType.Rocket)
            {
                Explode();
            }
            else
            {
                // Otherwise check if it hit an enemy and deal damage
                if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Aerial")
                {
                    collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(BulletDamage);
                }
            }
            Destroy(gameObject, 1);
        }

    }

    private void Explode()
    {
        // Explode and deal damage to any enemy in the explosion radius
        hasHit = true;
        audioSource.Play();
        Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider col in hits)
        {
            if (col.tag == "Enemy" || col.tag == "Aerial")
            {
                col.GetComponent<EnemyHealth>().TakeDamage(BulletDamage);
            }
        }
    }
}
