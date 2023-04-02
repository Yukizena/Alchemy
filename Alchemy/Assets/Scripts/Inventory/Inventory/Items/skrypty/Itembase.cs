using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum itemType
{
    Potka,
    Ingredient,
    Eq,
    Default
}
public abstract class Itemobj : ScriptableObject
{
    public GameObject prefab;
    public itemType baza;
    [TextArea(15, 20)]

    public string description;

}
