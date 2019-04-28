using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Aaron Mooney 
 * 
 * Weapon script used for multiplayer
 * 
 * Functions the same as the WeaponShoot script without the shooting mechanics
 * */
public class Weapon : MonoBehaviour {

    public enum FireType { Ray, VRNonScoped, Sniper, PlasmaSniper, Launcher };
    public FireType fireType;

    [SerializeField] private GameObject scopeOverlay;
    public GameObject weaponCamera;
    public Camera fpsCam;

    public float damagePerShot = 10f;
    public float fireRate = 0.5f;
    public float range = 100f;
    public int maxAmmo = 10;
    public int currentAmmo;
    public float reloadTime = 1f;
    public float scopedFov = 15f;
    private float normalFov;
    public float timer;
    public bool isReloading = false;
    public Animator animator;

    public ParticleSystem gunParticles;
    public GameObject impactEffect;
    public GameObject firePoint;

    public GameObject projectile;

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

    void Start()
    {
        currentAmmo = maxAmmo;
        OnScopedChanged += ScopedChangedHandler;
        

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
