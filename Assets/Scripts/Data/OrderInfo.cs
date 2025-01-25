using UnityEngine;

[System.Serializable]
public class OrderInfo
{
    public CharacterData customer;
    public CocktailData cocktail;

    public DialogueLine[] introText;
    public DialogueLine[] failText;
    public DialogueLine[] successText;
}

[System.Serializable]
public class DialogueLine
{
    [TextArea(3, 10)]
    public string Text;

    public bool IsMe;
}
