using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameState : MonoBehaviour
{
    public Shift[] ShiftData;

    private Shift _currentShift;
    public enum HorrorLevel
    {
        Normal,
        Unsettling,
        FullHorror
    }

    [Header("Game State Settings")]
    [SerializeField]
    private int currentStage;

    [SerializeField]
    private HorrorLevel currentHorrorLevel = HorrorLevel.Normal;

    [Header("Characters")]
    [SerializeField]
    private CharacterData[] characters;

    [Header("Failed Customers")]
    [SerializeField]
    private List<CharacterData> failedCustomers = new List<CharacterData>();

    [Header("Successful Customers")]
    [SerializeField]
    private List<CharacterData> successfulCustomers = new List<CharacterData>();

    private int currentCharacterIndex = 0;
    
    [Header("Shift Management")]
    [SerializeField]
    private int currentShiftIndex = 0;

    [Header("Game Status")]
    [SerializeField]
    private bool gameOverState = false;

    public bool GameOverState
    {
        get => gameOverState;
        set
        {
            gameOverState = value;
            if (gameOverState)
            {
                Debug.Log("Game Over State has been triggered.");
            }
        }
    }

    public int CurrentStage
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

    [Header("Canvas Image and Scene Sprites")]
    public UnityEngine.UI.Image customerBackground;
    public UnityEngine.UI.Image customerForeground;
    public SpriteRenderer barBackground;
    public SpriteRenderer barForeground;

    [Header("Horror Level Graphics")]
    public Sprite normalCustomerBackground;
    public Sprite normalCustomerForeground;
    public Sprite normalBarForeground;
    public Sprite normalBarBackground;

    public Sprite unsettlingCustomerBackground;
    public Sprite unsettlingCustomerForeground;
    public Sprite unsettlingBarBackground;
    public Sprite unsettlingBarForeground;

    public Sprite fullHorrorCustomerBackground;
    public Sprite fullHorrorCustomerForeground;
    public Sprite fullHorrorBarBackground;
    public Sprite fullHorrorBarForeground;

    private void OnShiftStart()
    {
        currentCharacterIndex = 0;
        Debug.Log("Current character index reset to 0.");

        _currentShift = null;
        
        foreach (Shift s in ShiftData)
        {
            if (s.stage == currentShiftIndex && s.horrorLevel == currentHorrorLevel)
            {
                _currentShift = s;
                break;
            }
        }
        
        if (_currentShift == null)
            Debug.LogError($"COULD NOT FIND SHIFT {currentShiftIndex} - {currentHorrorLevel}");

        characters = successfulCustomers.ToArray();
        Debug.Log("Repopulated characters list with successful customers.");

        failedCustomers.Clear();
        Debug.Log("Cleared the failed customers list for this shift.");
    }

    private void Start()
    {
        OnGameStart();
    }

    private void OnGameStart()
    {
        OnShiftStart();
    }

    private void OnOrderStart()
    {
        //Get Current order from current shift

        //Send intro dialogue to dialogue system

        Canvas environmentCanvas = FindObjectOfType<Canvas>();
        SpriteRenderer[] sceneSprites = FindObjectsOfType<SpriteRenderer>();

        if (environmentCanvas == null || sceneSprites.Length < 2)
        {
            Debug.LogError("Environment canvas or scene sprites are missing.");
            return;
        }

        Image customerBackground = environmentCanvas.GetComponentInChildren<Image>();
        if (customerBackground == null)
        {
            Debug.LogError("Canvas image is missing.");
            return;
        }

        Image customerForeground = environmentCanvas.GetComponentInChildren<Image>();
        if (customerForeground == null)
        {
            Debug.LogError("Canvas image is missing.");
            return;
        }

        if (customerBackground != null && customerForeground != null && barBackground != null && barForeground != null)
        {
            switch (CurrentHorrorLevel)
            {
                case HorrorLevel.Normal:
                    customerBackground.sprite = normalCustomerBackground;
                    customerForeground.sprite = normalCustomerForeground;
                    barBackground.sprite = normalBarBackground;
                    barForeground.sprite = normalBarForeground;
                    break;
                case HorrorLevel.Unsettling:
                    customerBackground.sprite = unsettlingCustomerBackground;
                    customerForeground.sprite = unsettlingCustomerForeground;
                    barBackground.sprite = unsettlingBarBackground;
                    barForeground.sprite = unsettlingBarForeground;
                    break;
                case HorrorLevel.FullHorror:
                    customerBackground.sprite = fullHorrorCustomerBackground;
                    customerForeground.sprite = fullHorrorCustomerForeground;
                    barBackground.sprite = fullHorrorBarBackground;
                    barForeground.sprite = fullHorrorBarForeground;
                    break;
                default:
                    Debug.LogWarning("Horror level not recognized. Graphics not updated.");
                    break;
            }
        }
        else
        {
            Debug.LogError("Canvas or scene sprites are not set.");
        }

        CharacterImageManager characterImageManager = FindObjectOfType<CharacterImageManager>();
        if (characterImageManager != null)
        {
            characterImageManager.UpdateCharacterImage();
        }
        else
        {
            Debug.LogError("CharacterImageManager not found in the scene.");
        }
    }

    private void OnShiftEnd()
    {
        if (failedCustomers.Count == 0)
        {
            IncreaseHorrorLevel();
            Debug.Log("No customers failed. Horror level increased.");
        }
        currentShiftIndex++;
        Debug.Log($"Shift increased to: {currentShiftIndex}");
    }

    private void OnOrderEnd(bool success)
    {
        //Check score, if failed add customer to failed list

        //Send success/fail dialogue to dialogue system

        //If failed, incriment failed this shift
    }

    public void CompleteShift(bool success)
    {
        if (characters.Length == 0)
        {
            GameOverState = true;
            Debug.Log("Game Over: No customers remaining.");
        }

        if (characters.Length == 0)
        {
            OnShiftEnd();
            Debug.Log("No customers remaining. Shift ending.");
        }

        //If past all available shifts, show ending

        if (GameOverState)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
            Debug.Log("Transitioning to GameOver scene.");
        }

        OnShiftStart();
    }

    public CharacterData[] GetCharactersArray()
    {
        return characters;
    }

    public void UpdateCharacters(CharacterData[] newCharacters)
    {
        characters = newCharacters;
        currentCharacterIndex = Mathf.Clamp(currentCharacterIndex, 0, characters.Length - 1);
    }

    public void AddFailedCustomer(CharacterData character)
    {
        if (!failedCustomers.Contains(character))
        {
            failedCustomers.Add(character);
            Debug.Log($"Added {character.characterName} to failed customers.");
        }
    }

    public void AddSuccessfulCustomer(CharacterData character)
    {
        if (!successfulCustomers.Contains(character))
        {
            successfulCustomers.Add(character);
            Debug.Log($"Added {character.characterName} to successful customers.");
        }
    }

    public List<CharacterData> GetFailedCustomers()
    {
        return failedCustomers;
    }

    public List<CharacterData> GetSuccessfulCustomers()
    {
        return successfulCustomers;
    }

    public void SetStage(int stageNumber)
    {
        if (stageNumber >= 1 && stageNumber <= 3)
        {
            CurrentStage = stageNumber;
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
        if (currentStage < 2)
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
        if (currentStage > 0)
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