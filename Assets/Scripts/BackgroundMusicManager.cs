using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    [Header("References")]
    public GameFlowController gameFlowController;
    public AudioSource audioSource;

    [Header("Music Tracks")]
    public AudioClip normalMusic;
    public AudioClip unsettlingMusic;
    public AudioClip fullHorrorMusic;

    private GameState.HorrorLevel currentHorrorLevel;

    private void Start()
    {
        if (gameFlowController == null || audioSource == null)
        {
            Debug.LogError("Missing references in MusicManager.");
            return;
        }

        audioSource.loop = true;
        UpdateMusic();
    }

    private void Update()
    {
        if (gameFlowController._currentHorrorLevel != currentHorrorLevel)
        {
            UpdateMusic();
        }
    }

    private void UpdateMusic()
    {
        currentHorrorLevel = gameFlowController._currentHorrorLevel;

        switch (currentHorrorLevel)
        {
            case GameState.HorrorLevel.Normal:
                audioSource.clip = normalMusic;
                Debug.Log("Playing Normal Music.");
                break;

            case GameState.HorrorLevel.Unsettling:
                audioSource.clip = unsettlingMusic;
                Debug.Log("Playing Unsettling Music.");
                break;

            case GameState.HorrorLevel.FullHorror:
                audioSource.clip = fullHorrorMusic;
                Debug.Log("Playing Full Horror Music.");
                break;

            default:
                Debug.LogWarning("Unknown Horror Level. No music change.");
                return;
        }

        audioSource.Play();
    }
}