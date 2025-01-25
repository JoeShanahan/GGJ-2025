using UnityEngine;

[System.Serializable]
public class OrderInfo
{
    public CharacterData customer;
    public CocktailData cocktail;

    [TextArea(3, 10)]
    public string[] introText;
    [TextArea(3, 10)]
    public string[] failText;
    [TextArea(3, 10)]
    public string[] successText;
}
