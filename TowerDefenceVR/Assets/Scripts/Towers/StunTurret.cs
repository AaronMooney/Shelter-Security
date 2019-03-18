﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunTurret : MonoBehaviour
{

    private List<Transform> targets;
    public float range = 15f;
    public string enemyTag = "Enemy";
    public float fireRate = 2.5f;


    Ray shootRay = new Ray();
    RaycastHit shootHit;
    ParticleSystem fireParticles;
    float effectsDisplayTime = 0.2f;

    float stunImmunityTime = 2f;
    float timer;

    void Awake()
    {
        fireParticles = GetComponent<ParticleSystem>();
        targets = new List<Transform>();
    }



    // Update is called once per frame
    void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);
        List<Transform> transforms = new List<Transform>();
        foreach (Collider col in hitColliders)
        {
            if (col.tag == enemyTag)
            {
                transforms.Add(col.transform);
            }
        }
        targets = transforms;

        timer += Time.deltaTime;

        if (timer >= fireRate && Time.timeScale != 0 && targets.Count > 0)
        {
            Shoot();
        }

    }

    void Shoot()
    {
        timer = 0f;

        fireParticles.Stop();
        fireParticles.Play();

        RaycastHit[] hits = Physics.SphereCastAll(transform.position, range, transform.forward);

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider.tag != enemyTag) continue;
            if (hits[i].collider.GetComponent<EnemyAI>().isImmune || hits[i].collider.GetComponent<EnemyAI>().stunned) continue;
            targets.Add(hits[i].collider.transform);
            hits[i].collider.GetComponent<EnemyAI>().stunned = true;
            hits[i].collider.GetComponent<EnemyAI>().isImmune = true;
            hits[i].collider.GetComponent<EnemyAI>().StunTime = 3f;

        }
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
