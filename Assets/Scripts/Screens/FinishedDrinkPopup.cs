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
    }
}