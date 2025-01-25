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
        
        [SerializeField] private float _totalR;
        [SerializeField] private float _totalG;
        [SerializeField] private float _totalB;
        [SerializeField] private float _totalLiquid;

        private void Start()
        {
            _liquidScaler.localScale = new Vector3(1, 0, 1);
        }
        
        public void AddLiquid(IngredientData ingredient, float amount)
        {
            _totalLiquid += amount;

            if (_totalLiquid == 0)
                return;

            if (_totalLiquid >= _maximumFill)
                return;
            
            _totalR += ingredient.Tint.r * amount;
            _totalG += ingredient.Tint.g * amount;
            _totalB += ingredient.Tint.b * amount;

            _liquidSprite.color = new Color(_totalR / _totalLiquid, _totalG / _totalLiquid, _totalB / _totalLiquid);

            float yScale = Mathf.Clamp01(_totalLiquid / _maximumFill);
            
            _liquidScaler.localScale = new Vector3(1, yScale, 1);

            _ui.AddToBar(amount / _maximumFill, ingredient);

        }
    }
}