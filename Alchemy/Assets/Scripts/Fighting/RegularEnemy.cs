using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RegularEnemy : MonoBehaviour
{
    public static event Action<RegularEnemy> OnEnemyKilled;
    [SerializeField] float health, maxHealth = 3f;

    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float detectionRange = 10f; // Odleg³oœæ, w której wróg zaczyna œledziæ gracza
    Rigidbody2D rb;
    Transform target;
    Vector2 moveDirection;
    Vector3 savedScale = Vector3.one;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        savedScale = transform.localScale;
    }

    private void Start()
    {
        health = maxHealth;
        target = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        if (target)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, target.position);

            if (distanceToPlayer <= detectionRange)
            {
                Vector3 direction = (target.position - transform.position).normalized;
                float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;

                moveDirection = direction;
            }
            else
            {
                moveDirection = Vector2.zero;
            }
        }
    }

    private void FixedUpdate()
    {
        if (target)
        {
            rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * moveSpeed;
        }
        if (moveDirection.x > 0)
            transform.localScale = savedScale;
        else if (moveDirection.x < 0)
            transform.localScale = new Vector3(-1 * savedScale.x, savedScale.y, savedScale.z);
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;

        if (health <= 0)
        {
            Destroy(gameObject);
            OnEnemyKilled?.Invoke(this);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.TryGetComponent<PlayerHealth>(out PlayerHealth playerComponent))
            {
                playerComponent.TakeDamage(1);
                Debug.Log("Gracz oberwa³. Aktualne zdrowie: " + playerComponent.currentHealth);
            }
        }
    }
}
