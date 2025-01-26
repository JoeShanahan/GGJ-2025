using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("References")]
    public GameFlowController gameFlowController;
    public Image menuBackground;

    [Header("Menu Backgrounds")]
    public Sprite normalBackground;
    public Sprite unsettlingBackground;
    public Sprite fullHorrorBackground;

    [Header("Togglable Image")]
    public GameObject togglableImage;

    private void Start()
    {
        UpdateMenuBackground();
    }

    public void UpdateMenuBackground()
    {
        if (gameFlowController == null || menuBackground == null)
        {
            Debug.LogError("Missing references in MenuManager.");
            return;
        }

        switch (gameFlowController._currentHorrorLevel)
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

    public void ToggleImageActiveState()
    {
        if (togglableImage == null)
        {
            Debug.LogError("Togglable image reference is missing.");
            return;
        }

        togglableImage.SetActive(!togglableImage.activeSelf);
        Debug.Log($"Togglable image is now {(togglableImage.activeSelf ? "active" : "inactive")}.");
    }
}