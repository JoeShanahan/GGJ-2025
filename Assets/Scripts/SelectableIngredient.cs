using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectableIngredient : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {
    public UnityEngine.UI.Image image;

    public UnityEngine.UI.Button nextButton;

    public Color alphaZero = new Color(1, 1, 1, 0);

    public Color alpha = new Color(0.3f, 0.3f, 0.3f, 1);

    private Vector3 selectedDelta = new Vector3(1.3f, 0, 0);
    public Vector3 inactiveScale;
    public Vector3 highlightScale;
    public Vector3 selectedScale;

    private bool isOver;
    private bool isSelected;
    [SerializeField]
    public Transform parent;

    public IngredientSelectionManager ingredientSelectionManager;
    public Transform barPosition;

    private Vector3 startPosition;

    public IngredientData ingredient;

    [SerializeField]
    private AudioSource bottleClinkAudioSource; 

    void Start() {
        startPosition = parent.transform.position;

        if (bottleClinkAudioSource == null) {
            Debug.LogWarning("No AudioSource specified for " + gameObject.name + ".");
            bottleClinkAudioSource = GetComponent<AudioSource>();
            if (bottleClinkAudioSource == null) {
                bottleClinkAudioSource = gameObject.AddComponent<AudioSource>();
            }
        }
    }

    void Update() {
        if (isSelected) {
            parent.transform.position = Vector3.Lerp(parent.transform.position, barPosition.position + (selectedDelta * ingredientSelectionManager.PositionInList(ingredient)), Time.deltaTime * 8);
            parent.transform.localScale = Vector3.Lerp(parent.transform.localScale, selectedScale, Time.deltaTime * 8);
        } else {
            parent.transform.position = Vector3.Lerp(parent.transform.position, startPosition, Time.deltaTime * 8);
        }
        if (isOver) {
            image.color = Color.Lerp(image.color, alphaZero, Time.deltaTime * 8);
            parent.transform.localScale = Vector3.Lerp(parent.transform.localScale, highlightScale, Time.deltaTime * 8);
        } else {
            image.color = Color.Lerp(image.color, alpha, Time.deltaTime * 8);
            parent.transform.localScale = Vector3.Lerp(parent.transform.localScale, inactiveScale, Time.deltaTime * 8);
        }
        if (ingredientSelectionManager.ingredientsSelected.Length > 0) {
            nextButton.gameObject.SetActive(true);
        } else {
            nextButton.gameObject.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData) {
        isOver = true;
    }

    public void OnPointerExit(PointerEventData eventData) {
        isOver = false;
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (isOver) {
            if (ingredientSelectionManager.IsInList(ingredient)) {
                ingredientSelectionManager.RemoveIngredient(ingredient);
                isSelected = false;
                PlayIngredientAudio();
            } else {
                if (ingredientSelectionManager.ingredientsSelected.Length < 5) {
                    ingredientSelectionManager.AddIngredient(ingredient);
                    isSelected = true;
                    PlayIngredientAudio();
                }
            }
        }
    }

    private void PlayIngredientAudio() {
        if (ingredient != null && ingredient.BottleClink != null) {
            bottleClinkAudioSource.clip = ingredient.BottleClink;
            bottleClinkAudioSource.Play();
        } else {
            Debug.LogWarning($"No BottleClink sound assigned for ingredient: {ingredient?.IngredientName}");
        }
    }
}