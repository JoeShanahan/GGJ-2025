using UnityEngine;

public class DebugCanvasToggle : MonoBehaviour
{
    [Header("References")]
    public GameObject debugCanvas;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Equals))
        {
            ToggleDebugCanvas();
        }
    }

    private void ToggleDebugCanvas()
    {
        if (debugCanvas != null)
        {
            debugCanvas.SetActive(!debugCanvas.activeSelf);
            Debug.Log($"DebugCanvas is now {(debugCanvas.activeSelf ? "open" : "closed")}.");
        }
        else
        {
            Debug.LogError("DebugCanvas reference is missing.");
        }
    }
}