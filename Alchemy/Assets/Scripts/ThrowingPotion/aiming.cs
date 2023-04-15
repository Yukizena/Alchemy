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

        //odnawianie siê amunicji w czasie
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
        //jeœli klikamy LPM i mamy amunicje
        if(Input.GetMouseButton(0) && ammo>0)
        {
            //zmniejszamy amunicje
            ammo--;
            //zmiana pozycji potki
            Instantiate(potion, potionTransform.position, Quaternion.identity);
            //usuwanie obiektu po 5 sekundach
            Object.Destroy(potion, 5.0f);
        }


    }
}

