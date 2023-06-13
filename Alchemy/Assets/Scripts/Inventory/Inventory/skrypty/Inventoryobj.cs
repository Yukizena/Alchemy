using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


[CreateAssetMenu(fileName ="New inventory",menuName ="Inventory system")]
public class Inventoryobj : ScriptableObject,ISerializationCallbackReceiver
{
    public string savePath;
    public ItemDatabase database;
    public List<Itemslot> Container = new List<Itemslot>();
    public void AddItem(Itemobj _item, int _amount)
    {
    
        for (int i = 0; i < Container.Count; i++)
        {
            if (Container[i].item == _item)
            {
                Container[i].AddAmount(_amount);
               
                return;
            }
        }         
         Container.Add(new Itemslot(database.Getid[_item],_item, _amount));
    }

    public void OnAfterDeserialize()
    {
       for(int i=0 ; i < Container.Count; i++)
        {
            Container[i].item = database.GetItem[Container[i].ID];
        }
    }

    public void OnBeforeSerialize()
    {
        
    }
    public void Save()
    {
        string saveData = JsonUtility.ToJson(this, true);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
        bf.Serialize(file, saveData);
        file.Close();
      }
    public void Load()
    {
        if (File.Exists(string.Concat(Application.persistentDataPath, savePath))){
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath),FileMode.Open);
            JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
            file.Close();
        }
        
    }
}
[System.Serializable]
public class Itemslot
{
    public int ID;
    public Itemobj item;
    public int amount;
    public Itemslot(int _id,Itemobj _item, int _amount)
    {
        ID = _id;
        item = _item;
        amount = _amount;
    }
    public void AddAmount(int value)
    {
        amount+=value;
    }
    
}