using UnityEngine;
using DG.Tweening;
using GGJ2025.PouringGame;
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

        private void Start()
        {
            ShowDayNumber();
        }
        
        public void SetShiftStarted(Shift shifty)
        {
            _dayNumberScreen.SetShiftStarted(shifty);
            ShowDayNumber();
        }

        public void SetOrderStarted(OrderInfo info, GameState.HorrorLevel horrorLevel)
        {
            _dialogueScreen.SetIntroduction(info, horrorLevel);
        }

        public void SetOrderComplete(DrinkMakeResult result, OrderInfo info)
        {
            _dialogueScreen.SetComplete(info, result.WasSuccess);
            GoToScreen(_dialogueScreen.gameObject);
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

        public void ShowIngredientPickingUI(GameState.HorrorLevel currentHorrorLevel) {
            bool isComingFromDialogue = _currentlyActiveScreen == _dialogueScreen.gameObject;
            bool isFullHorror = currentHorrorLevel == GameState.HorrorLevel.FullHorror;

            Debug.Log($"Transitioning to Pick Screen. Coming from Dialogue: {isComingFromDialogue}, Full Horror: {isFullHorror}");

            GoToScreen(_pickScreen.gameObject);

            if (isComingFromDialogue && isFullHorror) {
                IngredientSelectionManager ingredientManager = FindObjectOfType<IngredientSelectionManager>();
                if (ingredientManager != null) {
                    Debug.Log("Showing Knife in Ingredient Selection!");
                    ingredientManager.ShowKnife(true);
                }
            }
        }

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