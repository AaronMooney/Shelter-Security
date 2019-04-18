using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneAttack : MonoBehaviour {

    public Transform bulletSpawn;
    public LineRenderer lineRenderer;
    public ParticleSystem impact;
    public Light impactLight;

    private float laserRate = 0;
    private GameObject target;
    private int damage;

    // Use this for initialization
    void Start () {
        damage = GetComponent<EnemyAI>().damage;
	}
	
    public void Laser()
    {
        if (!GetComponent<EnemyHealth>().isDead)
        {
            target = GetComponent<EnemyAI>().targetObject;
            if (!lineRenderer.enabled)
            {
                lineRenderer.enabled = true;
                impact.Play();
                impactLight.enabled = true;
            }
            lineRenderer.SetPosition(0, bulletSpawn.position);
            Vector3 targetOffset = new Vector3(target.transform.position.x, target.transform.position.y + 1, target.transform.position.z);
            lineRenderer.SetPosition(1, targetOffset);

            Vector3 direction = transform.position - targetOffset;

            impact.transform.position = targetOffset + direction.normalized;
            impact.transform.rotation = Quaternion.LookRotation(direction);
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
