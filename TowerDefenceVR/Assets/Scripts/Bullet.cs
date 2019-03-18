using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private Transform target;
    public float speed = 70f;
    public GameObject impactEffect;
    public float BulletDamage { get; set; }

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


	}

    void HitTarget()
    {
        GameObject instance = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(instance, 2f);
        Destroy(gameObject);
        if (target.gameObject.tag.Contains("Enemy"))
        {
            target.gameObject.GetComponent<EnemyHealth>().TakeDamage(BulletDamage);
        }
    }
}
