using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * Aaron Mooney
 * 
 * WeaponShoot script that handles shooting and toggling the scope
 * */
public class WeaponShoot : MonoBehaviour
{
    [Header("VR Fields")]
    public VRTK.VRTK_ControllerEvents controllerEvents;
    public bool VR = false;

    public enum FireType { Ray, VRNonScoped, Sniper, PlasmaSniper, Launcher };
    [Header("Weapon Type")]
    public FireType fireType;

    [Header("Weapon Stats")]
    [SerializeField] private float damagePerShot;
    [SerializeField] private float fireRate;
    [SerializeField] private float range;
    [SerializeField] private int maxAmmo;
    [SerializeField] private float reloadTime;
    public float scopedFov = 15f;
    public int currentAmmo;

    [Header("Referenced Objects")]
    public Animator animator;
    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject scopeOverlay;
    public GameObject weaponCamera;
    public ParticleSystem gunParticles;
    public Camera fpsCam;
    public GameObject impactEffect;

    private bool isReloading = false;
    private float normalFov;
    private float timer;
    private Ray shootRay = new Ray();
    private RaycastHit shootHit;
    private AudioSource gunAudio;
    KeyCode fireKey = KeyCode.Mouse0;
    private AnimatorStateInfo info; 

    // Toggle Scope event
    public delegate void OnScopedChangedDelegate(bool newBool);
    public event OnScopedChangedDelegate OnScopedChanged;
    private bool m_isScoped = false;
    // IsScoped property that returns m_isScoped boolean and calls OnScopedChanged event
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

    // Use this for initialization
    void Start()
    {
        if (gameObject.name.Contains("VR")) VR = true;
        currentAmmo = maxAmmo;
        OnScopedChanged += ScopedChangedHandler;
        gunAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!VR)
        {
            info = animator.GetCurrentAnimatorStateInfo(0);
        }

        timer += Time.deltaTime;

