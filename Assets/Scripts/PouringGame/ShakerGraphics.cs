using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ2025.PouringGame
{
    public class ShakerGraphics : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _liquidSprite;

        [SerializeField] 
        private DrinkMeter _ui;
        
        [SerializeField] 
        private float _maximumFill = 50;

        [SerializeField] 
        private Transform _liquidScaler;
        
        private float _totalR;
        private float _totalG;
        private float _totalB;
        private float _totalLiquid;
        public Dictionary<IngredientData, float> CurrentAmounts;

        public float TotalLiquid => _totalLiquid;
        public float PercentFull => _totalLiquid / _maximumFill;
        
        public void Reset()
        {
            _ui.Reset();
            _totalR = _totalB = _totalG = _totalLiquid = 0;
            _liquidScaler.localScale = new Vector3(1, 0, 1);
            CurrentAmounts = new Dictionary<IngredientData, float>();
        }
        
        private void Start()
        {
            _liquidScaler.localScale = new Vector3(1, 0, 1);
        }
        
        public void AddLiquid(IngredientData ingredient, float amount)
        {
            if (amount == 0)
                return;

            if (_totalLiquid >= _maximumFill)
                return;

            if (CurrentAmounts.ContainsKey(ingredient) == false)
                CurrentAmounts[ingredient] = 0;

            _totalLiquid += amount;
            CurrentAmounts[ingredient] += amount;
            
            _totalR += ingredient.Tint.r * amount;
            _totalG += ingredient.Tint.g * amount;
            _totalB += ingredient.Tint.b * amount;

            _liquidSprite.color = new Color(_totalR / _totalLiquid, _totalG / _totalLiquid, _totalB / _totalLiquid);

            float yScale = Mathf.Clamp01(_totalLiquid / _maximumFill);
            
            _liquidScaler.localScale = new Vector3(1, yScale, 1);

            _ui.AddToBar(amount / _maximumFill, ingredient);
        }
                
        public string GetCurrentDebugString()
        {
            string result = "Current:\n";

            if (CurrentAmounts == null || _totalLiquid == 0)
                return "No liquid yet";

            foreach ((IngredientData key, float val) in CurrentAmounts)
            {
                result += $"> {key.name}: " + (val/_totalLiquid * 100).ToString("n1") + "%\n";
            }

            int percentFull = Mathf.RoundToInt((_totalLiquid / _maximumFill) * 100);
            result += $"({percentFull}% full)";
            
            return result;
        }
    }
}