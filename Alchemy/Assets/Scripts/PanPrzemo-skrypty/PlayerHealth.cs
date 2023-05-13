using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 10;
    public int currentHealth;
    public float invincibilityTime = 1f;
    private bool isInvincible = false;
    public healthBar healthBar;

    private void Start()
    {
        currentHealth = startingHealth;
        healthBar.setMaxHealth(startingHealth); 
    }

    public void TakeDamage(int damage)
    {
        if (!isInvincible)
        {
            currentHealth -= damage;
            isInvincible = true;
            StartCoroutine(Invincibility());
            healthBar.setHealth(currentHealth);
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