        if (isReloading) return;

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        // Right click to toggle scope
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            IsScoped = !IsScoped;
        }

        if (!isReloading && currentAmmo < maxAmmo && Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
            return;
        }

        // Check to make sure the player isnt carrying a turret before shooting
        if ((Input.GetKey(fireKey) && !GetComponentInParent<PickupAndPlace>().carrying))
        {
            Fire();
        }

        // If in VR then make sure that a weapon is grabbed
        if (VR && GetComponentInParent<VRTK.VRTK_InteractableObject>().IsGrabbed() && controllerEvents.triggerPressed)
        {
            Fire();
        }

    }

    private void FixedUpdate()
    {
        if (!VR)
        {
            AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
            // Disable shooting animation
            if (info.IsName("WeaponShoot")) animator.SetBool("Shoot", false);
        }
    }

    private void Fire()
    {
        if (timer >= fireRate)
        {
            if (fireType == FireType.Ray)
            {
                // Fire a raycast weapon if shops or menus are not open if not in VR
                if (!VR && !GetComponentInParent<PlayerActions>().gunShopActive && !GetComponentInParent<PlayerActions>().turretShopActive && !GetComponentInParent<PlayerActions>().menuActive)
                    ShootRay();
                // Fire a raycast weapon in VR if it is purchased
                else if (VR && GetComponentInParent<VRToggleWeapon>().isPurchased)
                    ShootRay();
            }
            else
            {
                // Fire a projectile weapon if shops or menus are not open if not in VR
                if (!VR && !GetComponentInParent<PlayerActions>().gunShopActive && !GetComponentInParent<PlayerActions>().turretShopActive && !GetComponentInParent<PlayerActions>().menuActive)
                    ShootProjectile();
                // Fire a projectile weapon in VR if it is purchased
                else if (VR && GetComponentInParent<VRToggleWeapon>().isPurchased)
                    ShootProjectile();
            }
        }
    }

    // Raycast weapon shoot method
    private void ShootRay()
    {
        // Set timer back to zero after shooting
        timer = 0f;

        // Play gun sounds, animation and particle effect
        gunAudio.Play();
        if (!VR) animator.SetBool("Shoot", true);

        gunParticles.Stop();
        gunParticles.Play();

        currentAmmo--;

        // Set origin and direction of raycast
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
            // If ray hits an enemy then deal damage
            if (shootHit.collider.tag == "Enemy" || shootHit.collider.tag == "Aerial")
            {
                shootHit.collider.GetComponent<EnemyHealth>().TakeDamage(damagePerShot);
            }
            // If the ray hits an object where the root has the enemy tag then deal damage
            else if (shootHit.collider.transform.root.tag == "Enemy" || shootHit.collider.transform.root.tag == "Aerial")
            {
                shootHit.collider.GetComponentInParent<EnemyHealth>().TakeDamage(damagePerShot);
            }

            // Create hit effect
            GameObject instance = (GameObject)Instantiate(impactEffect, shootHit.point, Quaternion.identity);
            Destroy(instance, 2f);
        }
    }

    // Fire projectile method
    private void ShootProjectile()
    {
        // Set timer to zero after firing
        timer = 0f;

        // if not in scope then play animation
        if (!info.IsName("SniperScoped") && !info.IsName("PlasmaScoped") && !info.IsName("LauncherScoped"))
        {
            if (!VR) animator.SetBool("Shoot", true);
        }

        // play gun sounds
        if (fireType == FireType.Launcher)
        {
            gunAudio.time = 0f;
            gunAudio.Play();
            gunAudio.SetScheduledEndTime(AudioSettings.dspTime + (0.55f - 0f));
        }
        else
        {
            gunAudio.Play();
        }

        // play particle effect
        gunParticles.Stop();
        gunParticles.Play();
        currentAmmo--;

        // Create projectile
        GameObject _proj = (GameObject)Instantiate(projectile, transform.position, transform.rotation);

        // Set force direction depending on whether the game is in VR or not
        if (fireType == FireType.Launcher)
        {
            Vector3 forceDir = new Vector3();
            if (VR) forceDir = Quaternion.AngleAxis(-1.5f, transform.right) * transform.forward;
            else forceDir = transform.forward;
            _proj.GetComponent<Rigidbody>().AddForce(forceDir * 4000);
        } else if (fireType == FireType.PlasmaSniper || fireType == FireType.Sniper)
        {   
            // Angle the direction if scoped to line projectile up with crosshair
            Vector3 forceDir = new Vector3();
            if (IsScoped || VR)
            {
                forceDir = Quaternion.AngleAxis(-1, transform.right) * transform.forward;
            }
            else
                forceDir = transform.forward;
            _proj.GetComponent<Rigidbody>().AddForce(forceDir * 6000);
        } else
        {
            _proj.GetComponent<Rigidbody>().AddForce(transform.forward * 4000);
        }
        // Create projectile and set its damage
        PlayerProjectile bullet = _proj.GetComponent<PlayerProjectile>();
        bullet.BulletDamage = damagePerShot;

    }

    // Reload Coroutine
    private IEnumerator Reload()
    {
        isReloading = true;
        // set animations if not in VR
        if (!VR) animator.SetBool("Reloading", true);
        yield return new WaitForSeconds(reloadTime - 0.25f);
        if (!VR) animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(0.25f);
        currentAmmo = maxAmmo;
        isReloading = false;
    }

    // Disable scope when swapping weapon
    private void OnDisable()
    {
        IsScoped = false;
    }

    // Disable scope, reset timer and set reloading to false when equipped
    void OnEnable()
    {
        IsScoped = false;
        timer = fireRate;
        isReloading = false;
        if (!VR) animator.SetBool("Reloading", false);
    }

    // Scope changed event handler
    private void ScopedChangedHandler(bool newBool)
    {
        if (!VR)
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
    }

    // Start scoping animation and call scope coroutine
    private void SetScoped(string animName, bool scoped)
    {
        animator.SetBool(animName, scoped);

        if (scoped)
            StartCoroutine(OnScoped());
        else
            OnUnScoped();
    }

    // Set overlay active and change camera fov
    private IEnumerator OnScoped()
    {
        yield return new WaitForSeconds(.15f);
        scopeOverlay.SetActive(true);
        weaponCamera.SetActive(false);

        normalFov = fpsCam.fieldOfView;
        fpsCam.fieldOfView = scopedFov;
    }

    // disable overlay and reset fov
    private void OnUnScoped()
    {
        scopeOverlay.SetActive(false);
        weaponCamera.SetActive(true);

        fpsCam.fieldOfView = normalFov;
    }
}
