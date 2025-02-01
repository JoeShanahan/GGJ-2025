using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IngredientSelectionManager : MonoBehaviour {
    public IngredientData[] ingredientsSelected;
    
    [SerializeField] private GameObject knife;

    [SerializeField] private Transform[] spawnPoints;

    private readonly List<GameObject> _spawnedDrinks = new List<GameObject>();

    public void AddIngredient(IngredientData ingredientAdded) {
        ingredientsSelected = ingredientsSelected.Concat(new IngredientData[] { ingredientAdded }).ToArray();
    }

    public void RemoveIngredient(IngredientData ingredientRemoved) {
        ingredientsSelected = ingredientsSelected.Where(ingredient => ingredient != ingredientRemoved).ToArray();
    }

    public bool IsInList(IngredientData ingredient) {
        return ingredientsSelected.Contains(ingredient);
    }

    public int PositionInList(IngredientData ingredient) {
        return System.Array.IndexOf(ingredientsSelected, ingredient);
    }

    public void ShowKnife(bool show) {
        if (knife == null) {
            return;
        }
        knife.SetActive(show);
    }

    public void NextPressed() {
        Debug.Log("Next pressed");
    }

    public void SpawnDrinkPrefabs() {
        foreach (var drink in _spawnedDrinks) {
            if (drink != null)
                Destroy(drink);
        }
        _spawnedDrinks.Clear();

        int spawnCount = Mathf.Min(ingredientsSelected.Length, spawnPoints.Length);

        for (int i = 0;i < spawnCount;i++) {
            IngredientData ingredient = ingredientsSelected[i];
            if (ingredient.BottlePrefab != null) {
                GameObject spawnedDrink = Instantiate(ingredient.BottlePrefab, spawnPoints[i].position, Quaternion.identity);
                _spawnedDrinks.Add(spawnedDrink);
            }
        }
    }
}