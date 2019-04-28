using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Aaron Mooney
 * 
 * EnemyHealth script that tracks an enemy's health
 * */
public class EnemyHealth : MonoBehaviour
{
    [Header("Enemy stats")]
    public float maxHealth = 100f;
    public float currentHealth;
    public bool isDead;
    public int coinWorth;

    private bool VR;
    private Collider collider;
    private Animator anim;

    void Awake()
    {
        // Get collider
        if (GetComponent<BoxCollider>() != null) collider = GetComponent<BoxCollider>();
        if (GetComponentInChildren<MeshCollider>() != null) collider = GetComponentInChildren<MeshCollider>();
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        anim.SetFloat("health", maxHealth);
    }

    private void Start()
    {
        VR = VRConfig.VREnabled;
    }

    // Take damage and set animator health value, die if health goes below 0
    public void TakeDamage(float amount)
    {
        print("hit");
        anim.SetFloat("health", anim.GetFloat("health") - amount);
        if (isDead)
            return;

        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Die method
    private void Die()
    {
        // Set animation
        anim.SetBool("walking", false);
        anim.SetBool("attacking", false);
        isDead = true;

        // Disable collider
        collider.enabled = false;

        // Destroy enemy
        if (gameObject.tag == "Aerial")
        {
            Destroy(gameObject, 2f);
        }
        else
        {
            Destroy(gameObject, 4f);
        }

        // Add coins to player
        if (!VR)
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerActions>().AddCoins(coinWorth);
        else
        {
            GameObject.Find("VRShopScripts").GetComponent<VRShop>().AddCoins(coinWorth);
        }
    }
}