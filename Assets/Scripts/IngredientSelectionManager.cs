using System.Linq;
using UnityEngine;

public class IngredientSelectionManager : MonoBehaviour
{
    public IngredientData[] ingredientsSelected;

    public void AddIngredient(IngredientData ingredientAdded)
    {
        ingredientsSelected = ingredientsSelected.Concat(new IngredientData[] { ingredientAdded }).ToArray();
    }
    public void RemoveIngredient(IngredientData ingredientRemoved)
    {
        ingredientsSelected = ingredientsSelected.Where(ingredient => ingredient != ingredientRemoved).ToArray();
    }
    public bool IsInList (IngredientData ingredient)
    {
        return ingredientsSelected.Contains(ingredient);
    }
    public int PositionInList (IngredientData ingredient)
    {
        return System.Array.IndexOf(ingredientsSelected, ingredient);
    }
<<<<<<< Updated upstream
=======

    public void ResetIngredients()
    {
        ingredientsSelected = new IngredientData[0];
    }
    public void ShowKnife()
    {
        knife.gameObject.SetActive(true);
    }
>>>>>>> Stashed changes
    public void NextPressed()
    {
        Debug.Log("Next pressed");
    }
}
