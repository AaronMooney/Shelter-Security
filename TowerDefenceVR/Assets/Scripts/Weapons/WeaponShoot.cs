using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class WeaponShoot : MonoBehaviour
{
    public VRTK.VRTK_ControllerEvents controllerEvents;

    public float damagePerShot = 10f;
    public float fireRate = 0.5f;
    public float range = 100f;

    float timer;
    Ray shootRay = new Ray();
    RaycastHit shootHit;
    ParticleSystem gunParticles;
    LineRenderer gunLine;
    AudioSource gunAudio;
    Light gunLight;
    public Light faceLight;
    float effectsDisplayTime = 0.2f;
    bool canShoot = true;
    KeyCode fireKey = KeyCode.Mouse0;
    private string fpsCam = "PlayerCamera";
    public GameObject impactEffect;
    public float damage;
    public string enemyTag = "Enemy";

    void Awake()
    {
        gunParticles = GetComponent<ParticleSystem>();
        gunLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        //gunLight = GetComponent<Light>();
    }

    // Use this for initialization
    void Start()
    {
        DisableEffects();
    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;

        if ((controllerEvents.triggerPressed || Input.GetKey(fireKey)) && canShoot && timer >= fireRate && Time.timeScale != 0)
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
        gunLine.enabled = false;
        //faceLight.enabled = false;
        //gunLight.enabled = false;
    }

    void Shoot()
    {
        timer = 0f;

        gunAudio.Play();

        ////gunLight.enabled = true;
        ////faceLight.enabled = true;

        //gunParticles.Stop();
        //gunParticles.Play();

        //gunLine.enabled = true;
        //gunLine.SetPosition(0, transform.position);

        
        if (VRConfig.VREnabled)
        {
            shootRay.origin = transform.position;
            shootRay.direction = transform.forward;
        }
        else
        {
            shootRay.origin = GameObject.Find(fpsCam).transform.position;
            shootRay.direction = GameObject.Find(fpsCam).transform.forward;
        }

        if (Physics.Raycast(shootRay, out shootHit, range))
        {
            Debug.DrawRay(shootRay.origin, shootRay.direction);
            if (shootHit.collider.tag == enemyTag)
            {
                shootHit.collider.GetComponent<EnemyHealth>().TakeDamage(damagePerShot);
            }
            //gunLine.SetPosition(1, shootHit.point);
            GameObject instance = (GameObject)Instantiate(impactEffect, shootHit.point, Quaternion.identity);
            Destroy(instance, 2f);
        }
        else
        {
            //gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
    }
}
