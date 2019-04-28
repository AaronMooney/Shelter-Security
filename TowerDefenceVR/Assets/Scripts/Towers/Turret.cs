using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    private Transform target;

    [Header("Turret Stats")]
    public float range = 15f;
    public float fireRate = 1f;
    private float fireCooldown = 0f;
    public float damage;
    public float turnSpeed = 8;

    [Header("Setup Fields")]
    public bool useLaser = false;
    public LineRenderer lineRenderer;
    public ParticleSystem impact;
    public Light impactLight;
    public Transform swivel;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    private float laserRate = 0;


    // Use this for initialization
    private void Start () {
        // update target every half second
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
	}

    // Update current target
    private void UpdateTarget()
    {
        GameObject[] enemies;
        if (gameObject.tag == "AntiAir")
            enemies = GameObject.FindGameObjectsWithTag("Aerial");
        else
            enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // init shortest distance to infinity
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            if (!enemy.GetComponent<EnemyHealth>().isDead)
            {
                // calculate distance and overwrite shortest distance
                float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = enemy;
                }
            }
        }

        // set target to nearest enemy
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

        // if no target then return and disable laser
        if (target == null)
        {
            if (useLaser && lineRenderer.enabled)
            {
                lineRenderer.enabled = false;
                if (impact != null) impact.Stop();

                if (impactLight != null) impactLight.enabled = false;
            }
            return;
        }

        FollowTarget();


        if (useLaser)
        {
            Laser();
        }
        else
        {
            // shoot when ready and reset cooldown
            if (fireCooldown <= 0f)
            {
                Shoot();
                fireCooldown = 1f / fireRate;
            }

            fireCooldown -= Time.deltaTime;
        }

    }

    // Shoot method
    private void Shoot()
    {
        // Spawn bullet and set its damage
        GameObject newBullet = (GameObject)Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        TurretBullet bullet = newBullet.GetComponent<TurretBullet>();
        bullet.BulletDamage = damage;
        if(bullet != null)
        {
            bullet.Focus(target);
        }
    }

    // Rotate turret head to follow its target
    private void FollowTarget()
    {
        Vector3 direction = target.position - transform.position;
        Quaternion pointDirection = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(swivel.rotation, pointDirection, Time.deltaTime * turnSpeed).eulerAngles;

        if (gameObject.tag == "AntiAir")
            swivel.rotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z);
        else
            swivel.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    // Fire laser method
    private void Laser()
    {
        // play effects
        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            impact.Play();
            impactLight.enabled = true;
        }
        // set origin of laser
        lineRenderer.SetPosition(0, bulletSpawn.position);

        // set other end of laser to enemy
        Vector3 targetOffset = new Vector3(target.position.x, target.position.y + 1, target.position.z);
        lineRenderer.SetPosition(1, targetOffset);

        Vector3 direction = transform.position - targetOffset;

        // set impact effect potision and rotation
        impact.transform.position = targetOffset +  direction.normalized;
        impact.transform.rotation = Quaternion.LookRotation(direction);

        // Deal 10% of enemy health as damage each second
        EnemyHealth enemyHealth = target.gameObject.GetComponent<EnemyHealth>();
        if (laserRate > 1f)
        {
            enemyHealth.TakeDamage(enemyHealth.maxHealth * 0.1f);
            laserRate = 0;
        }
        laserRate += Time.deltaTime;
    }

    // helper method that draws the turret range in the scene
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
