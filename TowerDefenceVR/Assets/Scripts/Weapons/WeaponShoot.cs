using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Original weapon class before implemnenting multiplayer
 * This class is still used for VR
 * */
public class WeaponShoot : MonoBehaviour
{
    public VRTK.VRTK_ControllerEvents controllerEvents;
    public bool VR = false;

    public enum FireType { Ray, VRNonScoped, Sniper, PlasmaSniper, Launcher };
    public FireType fireType;

    public float damagePerShot = 10f;
    public float fireRate = 0.5f;
    public float range = 100f;
    public int maxAmmo = 10;
    public int currentAmmo;
    public float reloadTime = 1f;
    private bool isReloading = false;
    public float scopedFov = 15f;
    private float normalFov;

    public Animator animator;
    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject scopeOverlay;
    public GameObject weaponCamera;

    float timer;
    Ray shootRay = new Ray();
    RaycastHit shootHit;
    public ParticleSystem gunParticles;
    private AudioSource gunAudio;
    KeyCode fireKey = KeyCode.Mouse0;
    public Camera fpsCam;
    public GameObject impactEffect;
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

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            IsScoped = !IsScoped;
        }

        

        if (!isReloading && currentAmmo < maxAmmo && Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
            return;
        }

        if ((Input.GetKey(fireKey) && !GetComponentInParent<PickupAndPlace>().carrying))
        {

            Fire();
        }

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

            if (info.IsName("WeaponShoot")) animator.SetBool("Shoot", false);
        }
    }

    private void Fire()
    {
        if (timer >= fireRate && Time.timeScale != 0)
        {
            if (fireType == FireType.Ray)
            {
                if (!VR && !GetComponentInParent<PlayerActions>().gunShopActive && !GetComponentInParent<PlayerActions>().turretShopActive && !GetComponentInParent<PlayerActions>().menuActive)
                    ShootRay();
                else if (VR && GetComponentInParent<VRToggleWeapon>().isPurchased)
                    ShootRay();
            }
            else
            {
                if (!VR && !GetComponentInParent<PlayerActions>().gunShopActive && !GetComponentInParent<PlayerActions>().turretShopActive && !GetComponentInParent<PlayerActions>().menuActive)
                    ShootProjectile();
                else if (VR && GetComponentInParent<VRToggleWeapon>().isPurchased)
                    ShootProjectile();
            }
        }
    }



    private void ShootRay()
    {
        timer = 0f;

        gunAudio.Play();
        if (!VR) animator.SetBool("Shoot", true);

        gunParticles.Stop();
        gunParticles.Play();


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
            if (shootHit.collider.tag == "Enemy" || shootHit.collider.tag == "Aerial")
            {
                shootHit.collider.GetComponent<EnemyHealth>().TakeDamage(damagePerShot);
            } else if (shootHit.collider.transform.root.tag == "Enemy" || shootHit.collider.transform.root.tag == "Aerial")
            {
                shootHit.collider.GetComponentInParent<EnemyHealth>().TakeDamage(damagePerShot);
            }

            GameObject instance = (GameObject)Instantiate(impactEffect, shootHit.point, Quaternion.identity);
            Destroy(instance, 2f);
        }
    }

    void ShootProjectile()
    {

        Debug.Log("firew");
        timer = 0f;

        if (!info.IsName("SniperScoped") && !info.IsName("PlasmaScoped") && !info.IsName("LauncherScoped"))
        {
            if (!VR) animator.SetBool("Shoot", true);
        }

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


        gunParticles.Stop();
        gunParticles.Play();
        currentAmmo--;


        GameObject _proj = (GameObject)Instantiate(projectile, transform.position, transform.rotation);
        if (fireType == FireType.Launcher)
        {
            Vector3 forceDir = new Vector3();
            if (VR) forceDir = Quaternion.AngleAxis(-1.5f, transform.right) * transform.forward;
            else forceDir = transform.forward;
            _proj.GetComponent<Rigidbody>().AddForce(forceDir * 4000);
        } else if (fireType == FireType.PlasmaSniper || fireType == FireType.Sniper)
        {
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
        PlayerProjectile bullet = _proj.GetComponent<PlayerProjectile>();
        bullet.BulletDamage = damagePerShot;

    }

    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");
        if (!VR) animator.SetBool("Reloading", true);
        yield return new WaitForSeconds(reloadTime - 0.25f);
        if (!VR) animator.SetBool("Reloading", false);
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
        if (!VR) animator.SetBool("Reloading", false);
    }

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
