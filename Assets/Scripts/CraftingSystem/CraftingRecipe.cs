using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ingredient
{
    public ItemData item;
    public int quantity;
}

[CreateAssetMenu(fileName = "New Recipe", menuName = "Crafting/Crafting Recipe")]
public class CraftingRecipe : ScriptableObject
{
    public List<Ingredient> ingredients; // Ingredients
    public ItemData result;              // result
    public int resultQuantity = 1;       // quantity
}
