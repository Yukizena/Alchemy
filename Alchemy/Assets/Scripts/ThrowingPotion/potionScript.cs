using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class potionScript : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera mainCam;
    private Rigidbody2D rb;
    public float force;
    public GameObject Enemy;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Rzucam Potkê");
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized*force;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Wejœcie w kolizje");
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Wejœcie w kolizje z wrogiem");
            if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemyComponent))
            {
                enemyComponent.TakeDamage(1);
                Debug.Log("Wróg oberwa³");
                Destroy(collision.gameObject);
                Debug.Log("Wróg zniszczony");
                Destroy(gameObject);
            }
        }
    }
    /*private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Enter collision with: " + other.name);
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Jest kolizja");
            Destroy(other.gameObject);
        }
        else
        {
            Debug.Log("Brak kolizji");
        }
        // niszczenie potki
        Destroy(gameObject);
    }*/
    // Update is called once per frame
    void Update()
    {
        
    }
}
