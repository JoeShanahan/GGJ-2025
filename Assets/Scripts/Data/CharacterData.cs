using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterData", menuName = "ScriptableObjects/CharacterData", order = 1)]
public class CharacterData : ScriptableObject
{
    [Header("Character Information")]
    public string characterName;

    [Header("Visuals")]
    public Sprite normal;
    public Sprite unsettling;
    public Sprite fullHorror;

    public Sprite GetSprite(GameState.HorrorLevel hor)
    {
        if (hor == GameState.HorrorLevel.Normal) return normal;
        if (hor == GameState.HorrorLevel.Unsettling) return unsettling;
        if (hor == GameState.HorrorLevel.FullHorror) return fullHorror;
        return normal;
    }
}