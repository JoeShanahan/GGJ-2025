using UnityEngine;

public class DebugText : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        #if !UNITY_EDITOR
        gameObject.SetActive(false);
        #endif
    }
}
