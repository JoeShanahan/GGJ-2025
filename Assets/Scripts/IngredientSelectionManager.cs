using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class IngredientSelectionManager : MonoBehaviour
{
    public IngredientData[] ingredientsSelected;

    public Transform knife;

    void Start()
    {
        knife.gameObject.SetActive(false);
    }

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

    public void ShowKnife()
    {
        knife.gameObject.SetActive(true);
    }
    public void NextPressed()
    {
        Debug.Log("Next pressed");
    }
}