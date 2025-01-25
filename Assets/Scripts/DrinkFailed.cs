using UnityEngine;
using System.Collections.Generic;

public class DrinkFailed : MonoBehaviour
{
    [Header("Failure Flags")]
    private Dictionary<string, bool> failedFlags = new Dictionary<string, bool>();

    [Header("References")]
    public GameState gameState;

    public void OnDrinkFailed()
    {
        if (gameState == null)
        {
            Debug.LogError("GameState reference is missing.");
            return;
        }

        CharacterData currentCharacter = gameState.CurrentCharacter;

        if (currentCharacter == null)
        {
            Debug.LogError("No active character found in GameState.");
            return;
        }

        if (!failedFlags.ContainsKey(currentCharacter.characterName))
        {
            failedFlags[currentCharacter.characterName] = true;
            Debug.Log($"{currentCharacter.characterName} has failed.");
        }
        else
        {
            Debug.LogWarning($"{currentCharacter.characterName} is already marked as failed.");
        }
    }

    public void HandleFailedCustomers()
    {
        if (gameState == null)
        {
            Debug.LogError("GameState reference is missing.");
            return;
        }

        var charactersArray = gameState.GetCharactersArray();
        List<CharacterData> remainingCharacters = new List<CharacterData>();

        foreach (CharacterData character in charactersArray)
        {
            if (failedFlags.ContainsKey(character.characterName) && failedFlags[character.characterName])
            {
                gameState.AddFailedCustomer(character);
                Debug.Log($"Moved {character.characterName} to failed customers.");
            }
            else
            {
                remainingCharacters.Add(character);
            }
        }

        gameState.UpdateCharacters(remainingCharacters.ToArray());
    }
}