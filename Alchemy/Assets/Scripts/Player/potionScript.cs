using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class potionScript : MonoBehaviour
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

        // Defaultowy wygl¹d potki
        selectedPotionIndex = 0;
        if (potionsCollection.potionArt.Count > 0)
        {
            Sprite selectedSprite = potionsCollection.potionArt[selectedPotionIndex];
            GetComponent<SpriteRenderer>().sprite = selectedSprite;
        }

        // Pobierz komponent Animator
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        //wybieranie potki na bazie wciœniêtych klawiszy 1-5
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectPotion(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectPotion(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectPotion(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SelectPotion(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SelectPotion(4);
        }
    }

    //funkcja do wybierania potki i przypisywania odpowiedniego indeksu
    public void SelectPotion(int index)
    {
        if (index >= 0 && index < potionsCollection.potionArt.Count)
        {
            selectedPotionIndex = index;
            Debug.Log("Wybrano potkê o indeksie: " + selectedPotionIndex);

            // Zaktualizuj sprite potki na podstawie wybranego indeksu
            if (potionsCollection.potionArt.Count > 0)
            {
                Sprite selectedSprite = potionsCollection.potionArt[selectedPotionIndex];
                GetComponent<SpriteRenderer>().sprite = selectedSprite;
            }
        }
        else
        {
            Debug.Log("B³êdny indeks potki!");
        }
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

