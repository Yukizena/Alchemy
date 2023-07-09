using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SmallEnemy : MonoBehaviour
{
    public static event Action<SmallEnemy> OnEnemyKilled;
    [SerializeField] float health, maxHealth = 3f;
    [SerializeField] float detectionRange = 10f; // Odleg³oœæ, w której wróg zaczyna œledziæ gracza
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D rb;
    Transform target;
    Vector2 moveDirection;
    Vector3 savedScale = Vector3.one;

    public GameObject fireball;
    public float shootInterval = 2f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        savedScale = transform.localScale;
    }


    // Start is called before the first frame update
    private void Start()
    {
        //Debug.Log("Jest Boss");
        health = maxHealth;
        target = GameObject.Find("Player").transform;

        // Rozpocznij korutynê strzelania pociskami
        StartCoroutine(ShootProjectileRoutine());
    }

    private void Update()
    {
        //szukanie gracza tylko w pewnej odleg³oœci
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
        //Debug.Log($"Damage Amount:{damageAmount}");
        health -= damageAmount;
       // Debug.Log($"Health is now: {health}");

        if (health <= 0)
        {
            Destroy(gameObject);
            OnEnemyKilled?.Invoke(this);
        }
    }

    private IEnumerator ShootProjectileRoutine()
    {
        while (true)
        {
            // Strzelanie pociskiem
            ShootProjectile();

            // Odstêp czasowy
            yield return new WaitForSeconds(shootInterval);
        }
    }

    private void ShootProjectile()
    {
        // Tworzenie pocisku
        GameObject projectile = Instantiate(fireball, transform.position, Quaternion.identity);

        // Dodawanie komponentu do pocisku, który bêdzie œledzi³ gracza
        ProjectileTracking projectileTracking = projectile.AddComponent<ProjectileTracking>();
        projectileTracking.target = target;

        // Zniszczenie pocisku po pewnym czasie
        Destroy(projectile, shootInterval);
    }

    // atakowanie gracza
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Atak na gracza");
            if (collision.gameObject.TryGetComponent<PlayerHealth>(out PlayerHealth playerComponent))
            {
                playerComponent.TakeDamage(1);
                Debug.Log("Gracz oberwa³. Aktualne zdrowie: " + playerComponent.currentHealth);
            }
        }
    }
}
