using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class potionChoosing : MonoBehaviour
{
    public Potion potionsCollection;
    public int selectedPotionIndex;

    private void Start()
    {
        // Domyœlnie wybierz pierwsz¹ potkê
        selectedPotionIndex = 0;
    }

    private void Update()
    {
        // SprawdŸ naciœniêcie klawiszy 1-5
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
    }

    public void SelectPotion(int index)
    {
        if (index >= 0 && index < potionsCollection.potionArt.Count)
        {
            selectedPotionIndex = index;
            Debug.Log("Wybrano potkê o indeksie: " + (selectedPotionIndex+1));
        }
        else
        {
            Debug.Log("B³êdny indeks potki!");
        }
    }

    public void UseSelectedPotion()
    {
        if (selectedPotionIndex >= 0 && selectedPotionIndex < potionsCollection.potionArt.Count)
        {
            Sprite selectedPotionSprite = potionsCollection.potionArt[selectedPotionIndex];
            Debug.Log("U¿yto potki o indeksie: " + selectedPotionIndex + ", sprite: " + selectedPotionSprite.name);
            // Tutaj mo¿esz dodaæ logikê do wykonania odpowiednich dzia³añ zwi¹zanych z u¿yciem potki
        }
        else
        {
            Debug.Log("B³êdny indeks potki!");
        }
    }
}
