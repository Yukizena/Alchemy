using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New potion obj", menuName = "Inventory/Items/Potka")]
public class Potkaobj : Itemobj
{
    public int restore;
    public int damage;
    public int poison;
    public void Awake()
    {
        baza = itemType.Potka;
    }
}
