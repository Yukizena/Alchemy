using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Inventoryobj iteminventory;
    public Inventoryobj potioninventory;
    public void OnTriggerEnter2D(Collider2D other)
    {
        var item=other.GetComponent<Item>();
        if (item)
        {
            if (item.item.baza == itemType.Potka)
                potioninventory.AddItem(item.item, 1);
            else
                iteminventory.AddItem(item.item, 1);
            Destroy(other.gameObject);
        }
    }
    //istnieje tylko w trakcie testowania, nie trzeba przekopiowywac
    private void OnApplicationQuit()
    {
        iteminventory.Container.Clear();
        potioninventory.Container.Clear();
    }
    //ruch tutaj do deleta, spacja i enter s¹ tylko do testowania funkcji save, mozna usunac funkcja istnieje na póŸniej
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            iteminventory.Save();
            potioninventory.Save();
            transform.position += new Vector3(0, 30, 0);
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            iteminventory.Load();
            potioninventory.Load();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(30, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position -= new Vector3(30, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.position += new Vector3(0, 30, 0);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.position -= new Vector3(0, 30, 0);
        }
    }
}
