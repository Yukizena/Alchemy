using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public float invincibilityTime = 1f;
    private bool isInvincible = false;

    private void Start()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(int damage)
    {
        if (!isInvincible)
        {
            currentHealth -= damage;
            isInvincible = true;
            StartCoroutine(Invincibility());
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    IEnumerator Invincibility()
    {
        // Make the player invincible for a short period of time
        gameObject.layer = LayerMask.NameToLayer("IgnoreEnemies");
        yield return new WaitForSeconds(invincibilityTime);
        gameObject.layer = LayerMask.NameToLayer("Player");
        isInvincible = false;
    }

    void Die()
    {
        // Handle player death here (e.g. show game over screen)
        Debug.Log("Player died!");
    }
}
