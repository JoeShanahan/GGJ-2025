using UnityEngine;
using System.Collections.Generic;

public class DrinkSucceeded : MonoBehaviour
{
    [Header("Success Flags")]
    private Dictionary<string, bool> successFlags = new Dictionary<string, bool>();

    [Header("References")]
    public GameState gameState;

    public void OnDrinkSuccess()
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

        if (!successFlags.ContainsKey(currentCharacter.characterName))
        {
            successFlags[currentCharacter.characterName] = true;
            Debug.Log($"{currentCharacter.characterName} has succeeded.");

            gameState.AddSuccessfulCustomer(currentCharacter);
        }
        else
        {
            Debug.LogWarning($"{currentCharacter.characterName} is already marked as succeeded.");
        }
    }

    public void HandleSuccessfulCustomers()
    {
        if (gameState == null)
        {
            Debug.LogError("GameState reference is missing.");
            return;
        }

        var charactersArray = gameState.GetCharactersArray();
        var successfulCustomers = gameState.GetSuccessfulCustomers();
        List<CharacterData> validCustomers = new List<CharacterData>();

        foreach (CharacterData character in charactersArray)
        {
            if (!successfulCustomers.Contains(character))
            {
                validCustomers.Add(character);
            }
        }

        gameState.UpdateCharacters(validCustomers.ToArray());
    }
}