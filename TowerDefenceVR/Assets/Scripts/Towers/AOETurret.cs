using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Aaron Mooney
 * 
 * AOETurret script that handles events relating to area of effect turrets
 * */
public class AOETurret : MonoBehaviour
{
    public enum TowerType { Stun, Slow, Damage };

    [Header("Tower Stats")]
    public TowerType towerType;
    public float range = 15f;
    public string enemyTag = "Enemy";
    public float fireRate = 2.5f;

    private Ray shootRay = new Ray();
    private RaycastHit shootHit;
    private ParticleSystem fireParticles;
    private List<Transform> targets;
    private float effectsDisplayTime = 0.2f;

    private float stunImmunityTime = 2f;
    private float timer;

    private void Awake()
    {
        fireParticles = GetComponent<ParticleSystem>();
        targets = new List<Transform>();
    }

    // Update is called once per frame
    private void Update()
    {
        // create a collider array with any objects colliding with the cast sphere
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);
        List<Transform> transforms = new List<Transform>();

        // loop through the colliders and add enemies to transforms list
        foreach (Collider col in hitColliders)
        {
            if (col.tag == enemyTag)
            {
                if (!col.GetComponent<EnemyHealth>().isDead)
                {
                    transforms.Add(col.transform);
                }
            }
        }
        targets = transforms;

        timer += Time.deltaTime;

        // Fire when possible and if targets exist
        if (timer >= fireRate && targets.Count > 0)
        {
            Shoot();
        }

    }

    // Apply effects
    void Shoot()
    {
        // set timer to 0 and play particle effect
        timer = 0f;

        fireParticles.Stop();
        fireParticles.Play();

        // raycast a sphere
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, range, transform.forward);

        for (int i = 0; i < hits.Length; i++)
        {
            // continue of not an enemy
            if (hits[i].collider.tag != enemyTag) continue;

            // check if enemies are immune and stun them if they are not
            if (towerType == TowerType.Stun)
            {
                if (hits[i].collider.GetComponent<EnemyAI>().isImmune || hits[i].collider.GetComponent<EnemyAI>().stunned) continue;
                targets.Add(hits[i].collider.transform);
                hits[i].collider.GetComponent<EnemyAI>().stunned = true;
                hits[i].collider.GetComponent<EnemyAI>().isImmune = true;

            }
            // check if enemies are immune and slow them if they are not
            else if (towerType == TowerType.Slow)
            {
                if (hits[i].collider.GetComponent<EnemyAI>().isImmune || hits[i].collider.GetComponent<EnemyAI>().isSlowed) continue;
                targets.Add(hits[i].collider.transform);
                hits[i].collider.GetComponent<EnemyAI>().isSlowed = true;
                hits[i].collider.GetComponent<EnemyAI>().isImmune = true;
                hits[i].collider.GetComponent<EnemyAI>().moveSpeed = hits[i].collider.GetComponent<EnemyAI>().moveSpeed / 2;
            }

        }
        
    }

    // helper method that draws the range of the turret in the scene
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
