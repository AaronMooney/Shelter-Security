using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    private Transform target;
    public float range = 15f;
    public string enemyTag = "Enemy";

    public float fireRate = 1f;


    Ray shootRay = new Ray();
    RaycastHit shootHit;
    ParticleSystem fireParticles;
    LineRenderer fireLine;
    Light fireLight;
    float effectsDisplayTime = 0.2f;
    float timer;

    void Awake()
    {
        fireParticles = GetComponent<ParticleSystem>();
        fireLine = GetComponent<LineRenderer>();
        fireLight = GetComponent<Light>();
    }

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

        timer += Time.deltaTime;

        if (timer >= fireRate && Time.timeScale != 0 && target != null)
        {
            Shoot();

        }

        if (timer >= fireRate * effectsDisplayTime)
        {
            DisableEffects();
        }

    }

    public void DisableEffects()
    {
        fireLine.enabled = false;
        fireLight.enabled = false;
    }

    void Shoot()
    {
        timer = 0f;

        fireLight.enabled = true;
        fireLight.enabled = true;

        fireParticles.Stop();
        fireParticles.Play();

        fireLine.enabled = true;
        fireLine.SetPosition(0, transform.position);

        shootRay.origin = transform.position;
        var heading = target.position - transform.position;
        var distance = heading.magnitude;
        shootRay.direction = heading / distance;

        if (Physics.Raycast(shootRay, out shootHit, range))
        {
            EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(50);
            }
            fireLine.SetPosition(1, shootHit.point);
        }
        else
        {
            fireLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
