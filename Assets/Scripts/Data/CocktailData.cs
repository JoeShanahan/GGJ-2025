using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CocktailData : ScriptableObject
{
    public string CocktailName;
    public List<RecipeItem> Recipe;
    
    [Range(0, 100)]
    public int PercentFull = 50;
    
    [Range(0, 100)]
    public int FullTolerance = 10;
}

[System.Serializable]
public class RecipeItem
{
    public IngredientData Ingredient;
    
    [Range(1, 20)]
    public int Parts;
}