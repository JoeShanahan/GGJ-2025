using UnityEngine;
using UnityEngine.UI;

namespace GGJ2025.Screens
{
    public class DrinkRatingItem : MonoBehaviour
    {
        [SerializeField] private Text _text;
        [SerializeField] private Image _checkmark;
        [SerializeField] private Image _cross;
        [SerializeField] private Image _background;

        [Header("Colors")]
        [SerializeField] private Color _goodBg;
        [SerializeField] private Color _goodFg;
        [Space(8)]
        [SerializeField] private Color _badBg;
        [SerializeField] private Color _badFg;

        public void SetData(bool success, string message)
        {
            _checkmark.gameObject.SetActive(success);
            _cross.gameObject.SetActive(!success);
            _text.text = message;

            _background.color = success ? _goodBg : _badBg;
            _text.color = success ? _goodFg : _badFg;
            _checkmark.color = success ? _goodFg : _badFg;
            _cross.color = success ? _goodFg : _badFg;
        }
    }
}