using System;
using UnityEngine;

public class PickableIngredient : MonoBehaviour
{
    public SpriteRenderer sprite;

    public Color inactiveColor;
    public Color highlightColor;
    
    public Vector3 inactiveScale;
    public Vector3 highlightScale;
    public Vector3 selectedScale;

    private bool isOver;
    private bool isSelected;

    // Update is called once per frame
    void Update()
    {
        if (isSelected)
        {
            sprite.color = Color.Lerp(sprite.color, Color.white, Time.deltaTime * 8);
            sprite.transform.localScale = Vector3.Lerp(sprite.transform.localScale, selectedScale, Time.deltaTime * 8);
        }
        else if (isOver)
        {
            sprite.color = Color.Lerp(sprite.color, highlightColor, Time.deltaTime * 8);
            sprite.transform.localScale = Vector3.Lerp(sprite.transform.localScale, highlightScale, Time.deltaTime * 8);
        }
        else
        {
            sprite.color = Color.Lerp(sprite.color, inactiveColor, Time.deltaTime * 8);
            sprite.transform.localScale = Vector3.Lerp(sprite.transform.localScale, inactiveScale, Time.deltaTime * 8);
        }
    }

    public void OnMouseEnter()
    {
        isOver = true;
    }

    public void OnMouseExit()
    {
        isOver = false;
    }

    public void OnMouseDown()
    {
        if (isOver)
        {
            isSelected = !isSelected;
        }
    }
}
