using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    private Transform target;

    [Header("Attributes")]
    public float range = 15f;
    public float fireRate = 1f;
    private float fireCooldown = 0f;
    public float damage;

    [Header("Setup Fields")]
    public string enemyTag = "Enemy";
    public Transform swivel;
    public float turnSpeed = 8;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;


    // Use this for initialization
    void Start () {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
	}

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        } else
        {
            target = null;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (target == null) return;

        Vector3 direction = target.position - transform.position;
        Quaternion pointDirection = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(swivel.rotation, pointDirection, Time.deltaTime * turnSpeed).eulerAngles;

        swivel.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (fireCooldown <= 0f)
        {
            Shoot();
            fireCooldown = 1f / fireRate;
        }

        fireCooldown -= Time.deltaTime;

    }

    void Shoot()
    {
        GameObject newBullet = (GameObject)Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        Bullet bullet = newBullet.GetComponent<Bullet>();
        bullet.BulletDamage = damage;
        if(bullet != null)
        {
            bullet.Focus(target);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
