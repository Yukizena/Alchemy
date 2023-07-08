using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aiming : MonoBehaviour
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

        //okre�lenie k�ta rotacji wok� postaci (o� z)
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        //zwr�cenie rotacji wok� osi z
        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        //odnawianie si� amunicji w czasie
        if(ammo<maxAmmo)
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
        //je�li klikamy LPM i mamy amunicje
        if (Input.GetMouseButton(0) && ammo > 0 && canFire)
        {
            //zmniejszamy amunicje
            ammo--;

            //zmiana pozycji potki
            GameObject potionInstance = Instantiate(potion, potionTransform.position, Quaternion.identity);

            //usuwanie obiektu po 5 sekundach
            Debug.Log("Niszcz� Potk�");
            Object.Destroy(potionInstance, 5.0f);

            // ustawienie flagi informuj�cej o tym, �e kolejny strza� nie mo�e by� wykonany
            canFire = false;

            // uruchomienie funkcji, kt�ra po czasie timeBetweenFire ustawia flag� canFire na true
            StartCoroutine(EnableFire(timeBetweenFire));
        }
    }

    // funkcja ustawiaj�ca flag� canFire na true po okre�lonym czasie
    private IEnumerator EnableFire(float time)
    {
        yield return new WaitForSeconds(time);
        canFire = true;
    }
}

