using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("References")]
    public GameState gameState;
    public Image menuBackground;

    [Header("Menu Backgrounds")]
    public Sprite normalBackground;
    public Sprite unsettlingBackground;
    public Sprite fullHorrorBackground;

    private void Start()
    {
        UpdateMenuBackground();
    }

    public void UpdateMenuBackground()
    {
        if (gameState == null || menuBackground == null)
        {
            Debug.LogError("Missing references in MenuManager.");
            return;
        }

        switch (gameState.CurrentHorrorLevel)
        {
            case GameState.HorrorLevel.Normal:
                menuBackground.sprite = normalBackground;
                Debug.Log("Menu background set to Normal.");
                break;

            case GameState.HorrorLevel.Unsettling:
                menuBackground.sprite = unsettlingBackground;
                Debug.Log("Menu background set to Unsettling.");
                break;

            case GameState.HorrorLevel.FullHorror:
                menuBackground.sprite = fullHorrorBackground;
                Debug.Log("Menu background set to Full Horror.");
                break;

            default:
                Debug.LogWarning("Unknown Horror Level. No background change.");
                break;
        }
    }
}