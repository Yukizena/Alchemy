using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class potionScript : MonoBehaviour
{
    public Potion potion;
    private Vector3 mousePos;
    private Camera mainCam;
    private Rigidbody2D rb;
    public float force;
    public GameObject Enemy;

    private int currentSpriteIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Rzucam Potk�");
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        //Poruszanie si� potki w okre�lonym celu
        rb = GetComponent<Rigidbody2D>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized*force;

        // losowa potka
        if (potion != null && potion.potionArt.Count > 0)
        {
            currentSpriteIndex = Random.Range(0, potion.potionArt.Count);

            Sprite randomSprite = potion.potionArt[currentSpriteIndex];
            GetComponent<SpriteRenderer>().sprite = randomSprite;
            Debug.Log("Numer wybranej grafiki: " + currentSpriteIndex);
            // Zaktualizuj indeks dla kolejnego rzutu potk�
            currentSpriteIndex = (currentSpriteIndex + 1) % potion.potionArt.Count;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Wej�cie w kolizje");
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //Debug.Log("Wej�cie w kolizje z wrogiem");
            if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemyComponent))
            {
                enemyComponent.TakeDamage(1);
                //Debug.Log("Wr�g oberwa�");
                Destroy(collision.gameObject);
                //Debug.Log("Wr�g zniszczony");
                Destroy(gameObject);
            }
        }
    }
    void Update()
    {

    }
}
