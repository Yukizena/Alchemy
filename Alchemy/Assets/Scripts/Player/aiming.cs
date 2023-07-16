using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;
    private Vector3 rotation;
    public GameObject potion;
    public Transform potionTransform;
    public float maxAmmo;
    private float ammo;
    private float timer;
    public float timeBetweenFire;
    bool canFire = true;

    public Potion potionsCollection; // Referencja do obiektu scriptable object Potion
    private int selectedPotionIndex = 0; // Indeks wybranej potki, domyœlnie 0

    // Start is called before the first frame update
    void Start()
    {
        // inicjalizacja kamery 
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        // ustalanie pozycji myszki na ekranie
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        // ustawianie wektora rotacji
        rotation = mousePos - transform.position;

        //okreœlenie k¹ta rotacji wokó³ postaci (oœ z)
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        //zwrócenie rotacji wokó³ osi z
        transform.rotation = Quaternion.Euler(0, 0, rotZ);

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

        //odnawianie siê amunicji w czasie
        if (ammo<maxAmmo)
        {
            //timer synchronizowany z czasem w grze
            timer += Time.deltaTime;
            //Dodanie amunicji
            if(timer>timeBetweenFire)
            {
                ammo++;
                timer=0;
            }
        }
        //jeœli klikamy LPM i mamy amunicje
        if (Input.GetMouseButton(0) && ammo > 0 && canFire)
        {
            //zmniejszamy amunicje
            ammo--;

            //zmiana pozycji potki
            GameObject potionInstance = Instantiate(potion, potionTransform.position, Quaternion.identity);

            // Pobranie grafiki potki z obiektu scriptable object Potion
            Sprite selectedSprite = potionsCollection.potionArt[selectedPotionIndex];

            // Ustawienie odpowiedniej grafiki potki na obiekcie potki
            potionInstance.GetComponent<SpriteRenderer>().sprite = selectedSprite;


            //usuwanie obiektu po 5 sekundach
            Debug.Log("Niszczê Potkê");
            Object.Destroy(potionInstance, 5.0f);

            // ustawienie flagi informuj¹cej o tym, ¿e kolejny strza³ nie mo¿e byæ wykonany
            canFire = false;

            // uruchomienie funkcji, która po czasie timeBetweenFire ustawia flagê canFire na true
            StartCoroutine(EnableFire(timeBetweenFire));
        }
    }

    // funkcja ustawiaj¹ca flagê canFire na true po okreœlonym czasie
    private IEnumerator EnableFire(float time)
    {
        yield return new WaitForSeconds(time);
        canFire = true;
    }

    // Funkcja do wybierania potki na podstawie indeksu
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
}

