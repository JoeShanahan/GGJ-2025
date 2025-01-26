using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace GGJ2025.Screens
{
    public class DayNumberScreen : MonoBehaviour
    {
        [SerializeField]
        private Text _text;

        [SerializeField] 
        private Color _red;
        
        public void SetShiftStarted(Shift shifty)
        {
            if (shifty.horrorLevel == GameState.HorrorLevel.FullHorror)
            {
                _text.color = _red;
            }

            if (shifty.stage == 0)
            {
                _text.text = "First Day";
            }
            else if (shifty.stage == 1)
            {
                _text.text = "Second Day";
            }
            else if (shifty.stage == 2)
            {
                _text.text = "Final Day";
            }
        }

        private void OnEnable()
        {
            StartCoroutine(StartRoutine());
        }

        private IEnumerator StartRoutine()
        {
            yield return new WaitForSeconds(3);
            FindFirstObjectByType<ScreenManager>().ShowOrderScreen();
        }

    }
}
