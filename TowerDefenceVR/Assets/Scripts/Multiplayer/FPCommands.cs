using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
/*
 * Aaron Mooney
 * 
 * FPCommands Script that handles player commands in multiplayer
 * */
public class FPCommands : NetworkBehaviour {

    public Weapon weapon;
    
    Ray shootRay = new Ray();
    RaycastHit shootHit;
    KeyCode fireKey = KeyCode.Mouse0;
    public string enemyTag = "Enemy";
    private AnimatorStateInfo info;
    public AudioSource gunAudio;

    // Command method that calls the replacate fire methods
    [Command]
    public void CmdShoot()
    {
        if (weapon.fireType == Weapon.FireType.Ray)
        {
            RpcShootEffectsRay();
        }
        else
        {
            RpcShootEffectsProjectile();
        }
    }

    // Command method that calls the replicate impact method
    [Command]
    public void CmdImpact()
    {
        RpcImpact();
    }

    void Update()
    {
        if (!isLocalPlayer) return;
        gunAudio = weapon.firePoint.GetComponent<AudioSource>();
        weapon = GetComponent<WeaponSelectionMP>().currentWeapon.GetComponent<Weapon>();
        info = weapon.animator.GetCurrentAnimatorStateInfo(0);

        weapon.timer += Time.deltaTime;

        if (weapon.isReloading) return;
            

        if (weapon.currentAmmo <= 0)
        {

            StartCoroutine(Reload());
            return;
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            weapon.IsScoped = !weapon.IsScoped;
        }

        if (!weapon.isReloading && weapon.currentAmmo < weapon.maxAmmo && Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
            return;
        }

        if ((Input.GetKey(fireKey)))
        {

            Fire();
        }

    }

    private void FixedUpdate()
    {
        AnimatorStateInfo info = weapon.animator.GetCurrentAnimatorStateInfo(0);
        if (info.IsName("WeaponShoot")) StopShooting();
        
    }

    // Client method that calls the command to stop shooting
    [Client]
    private void StopShooting()
    {
        CmdStopShoot();
    }

    // Command method that calls the replicate method to stop shooting
    [Command]
    private void CmdStopShoot()
    {
        RpcStopShoot();
    }

    // repliacte method that replicated the stop shooting animation to all clients
    [ClientRpc]
    private void RpcStopShoot()
    {
        weapon.animator.SetBool("Shoot", false);
    }

    // the client fire method that calls the shoot method
    [Client]
    private void Fire()
    {
        if (weapon.timer >= weapon.fireRate && Time.timeScale != 0)
        {
            if (weapon.fireType == Weapon.FireType.Ray)
            {
                ShootRay();
            }
            else
            {
                ShootProjectile();
            }
        }
    }

    // The replicate method that replicates weapon firing across all clients
    [ClientRpc]
    public void RpcShootEffectsRay()
    {
        gunAudio.Play();
        weapon.animator.SetBool("Shoot", true);
        weapon.gunParticles.Stop();
        weapon.gunParticles.Play();
    }

    // The replicate method that replicates stop shooting
    [ClientRpc]
    public void RpcDisableShooting()
    {
        weapon.animator.SetBool("Shooting", false);
    }

    // The replicate method that replicates impacts across clients
    [ClientRpc]
    public void RpcImpact()
    {
        GameObject instance = (GameObject)Instantiate(weapon.impactEffect, shootHit.point, Quaternion.identity);
        Destroy(instance, 2f);
    }

    // The client shoot method that calls the command shoot and command impact
    [Client]
    private void ShootRay()
    {
        weapon.timer = 0f;


        weapon.currentAmmo--;

        CmdShoot();

        if (VRConfig.VREnabled)
        {
            shootRay.origin = weapon.firePoint.transform.position;
            shootRay.direction = weapon.firePoint.transform.forward;
        }
        else
        {
            shootRay.origin = weapon.fpsCam.transform.position;
            shootRay.direction = weapon.fpsCam.transform.forward;
        }

        if (Physics.Raycast(shootRay, out shootHit, weapon.range))
        {
            Debug.DrawRay(shootRay.origin, shootRay.direction);
            if (shootHit.collider.tag == enemyTag)
            {
                shootHit.collider.GetComponent<EnemyHealth>().TakeDamage(weapon.damagePerShot);
            }

            CmdImpact();
        }
    }

    // The replicate method that replicates shoot effects for projectile weapons
    [ClientRpc]
    public void RpcShootEffectsProjectile()
    {
        if (!info.IsName("SniperScoped") && !info.IsName("PlasmaScoped") && !info.IsName("LauncherScoped"))
        {
            weapon.animator.SetBool("Shoot", true);
        }
        if (weapon.fireType == Weapon.FireType.Launcher)
        {
            gunAudio.time = 0f;
            gunAudio.Play();
            gunAudio.SetScheduledEndTime(AudioSettings.dspTime + (0.55f - 0f));
        }
        else
        {
            gunAudio.Play();
        }

        weapon.gunParticles.Stop();
        weapon.gunParticles.Play();

    }

    // The client shoot method that calls the command shoot and command impact
    [Client]
    void ShootProjectile()
    {

        Debug.Log("firew");
        weapon.timer = 0f;

        
        weapon.currentAmmo--;

        CmdShoot();

        GameObject _proj = (GameObject)Instantiate(weapon.projectile, weapon.firePoint.transform.position, weapon.firePoint.transform.rotation);
        if (weapon.fireType == Weapon.FireType.Launcher)
        {
            Vector3 forceDir = weapon.firePoint.transform.forward;
            _proj.GetComponent<Rigidbody>().AddForce(forceDir * 4000);
        }
        else if (weapon.fireType == Weapon.FireType.PlasmaSniper || weapon.fireType == Weapon.FireType.Sniper)
        {
            Vector3 forceDir = new Vector3();
            if (weapon.IsScoped)
            {
                forceDir = Quaternion.AngleAxis(-1, weapon.firePoint.transform.right) * weapon.firePoint.transform.forward;
            }
            else
                forceDir = weapon.firePoint.transform.forward;
            _proj.GetComponent<Rigidbody>().AddForce(forceDir * 6000);
        }
        else
        {
            _proj.GetComponent<Rigidbody>().AddForce(weapon.firePoint.transform.forward * 4000);
        }
        PlayerProjectile bullet = _proj.GetComponent<PlayerProjectile>();
        bullet.BulletDamage = weapon.damagePerShot;

    }

    [Client]
    IEnumerator Reload()
    {
        weapon.isReloading = true;
        Debug.Log("Reloading...");
        weapon.animator.SetBool("Reloading", true);
        yield return new WaitForSeconds(weapon.reloadTime - 0.25f);
        weapon.animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(0.25f);
        weapon.currentAmmo = weapon.maxAmmo;
        weapon.isReloading = false;
    }

    
}
