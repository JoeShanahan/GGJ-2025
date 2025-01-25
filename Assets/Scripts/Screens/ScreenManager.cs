using UnityEngine;
using DG.Tweening;
using UnityEngine.XR;

namespace GGJ2025.Screens
{
    public class ScreenManager : MonoBehaviour
    {
        [SerializeField] private float _fadeTime = 0.4f;
        
        [Space(8)]
        [SerializeField] private DayNumberScreen _dayNumberScreen;
        [SerializeField] private DialogueScreen _dialogueScreen;
        [SerializeField] private PickIngredientsScreen _pickScreen;
        [SerializeField] private PourDrinkScreen _pourScreen;
        [SerializeField] private GameOverScreen _gameOverScreen;
        
        [Space(16)]
        [SerializeField] private CanvasGroup _fader;

        private GameObject _currentlyActiveScreen;
        private bool _inTransition;
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                ShowOrderScreen();
            
            if (Input.GetKeyDown(KeyCode.Alpha2))
                ShowIngredientPickingUI();
            
            if (Input.GetKeyDown(KeyCode.Alpha3))
                ShowPouringScreen();
            
            if (Input.GetKeyDown(KeyCode.Alpha4))
                ShowGameOverScreen();
            
            if (Input.GetKeyDown(KeyCode.Alpha5))
                ShowDayNumber();
            
            if (Input.GetKeyDown(KeyCode.Alpha6))
                ShowResponseScreen();
        }

        public void ShowDayNumber() => GoToScreen(_dayNumberScreen.gameObject);
        
        public void ShowOrderScreen()
        {
            GoToScreen(_dialogueScreen.gameObject);
            // TODO set mode of screen to ordering
        }
        
        public void ShowResponseScreen()
        {
            GoToScreen(_dialogueScreen.gameObject);
            // TODO set mode of screen to response
        }

        public void ShowIngredientPickingUI() => GoToScreen(_pickScreen.gameObject);

        public void ShowPouringScreen() => GoToScreen(_pourScreen.gameObject);

        public void ShowGameOverScreen() => GoToScreen(_gameOverScreen.gameObject);

        private void GoToScreen(GameObject newScreen)
        {
            if (newScreen.gameObject.activeSelf || _inTransition)
                return;
            
            _fader.gameObject.SetActive(true);
            _fader.alpha = 0;
            _inTransition = true;

            _fader.DOFade(1, _fadeTime / 2).SetEase(Ease.Linear).OnComplete(() =>
            {
                _currentlyActiveScreen?.SetActive(false);
                newScreen.gameObject.SetActive(true);
                _currentlyActiveScreen = newScreen;
                
                _fader.DOFade(0, _fadeTime / 2).SetEase(Ease.Linear).OnComplete(() =>
                {
                    _fader.gameObject.SetActive(false);
                    _inTransition = false;
                });
            });

        }
    }
}