using UnityEngine;
using TMPro;

public class GameStateUI : MonoBehaviour
{
    [Header("References")]
    public GameState gameState;
    public TMP_Text stateTextBox;
    public NextCustomerHandler nextCustomerHandler;

    private void Update()
    {
        UpdateStateText();
    }

    private void UpdateStateText()
    {
        if (gameState == null || stateTextBox == null || nextCustomerHandler == null)
        {
            Debug.LogError("Missing references in GameStateUI.");
            return;
        }

        string stage = gameState.CurrentStage.ToString();
        string horrorLevel = gameState.CurrentHorrorLevel.ToString();
        string characterName = gameState.CurrentCharacter != null ? gameState.CurrentCharacter.characterName : "No Character";
        string score = nextCustomerHandler.score.ToString("F2");

        stateTextBox.text = $"Stage: {stage}\nHorror Level: {horrorLevel}\nCharacter: {characterName}\nScore: {score}";
    }
}