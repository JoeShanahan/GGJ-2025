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
}