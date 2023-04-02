using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="new ItemDatabase", menuName ="Inventory/Items/Database")]
public class ItemDatabase : ScriptableObject,ISerializationCallbackReceiver
{
    public Itemobj[] Items;
    public Dictionary<Itemobj,int>Getid=new Dictionary<Itemobj,int>();
    public Dictionary<int,Itemobj> GetItem = new Dictionary<int, Itemobj>();
    public void OnAfterDeserialize()
    {
        Getid = new Dictionary<Itemobj,int>();
        GetItem= new Dictionary<int, Itemobj>();
        for (int i = 0; i < Items.Length; i++)
        {
            Getid.Add(Items[i], i);
            GetItem.Add(i, Items[i]);   
        }
    }
    public void OnBeforeSerialize()
    {

    }
}
