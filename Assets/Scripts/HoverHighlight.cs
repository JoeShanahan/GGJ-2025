using UnityEngine;

public class HoverHighlight : MonoBehaviour {
    private Vector3 originalScale;
    private Vector3 childOriginalScale;
    [SerializeField] private float upScale = 1.5f;
    [SerializeField] private float additionalChildScalePercent = 0.1f;

    [SerializeField] private Transform childTransform; 

    private void Start() {
        originalScale = transform.localScale;

        if (childTransform != null) {
            childOriginalScale = childTransform.localScale;
        } else {
            Debug.LogWarning("Child Transform is not assigned. Please assign it in the Inspector.", this);
        }
    }

    private void OnMouseEnter() {
        transform.localScale = originalScale * upScale;

        if (childTransform != null) {
            float totalChildScaleMultiplier = 1f + (additionalChildScalePercent);
            childTransform.localScale = childOriginalScale * totalChildScaleMultiplier;
        }
    }

    private void OnMouseExit() {
        transform.localScale = originalScale;

        if (childTransform != null) {
            childTransform.localScale = childOriginalScale;
        }
    }
}