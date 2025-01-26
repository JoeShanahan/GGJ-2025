using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayGame : MonoBehaviour
{
    public void LoadMainScene()
    {
        SceneManager.LoadScene("CursedMixology");
        Debug.Log("Loading main scene: CursedMixology");
    }
}