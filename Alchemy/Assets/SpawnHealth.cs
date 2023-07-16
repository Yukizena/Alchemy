using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHealth : MonoBehaviour
{
    public float maxHealth = 3f;
    private float currentHealth;

    public Animator animator;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    //obra�enia od potki
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Potion"))
        {
            TakeDamage(1);

            // Zniszcz potk�
            Destroy(collision.gameObject);
        }
    }
    //dostawanie obra�e�
    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            DestroySpawner();
        }
    }

    //niszczenie spawnera
    private void DestroySpawner()
    {
        // Zniszcz spawner
        Destroy(gameObject);

        // Uruchom animacj� niszczenia po pewnym czasie
        StartCoroutine(PlayDestroyAnimation());
    }

    //odpalanie animacji
    private IEnumerator PlayDestroyAnimation()
    {
        // Poczekaj jeden frame, aby upewni� si�, �e obiekt zosta� zniszczony
        yield return null;

        // Uruchom animacj� niszczenia
        animator.SetTrigger("Destroy");
    }
}