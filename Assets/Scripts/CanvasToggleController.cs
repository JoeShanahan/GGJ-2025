using UnityEngine;
using UnityEngine.UI;

public class ImageToggleController : MonoBehaviour {

    [SerializeField] private Image targetImage;

    public void ToggleImageVisibility() {
        if (targetImage != null) {
            targetImage.gameObject.SetActive(!targetImage.gameObject.activeSelf);
        } else {
            Debug.LogWarning("Target Image is not assigned");
        }
    }
}