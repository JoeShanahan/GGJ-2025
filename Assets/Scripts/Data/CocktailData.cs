using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CocktailData : ScriptableObject
{
    public string CocktailName;
    public List<RecipeItem> Recipe;
    
    [TextArea(3, 3)]
    public string Description;

    public Sprite Icon;
    public Sprite FailedIcon;
    
    [Range(0, 100)]
    public int FullMinimum = 10;

    [Range(0, 100)]
    public int FullMaximum = 10;
}

[System.Serializable]
public class RecipeItem
{
    public IngredientData Ingredient;
    
    [Range(1, 10)]
    public float Parts;
}