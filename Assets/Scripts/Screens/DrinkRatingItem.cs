using UnityEngine;
using UnityEngine.UI;

namespace GGJ2025.Screens
{
    public class DrinkRatingItem : MonoBehaviour
    {
        [SerializeField] private Text _text;
        [SerializeField] private RectTransform _checkmark;
        [SerializeField] private RectTransform _cross;
        [SerializeField] private Image _background;

        [Header("Colors")]
        [SerializeField] private Color _goodBg;
        [SerializeField] private Color _goodFg;
        [Space(8)]
        [SerializeField] private Color _badBg;
        [SerializeField] private Color _badFg;
    }
}