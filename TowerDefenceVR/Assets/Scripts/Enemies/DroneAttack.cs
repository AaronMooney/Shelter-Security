using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Aaron Mooney
 * 
 * DroneAttack script that handles the drones attack pattern
 * */
public class DroneAttack : MonoBehaviour {

    [Header("Components")]
    public Transform bulletSpawn;
    public LineRenderer lineRenderer;
    public ParticleSystem impact;
    public Light impactLight;

    private float laserRate = 0;
    private GameObject target;
    private int damage;

    // Use this for initialization
    private void Start () {
        damage = GetComponent<EnemyAI>().damage;
	}
	
    // Fire Laser
    public void Laser()
    {
        if (!GetComponent<EnemyHealth>().isDead)
        {
            // set target and enable effects
            target = GetComponent<EnemyAI>().targetObject;
            if (!lineRenderer.enabled)
            {
                lineRenderer.enabled = true;
                impact.Play();
                impactLight.enabled = true;
            }
            // set origin of laser to drone
            lineRenderer.SetPosition(0, bulletSpawn.position);

            // set other end to target
            Vector3 targetOffset = new Vector3(target.transform.position.x, target.transform.position.y + 1, target.transform.position.z);
            lineRenderer.SetPosition(1, targetOffset);

            Vector3 direction = transform.position - targetOffset;

            // spawn impact effect on target
            impact.transform.position = targetOffset + direction.normalized;
            impact.transform.rotation = Quaternion.LookRotation(direction);
            
            // Deal damage every second
            Health targetHealth = target.GetComponent<Health>();
            if (laserRate > 1f)
            {
                targetHealth.TakeDamage(damage);
                laserRate = 0;
            }
            laserRate += Time.deltaTime;
        }
    }
}
