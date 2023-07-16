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

    //obra¿enia od potki
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Potion"))
        {
            TakeDamage(1);

            // Zniszcz potkê
            Destroy(collision.gameObject);
        }
    }
    //dostawanie obra¿eñ
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

        // Uruchom animacjê niszczenia po pewnym czasie
        StartCoroutine(PlayDestroyAnimation());
    }

    //odpalanie animacji
    private IEnumerator PlayDestroyAnimation()
    {
        // Poczekaj jeden frame, aby upewniæ siê, ¿e obiekt zosta³ zniszczony
        yield return null;

        // Uruchom animacjê niszczenia
        animator.SetTrigger("Destroy");
    }
}