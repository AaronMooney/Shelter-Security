using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class WeaponShoot : MonoBehaviour
{
    public VRTK.VRTK_ControllerEvents controllerEvents;

    public enum FireType { Ray, Sniper, PlasmaSniper, Launcher };
    public FireType fireType;

    public float damagePerShot = 10f;
    public float fireRate = 0.5f;
    public float range = 100f;

    public int maxAmmo = 10;
    private int currentAmmo;
    public float reloadTime = 1f;
    private bool isReloading = false;
    public Animator animator;
    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject scopeOverlay;
    public GameObject weaponCamera;

    public float scopedFov = 15f;
    private float normalFov;

    float timer;
    Ray shootRay = new Ray();
    RaycastHit shootHit;
    public ParticleSystem gunParticles;
    AudioSource gunAudio;
    public Light faceLight;
    float effectsDisplayTime = 0.2f;
    KeyCode fireKey = KeyCode.Mouse0;
    public Camera fpsCam;
    public GameObject impactEffect;
    public string enemyTag = "Enemy";
    private AnimatorStateInfo info;

    public delegate void OnScopedChangedDelegate(bool newBool);
    public event OnScopedChangedDelegate OnScopedChanged;
    private bool m_isScoped = false;
    public bool IsScoped
    {
        get { return m_isScoped; }
        set
        {
            if (m_isScoped == value) return;
            m_isScoped = value;
            if (OnScopedChanged != null)
                OnScopedChanged(m_isScoped);
        }
    }

    void Awake()
    {
        gunAudio = GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start()
    {
        currentAmmo = maxAmmo;
        OnScopedChanged += ScopedChangedHandler;
    }

    // Update is called once per frame
    void Update()
    {
        info = animator.GetCurrentAnimatorStateInfo(0);
        timer += Time.deltaTime;

        if (isReloading) return;

        if (currentAmmo <= 0)
        {
            
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            IsScoped = !IsScoped;
        }

        

        if (!isReloading && currentAmmo < maxAmmo && Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
            return;
        }

        if ((controllerEvents.triggerPressed || Input.GetKey(fireKey)) && timer >= fireRate && Time.timeScale != 0)
        {
            
            if (fireType == FireType.Ray)
            {
                ShootRay();
            } else
            {
                ShootProjectile();
            }
        }

    }

    private void FixedUpdate()
    {
        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);

        if (info.IsName("WeaponShoot")) animator.SetBool("Shoot", false);
    }

    public void DisableShooting()
    {
        animator.SetBool("Shooting", false);
    }

    void ShootRay()
    {
        timer = 0f;

        gunAudio.Play();
        animator.SetBool("Shoot", true);
        ////gunLight.enabled = true;
        ////faceLight.enabled = true;

        gunParticles.Stop();
        gunParticles.Play();

        //gunLine.enabled = true;
        //gunLine.SetPosition(0, transform.position);

        currentAmmo--;
        
        if (VRConfig.VREnabled)
        {
            shootRay.origin = transform.position;
            shootRay.direction = transform.forward;
        }
        else
        {
            shootRay.origin = fpsCam.transform.position;
            shootRay.direction = fpsCam.transform.forward;
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

    void ShootProjectile()
    {
        timer = 0f;

        if (!info.IsName("SniperScoped") && !info.IsName("PlasmaScoped") && !info.IsName("LauncherScoped"))
        {
            animator.SetBool("Shoot", true);
        }

        if (fireType == FireType.Launcher)
        {
            gunAudio.time = 0f;
            gunAudio.Play();
            gunAudio.SetScheduledEndTime(AudioSettings.dspTime + (0.55f - 0f));
        } else
        {
            gunAudio.Play();
        }

        ////gunLight.enabled = true;
        ////faceLight.enabled = true;

        gunParticles.Stop();
        gunParticles.Play();
        currentAmmo--;

        GameObject _proj = (GameObject)Instantiate(projectile, transform.position, transform.rotation);
        if (fireType == FireType.Launcher)
        {
            _proj.GetComponent<Rigidbody>().AddForce(transform.forward * 2000);
        } else
        {
            Vector3 forceDir = new Vector3();
            if (IsScoped)
                forceDir = Quaternion.AngleAxis(-1, transform.right) * transform.forward;
            else
                forceDir = transform.forward;
            _proj.GetComponent<Rigidbody>().AddForce(forceDir * 4000);
        }
        PlayerProjectile bullet = _proj.GetComponent<PlayerProjectile>();
        bullet.BulletDamage = damagePerShot;

    }

    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");
        animator.SetBool("Reloading", true);
        yield return new WaitForSeconds(reloadTime - 0.25f);
        animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(0.25f);
        currentAmmo = maxAmmo;
        isReloading = false;
    }

    private void OnDisable()
    {
        IsScoped = false;
    }

    void OnEnable()
    {
        IsScoped = false;
        timer = fireRate;
        isReloading = false;
        animator.SetBool("Reloading", false);
    }

    private void ScopedChangedHandler(bool newBool)
    {
        if (fireType == FireType.Launcher)
        {
            SetScoped("LauncherScoped", newBool);
        }
        else if (fireType == FireType.PlasmaSniper)
        {
            SetScoped("PlasmaScoped", newBool);
        }
        else if (fireType == FireType.Sniper)
        {
            SetScoped("SniperScoped", newBool);
        }
    }

    private void SetScoped(string animName, bool scoped)
    {
        animator.SetBool(animName, scoped);

        if (scoped)
            StartCoroutine(OnScoped());
        else
            OnUnScoped();
    }

    private IEnumerator OnScoped()
    {
        yield return new WaitForSeconds(.15f);
        scopeOverlay.SetActive(true);
        weaponCamera.SetActive(false);

        normalFov = fpsCam.fieldOfView;
        fpsCam.fieldOfView = scopedFov;
    }

    private void OnUnScoped()
    {
        scopeOverlay.SetActive(false);
        weaponCamera.SetActive(true);

        fpsCam.fieldOfView = normalFov;
    }
}
