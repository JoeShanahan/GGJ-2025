using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SelectableIngredient : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public UnityEngine.UI.Image image;

    public UnityEngine.UI.Button nextButton;

    public Color alphaZero = new Color(1, 1, 1, 0);

    public Color alpha = new Color(0.3f, 0.3f, 0.3f, 1);
    
    private Vector3 selectedDelta = new Vector3(110, 0, 0);
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

    void Start()
    {
        startPosition = parent.transform.position;

    }

    void Update()
    {
        if (isSelected)
        {
            parent.transform.position = Vector3.Lerp(parent.transform.position, barPosition.position + (selectedDelta * ingredientSelectionManager.PositionInList(ingredient)), Time.deltaTime * 8);
            parent.transform.localScale = Vector3.Lerp(parent.transform.localScale, selectedScale, Time.deltaTime * 8);
        }
        else
        {
            parent.transform.position = Vector3.Lerp(parent.transform.position, startPosition, Time.deltaTime * 8);
        }
        if (isOver)
        {
            image.color = Color.Lerp(image.color, alphaZero, Time.deltaTime * 8);
            parent.transform.localScale = Vector3.Lerp(parent.transform.localScale, highlightScale, Time.deltaTime * 8);
        }
        else
        {
            image.color = Color.Lerp(image.color, alpha, Time.deltaTime * 8);
            parent.transform.localScale = Vector3.Lerp(parent.transform.localScale, inactiveScale, Time.deltaTime * 8);
        }
        if (ingredientSelectionManager.ingredientsSelected.Length > 0)
        {
            nextButton.gameObject.SetActive(true);
        }
        else
        {
            nextButton.gameObject.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isOver = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isOver)
        {            
            if (ingredientSelectionManager.IsInList(ingredient))
            {
                ingredientSelectionManager.RemoveIngredient(ingredient);
                isSelected = false;
            }
            else
            {
                if (ingredientSelectionManager.ingredientsSelected.Length < 5)
                {
                    ingredientSelectionManager.AddIngredient(ingredient);
                    isSelected = true;
                }
            }
        }
    }
}