using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PotionScript : MonoBehaviour
{
    public Potion potionsCollection;
    private int selectedPotionIndex;

    private Vector3 mousePos;
    private Camera mainCam;
    private Rigidbody2D rb;
    public float force;
    public GameObject Enemy;

    //dodanie animatora
    private Animator animator;
    private void Start()
    {
       //pobranie kamery
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        // pobranie rigidbody potki
        rb = GetComponent<Rigidbody2D>();
        //strza³ w kierunku wychylenia myszki
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
        // Pobierz komponent Animator
        animator = GetComponent<Animator>();
    }

    private void Update()
    {

    }

  

    //zderzenie siê potki z wrogami lub map¹
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (collision.gameObject.TryGetComponent<RegularEnemy>(out RegularEnemy enemyComponent))
            {
                enemyComponent.TakeDamage(1);
                // Zniszcz obiekt
                Destroy(gameObject);

                // Wywo³aj animacjê wybuchu dla konkretnej potki po pewnym czasie
                StartCoroutine(ExplodeAnimation(selectedPotionIndex));

            }
        }
        if (collision.gameObject.CompareTag("Boss"))
        {
            if (collision.gameObject.TryGetComponent<SmallEnemy>(out SmallEnemy bossComponent))
            {
                bossComponent.TakeDamage(1);
                //Debug.Log("Boss oberwa³");
                // Zniszcz obiekt
                Destroy(gameObject);

                // Wywo³aj animacjê wybuchu dla konkretnej potki po pewnym czasie
                StartCoroutine(ExplodeAnimation(selectedPotionIndex));
            }
        }
        if (collision.gameObject.GetComponent<TilemapCollider2D>() != null)
        {
            // Zniszcz obiekt
            Destroy(gameObject);

            // Wywo³aj animacjê wybuchu dla konkretnej potki po pewnym czasie
            StartCoroutine(ExplodeAnimation(selectedPotionIndex));
        }
        //przenikanie fireballa
        if (collision.gameObject.CompareTag("Fireball"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }

    //aktywowanie eksplozji potki po uderzeniu
    private IEnumerator ExplodeAnimation(int animationIndex)
    {
        // Poczekaj chwilê przed odtworzeniem animacji
        yield return new WaitForSeconds(0.1f);

        // Odtwórz animacjê wybuchu
        animator.SetInteger("AnimationIndex", animationIndex);
        animator.SetTrigger("Explode");

        // Poczekaj na zakoñczenie animacji
        yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0).Length);

        // Zniszcz obiekt, jeœli jeszcze istnieje (np. animacja zosta³a przerwana)
        if (gameObject != null)
        {
            Destroy(gameObject);
        }
    }
}

