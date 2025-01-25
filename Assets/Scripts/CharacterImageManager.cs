using UnityEngine;
using UnityEngine.UI;

public class CharacterImageManager : MonoBehaviour
{
    [Header("References")]
    public GameState gameState;
    public Image characterImage;

    private void Update()
    {
        UpdateCharacterImage();
    }

    private void UpdateCharacterImage()
    {
        if (gameState == null || characterImage == null)
        {
            Debug.LogError("Missing references in CharacterImageManager.");
            return;
        }

        CharacterData currentCharacter = gameState.CurrentCharacter;

        if (currentCharacter == null)
        {
            Debug.LogError("No active character found in GameState.");
            return;
        }

        switch (gameState.CurrentHorrorLevel)
        {
            case GameState.HorrorLevel.Normal:
                characterImage.sprite = currentCharacter.normal;
                break;

            case GameState.HorrorLevel.Unsettling:
                characterImage.sprite = currentCharacter.unsettling;
                break;

            case GameState.HorrorLevel.FullHorror:
                characterImage.sprite = currentCharacter.fullHorror;
                break;

            default:
                Debug.LogWarning("Unknown horror level. No sprite assigned.");
                break;
        }
    }
}