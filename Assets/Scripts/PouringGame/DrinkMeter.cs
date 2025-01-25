using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GGJ2025.PouringGame
{
    public class DrinkMeter : MonoBehaviour
    {
        [SerializeField] 
        private RectTransform _parentObject;
        
        private LayoutElement _latestElement;
        private IngredientData _latestIngredient;

        public void AddToBar(float percentOfBar, IngredientData data)
        {
            if (percentOfBar == 0)
                return;
            
            if (_latestIngredient != data)
            {
                _latestIngredient = data;
                CreateNewBar();
            }

            _latestElement.preferredHeight += percentOfBar * _parentObject.rect.height;
        }

        private void CreateNewBar()
        {
            // This is so lazy but oh well
            GameObject newObj = new GameObject("Bar!");
            newObj.transform.parent = _parentObject;
            newObj.transform.localScale = Vector3.one;
            newObj.transform.rotation = Quaternion.identity;
            newObj.AddComponent<RectTransform>();
            newObj.AddComponent<Image>().color = _latestIngredient.Tint;
            _latestElement = newObj.AddComponent<LayoutElement>();
            _latestElement.flexibleWidth = 1;
            _latestElement.preferredHeight = 0;
        }

        private void Reset()
        {
            foreach (Transform t in _parentObject)
            {
                Destroy(t.gameObject);
            }

            _latestElement = null;
            _latestIngredient = null;
        }
    }
}