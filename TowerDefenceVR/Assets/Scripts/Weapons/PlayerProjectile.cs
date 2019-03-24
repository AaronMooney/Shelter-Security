using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour {

    public enum ProjectileType {Rocket, Bullet};
    public ProjectileType projectileType;
    public AudioSource audioSource;

    public GameObject impactEffect;
    public float BulletDamage { get; set; }
    public float explosionRadius = 4;
    public Rigidbody rb;
    private bool hasHit = false;

    void FixedUpdate()
    {

        Destroy(gameObject, 5f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!hasHit)
        {
            GameObject instance = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(instance, 5f);

            if (projectileType == ProjectileType.Rocket)
            {
                Explode();
            }
            else
            {
                if (collision.gameObject.tag == "Enemy")
                {
                    collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(BulletDamage);
                    Debug.Log("enemy hit");
                }
            }
            Destroy(gameObject, 1);
        }

    }

    private void Explode()
    {
        hasHit = true;
        audioSource.Play();
        Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider col in hits)
        {
            if (col.tag == "Enemy")
            {
                Debug.Log("E");
                col.GetComponent<EnemyHealth>().TakeDamage(BulletDamage);
            }
        }
    }
}
