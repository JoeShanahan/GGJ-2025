using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GGJ2025.PouringGame
{
    public class PouringController : MonoBehaviour
    {
        [SerializeField] 
        private ShakerGraphics _shaker;

        [SerializeField] private CocktailData _currentCocktail;
        [SerializeField] private Text _goalText;
        [SerializeField] private Text _currentText;
        [SerializeField, Range(0, 1)] private float _percentTolerance = 0.1f;
        
        private Dictionary<IngredientData, float> _goalPercents;

        private void Start()
        {
            InitPouringGame(_currentCocktail);
            _goalText.text = GetGoalDebugString();
        }

        private void Update()
        {
            _currentText.text = _shaker.GetCurrentDebugString();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log($"Correct Ingredients? {DidUseCorrectIngredients()}");
                Debug.Log($"Correct Mix? {DidUseCorrectMixture()}");
            }
        }
        
        public void InitPouringGame(CocktailData cocktail)
        {
            float totalParts = 0;

            foreach (RecipeItem itm in cocktail.Recipe)
            {
                totalParts += itm.Parts;
            }

            _goalPercents = new Dictionary<IngredientData, float>();
            
            foreach (RecipeItem itm in cocktail.Recipe)
            {
                _goalPercents[itm.Ingredient] = itm.Parts / totalParts;
            }

            _shaker.Reset();
        }

        public DrinkMakeResult GetResult()
        {
            return null;
        }

        private bool DidUseCorrectIngredients()
        {
            HashSet<IngredientData> correct = new();
            HashSet<IngredientData> actual = new();

            foreach (var val in _goalPercents.Keys)
                correct.Add(val);

            foreach (var val in _shaker.CurrentAmounts.Keys)
                actual.Add(val);

            foreach (IngredientData ing in correct)
            {
                if (actual.Contains(ing) == false)
                    return false;
            }
            
            foreach (IngredientData ing in actual)
            {
                if (correct.Contains(ing) == false)
                    return false;
            }

            return true;
        }
        
        private bool DidUseCorrectMixture()
        {
            if (_shaker.TotalLiquid == 0)
                return false;
            
            foreach ((var ingData, var tgtPercent) in _goalPercents)
            {
                _shaker.CurrentAmounts.TryGetValue(ingData, out float actualAmount);
                actualAmount /= _shaker.TotalLiquid;

                float diff = Mathf.Abs(actualAmount - tgtPercent);

                if (diff > _percentTolerance)
                {
                    Debug.LogWarning($"{ingData.name} is {actualAmount} when it should be {tgtPercent} - FAIL");
                    return false;
                }
            }
            
            return true;
        }

        public string GetGoalDebugString()
        {
            string result = "Goal:\n";

            if (_currentCocktail == null)
                return "Goal unavailable";


            foreach ((IngredientData key, float val) in _goalPercents)
            {
                result += $"> {key.name}: " + (val * 100).ToString("n1") + "%\n";
            }

            result += $"({_currentCocktail.FullMinimum}% - {_currentCocktail.FullMaximum}% full)";
            
            return result;
        }
    }
}