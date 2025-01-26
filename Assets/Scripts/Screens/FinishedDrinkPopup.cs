using System.Collections.Generic;
using GGJ2025.PouringGame;
using UnityEngine;
using UnityEngine.UI;

namespace GGJ2025.Screens
{
    public class FinishedDrinkPopup : MonoBehaviour
    {
        [SerializeField] private Text _nameText;
        [SerializeField] private Text _description;
        [SerializeField] private Image _drinkImage;
        [SerializeField] private DrinkRatingItem[] _ratings;
        
        public void Show(DrinkMakeResult result)
        {
            gameObject.SetActive(true);
            
            if (result.WasSuccess)
            {
                _nameText.text = result.Cocktail.CocktailName;
                _description.text = result.Cocktail.Description;
                _drinkImage.sprite = result.Cocktail.Icon;
            }
            else
            {
                _nameText.text = "Failed Drink";
                _drinkImage.sprite = result.Cocktail.FailedIcon;
                _description.text = _failText[Random.Range(0, _failText.Count)];
            }
            
            _ratings[0].SetData(result.MixSuccess, result.MixMessage);
            _ratings[1].SetData(result.FillSuccess, result.FillMessage);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private List<string> _failText = new List<string>()
        {
            "You tried, but maybe you shouldn't have",
            "It's not clear what this is, but it's not what they wanted",
            "It looks like it might still be drinkable",
            "Were you intentionally trying to screw this up?",
            "Giving this to a customer could be considered a jailable offense"
        };

    }
}