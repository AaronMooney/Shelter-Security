using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public float maxHealth = 100f;
    public float currentHealth;
    public int coinWorth;
    private bool VR;
    Collider collider;
    public bool isDead;
    Animator anim;

    void Awake()
    {
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

    void Die()
    {
        anim.SetBool("walking", false);
        anim.SetBool("attacking", false);
        isDead = true;

        collider.enabled = false;

        if (gameObject.tag == "Aerial")
        {
            Destroy(gameObject, 2f);
        }
        else
        {
            Destroy(gameObject, 4f);
        }
        if (!VR)
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerActions>().AddCoins(coinWorth);
    }
}