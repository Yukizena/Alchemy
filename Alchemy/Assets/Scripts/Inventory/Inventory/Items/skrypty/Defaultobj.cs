using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New default obj", menuName = "Inventory/Items/Default")]
public class Defaultobj : Itemobj
{
    public void Awake()
    {
        baza=itemType.Default;
    }
}
