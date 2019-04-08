using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public float maxHealth = 100f;
    public float currentHealth;
    BoxCollider boxCollider;
    public bool isDead;
    Animator anim;

    void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        anim.SetFloat("health", maxHealth);
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

        boxCollider.isTrigger = true;

        Destroy(gameObject, 5f);
    }
}