using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBullet : MonoBehaviour {

    private Transform target;
    public float speed = 70f;
    public GameObject impactEffect;
    public float BulletDamage { get; set; }
    public float explosionRadius = 0;

    public void Focus(Transform newTarget)
    {
        target = newTarget;
    }
	
	// Update is called once per frame
	void Update () {
		
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = target.position - transform.position;

        if (direction.magnitude <= speed * Time.deltaTime)
        {
            HitTarget();
            return;
        }

        transform.Translate(direction.normalized * (speed * Time.deltaTime), Space.World);
        transform.LookAt(target);

	}

    void HitTarget()
    {
        GameObject instance = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(instance, 5f);
        Destroy(gameObject);
        if (target.gameObject.tag == "Enemy")
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

    void Explode()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider col in hits)
        {
            if (col.tag == "Enemy")
            {
                col.GetComponent<EnemyHealth>().TakeDamage(BulletDamage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
