using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    [Header("References")]
    public GameState gameState;
    public AudioSource audioSource;

    [Header("Background Music Clips")]
    public AudioClip normalMusic;
    public AudioClip unsettlingMusic;
    public AudioClip fullHorrorMusic;

    private GameState.HorrorLevel currentHorrorLevel;

    private void Start()
    {
        if (audioSource == null || gameState == null)
        {
            Debug.LogError("Missing references in BackgroundMusicManager.");
            return;
        }

        audioSource.loop = true;
        UpdateBackgroundMusic();
    }

    private void Update()
    {
        if (gameState.CurrentHorrorLevel != currentHorrorLevel)
        {
            UpdateBackgroundMusic();
        }
    }

    private void UpdateBackgroundMusic()
    {
        currentHorrorLevel = gameState.CurrentHorrorLevel;

        switch (currentHorrorLevel)
        {
            case GameState.HorrorLevel.Normal:
                audioSource.clip = normalMusic;
                Debug.Log("Playing Normal Background Music.");
                break;

            case GameState.HorrorLevel.Unsettling:
                audioSource.clip = unsettlingMusic;
                Debug.Log("Playing Unsettling Background Music.");
                break;

            case GameState.HorrorLevel.FullHorror:
                audioSource.clip = fullHorrorMusic;
                Debug.Log("Playing Full Horror Background Music.");
                break;

            default:
                Debug.LogWarning("Unknown Horror Level. No music change.");
                return;
        }

        audioSource.Play();
    }
}