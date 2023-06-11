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
        GetComponent<Animator>().SetTrigger("Death");
    }

    // obs³uga obra¿eñ od dziury
    private Vector3 lastGoodPosition;
    public bool backToGoodPosition = true;

    /*private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hole"))
        {
            // Czy mamy mo¿liwoœæ wejœcia w dziurê czy nie
            if (backToGoodPosition)
            {
                TakeDamage(1);

                // Przywrócenie gracza do pozycji obok dziury
                Vector3 newPosition = transform.position;
                Vector3 holePosition = other.transform.position;

                //przesuwanie gracza w kierunku z którego przyszed³
                if (holePosition.x < newPosition.x)
                {
                    newPosition.x += 0.3f; 
                }
                else if (holePosition.x > newPosition.x)
                {
                    newPosition.x -= 0.3f; 
                }

                if (holePosition.y < newPosition.y)
                {
                    newPosition.y += 0.3f; 
                }
                else if (holePosition.y > newPosition.y)
                {
                    newPosition.y -= 0.3f; 
                }

                transform.position = newPosition;
            }
            else
            {
                TakeDamage(1);
            }
        }
    }*/
}
