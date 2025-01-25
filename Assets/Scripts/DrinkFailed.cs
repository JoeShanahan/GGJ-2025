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

            gameState.AddFailedCustomer(currentCharacter);
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
        var failedCustomers = gameState.GetFailedCustomers();
        List<CharacterData> validCustomers = new List<CharacterData>();

        foreach (CharacterData character in charactersArray)
        {
            if (!failedCustomers.Contains(character))
            {
                validCustomers.Add(character);
            }
        }

        gameState.UpdateCharacters(validCustomers.ToArray());
    }
}
