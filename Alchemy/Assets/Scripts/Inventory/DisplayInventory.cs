using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DisplayInventory : MonoBehaviour
{
    public Inventoryobj inventory;
    public int XBetweenItems;
    public int YBetweenItems;
    public int NumberOfColumns;
    public int xstart;
    public int ystart;
   
    Dictionary<Itemslot,GameObject>ItemsDisplayed=new Dictionary<Itemslot,GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        CreateDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDisplay();
    }
    public void CreateDisplay()
    {
        for(int i=0; i < inventory.Container.Count; i++)
        {
            var obj = Instantiate(inventory.Container[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition=GetPosition(i);
            obj.GetComponentInChildren < TextMeshProUGUI >().text= inventory.Container[i].amount.ToString("n0");
            ItemsDisplayed.Add(inventory.Container[i], obj);
        }
    }
    public Vector3 GetPosition(int i)
    {
        return new Vector3(xstart+XBetweenItems*(i%NumberOfColumns), ystart+YBetweenItems*(i/NumberOfColumns)*(-1),0f);
    }
    public void UpdateDisplay()
    {
        for(int i=0;i<inventory.Container.Count;i++)
        {
            if (ItemsDisplayed.ContainsKey(inventory.Container[i]))
            {
                ItemsDisplayed[inventory.Container[i]].GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
            }
            else
            {
                var obj = Instantiate(inventory.Container[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
                ItemsDisplayed.Add(inventory.Container[i] , obj);
            }
        }
    }
    

    
}
