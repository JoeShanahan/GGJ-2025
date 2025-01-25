using UnityEngine;

public class GameState : MonoBehaviour
{
    public Shift[] ShiftData;
    public enum Stage
    {
        Stage1 = 1,
        Stage2 = 2,
        Stage3 = 3
    }

    public enum HorrorLevel
    {
        Normal,
        Unsettling,
        FullHorror
    }

    [Header("Game State Settings")]
    [SerializeField]
    private Stage currentStage = Stage.Stage1;

    [SerializeField]
    private HorrorLevel currentHorrorLevel = HorrorLevel.Normal;

    [Header("Characters")]
    [SerializeField]
    private CharacterData[] characters;

    private int currentCharacterIndex = 0;

    public Stage CurrentStage
    {
        get => currentStage;
        set
        {
            currentStage = value;
            Debug.Log($"Stage set to: {currentStage}");
        }
    }

    public HorrorLevel CurrentHorrorLevel
    {
        get => currentHorrorLevel;
        set
        {
            currentHorrorLevel = value;
            Debug.Log($"Horror level set to: {currentHorrorLevel}");
        }
    }

    public CharacterData CurrentCharacter
    {
        get => characters.Length > 0 ? characters[currentCharacterIndex] : null;
    }

    public void SetStage(int stageNumber)
    {
        if (stageNumber >= 1 && stageNumber <= 3)
        {
            CurrentStage = (Stage)stageNumber;
        }
        else
        {
            Debug.LogError("Invalid stage number. Valid values are 1, 2, or 3.");
        }
    }

    public void SetHorrorLevel(string horrorLevel)
    {
        if (System.Enum.TryParse(horrorLevel, true, out HorrorLevel parsedLevel))
        {
            CurrentHorrorLevel = parsedLevel;
        }
        else
        {
            Debug.LogError("Invalid horror level. Valid values are Normal, Unsettling, or FullHorror.");
        }
    }

    public void IncreaseStage()
    {
        if (currentStage < Stage.Stage3)
        {
            CurrentStage = currentStage + 1;
        }
        else
        {
            Debug.LogWarning("Cannot increase stage. Already at maximum stage.");
        }
    }

    public void DecreaseStage()
    {
        if (currentStage > Stage.Stage1)
        {
            CurrentStage = currentStage - 1;
        }
        else
        {
            Debug.LogWarning("Cannot decrease stage. Already at minimum stage.");
        }
    }

    public void IncreaseHorrorLevel()
    {
        if (currentHorrorLevel < HorrorLevel.FullHorror)
        {
            CurrentHorrorLevel = currentHorrorLevel + 1;
        }
        else
        {
            Debug.LogWarning("Cannot increase horror level. Already at maximum level.");
        }
    }

    public void DecreaseHorrorLevel()
    {
        if (currentHorrorLevel > HorrorLevel.Normal)
        {
            CurrentHorrorLevel = currentHorrorLevel - 1;
        }
        else
        {
            Debug.LogWarning("Cannot decrease horror level. Already at minimum level.");
        }
    }

    public void NextCharacter()
    {
        if (characters.Length > 0)
        {
            currentCharacterIndex = (currentCharacterIndex + 1) % characters.Length;
            Debug.Log($"Switched to next character: {CurrentCharacter.characterName}");
        }
        else
        {
            Debug.LogWarning("No characters available to switch to.");
        }
    }

    public void PreviousCharacter()
    {
        if (characters.Length > 0)
        {
            currentCharacterIndex = (currentCharacterIndex - 1 + characters.Length) % characters.Length;
            Debug.Log($"Switched to previous character: {CurrentCharacter.characterName}");
        }
        else
        {
            Debug.LogWarning("No characters available to switch to.");
        }
    }
}