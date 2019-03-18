using System.Collections;
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

    // Use this for initialization
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        //StartCoroutine("Stun");
    }

    //Issue lies in this funtion
    void UpdateTarget()
    {
        //GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        //foreach (GameObject enemy in enemies)
        //{
        //    float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
        //    if (enemy.name == "Diamond Lord (2)") Debug.Log("D: " + distanceToEnemy + ", range: " + range);
        //    if (distanceToEnemy <= range)
        //    {
        //        targets.Add(enemy.transform);
        //    }
        //    else
        //    {
        //        if (targets.Contains(enemy.transform))
        //        {
        //            targets.Remove(enemy.transform);
        //        }
        //    }
        //}

        //Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);
        //List<Transform> transforms = new List<Transform>();
        //foreach (Collider col in hitColliders){
        //    if (col.tag.Contains("Enemy"))
        //    {
        //        transforms.Add(col.transform);
        //        Debug.Log("COL: " + col.name);
        //    }
        //}
        //targets = transforms;

    }

    // Update is called once per frame
    void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);
        List<Transform> transforms = new List<Transform>();
        foreach (Collider col in hitColliders)
        {
            if (col.tag.Contains("Enemy"))
            {
                transforms.Add(col.transform);
                Debug.Log("COL: " + col.name);
            }
        }
        targets = transforms;

        timer += Time.deltaTime;
        //works if i remove targets.count
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
            //Debug.Log(hits[i].collider.name);
            if (!hits[i].collider.tag.Contains("Enemy")) continue;
            if (hits[i].collider.GetComponent<EnemyAI>().isImmune || hits[i].collider.GetComponent<EnemyAI>().stunned) continue;
            targets.Add(hits[i].collider.transform);
            hits[i].collider.GetComponent<EnemyAI>().stunned = true;
            hits[i].collider.GetComponent<EnemyAI>().isImmune = true;
            hits[i].collider.GetComponent<EnemyAI>().StunTime = 3f;

        }
        //if (Physics.SphereCastAll(origin, range, transform.forward, out hit, range ))
        //{
        //  if (hit.collider.tag.Contains("Enemy"))
        //{
        //EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
        //if (enemyHealth != null)
        //{
        //  enemyHealth.TakeDamage(50);
        //}
        //  hit.collider.GetComponent<EnemyAI>().stunned = true;
        //StopCoroutine("Stun");
        //StartCoroutine("Stun", hit.collider);
        //Debug.Log(hit.collider.name);
        // }
        //}
    }

    //IEnumerator Stun()
    //{
    //    while (true)
    //    {
    //        foreach (Transform enemyTransform in targets)
    //        {
    //            Debug.Log("Name: " + enemyTransform.gameObject.name);
    //            if (!enemyTransform.GetComponent<EnemyAI>().stunned) continue;

    //            enemyTransform.GetComponent<EnemyAI>().StunTime -= Time.deltaTime;
    //            if (enemyTransform.GetComponent<EnemyAI>().StunTime <= 0.5f)
    //            {
    //                enemyTransform.GetComponent<EnemyAI>().StunTime = 0;
    //                enemyTransform.GetComponent<EnemyAI>().stunned = false;
    //                enemyTransform.GetComponent<EnemyAI>().isImmune = true;

    //            }
    //        }
    //        yield return null;

    //}

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
