using UnityEngine;

public class BottleClink : MonoBehaviour
{
    [Header("Audio Settings")]
    public AudioClip clinkSound;
    public float collisionCooldown = 0.05f;

    private AudioSource audioSource;
    private float lastCollisionTime = -Mathf.Infinity;

    private void Start()
    {
        GameObject audioListenerObject = FindObjectOfType<AudioListener>()?.gameObject;

        if (audioListenerObject == null)
        {
            Debug.LogError("No AudioListener found in the scene.");
            return;
        }

        audioSource = audioListenerObject.GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = audioListenerObject.AddComponent<AudioSource>();
            Debug.LogWarning("No AudioSource found on the AudioListener. A new AudioSource has been added.");
        }

        if (clinkSound == null)
        {
            Debug.LogError("Clink sound is not assigned.");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Time.time - lastCollisionTime < collisionCooldown)
        {
            return;
        }

        lastCollisionTime = Time.time;

        if (audioSource != null && clinkSound != null)
        {
            audioSource.Stop();
            audioSource.PlayOneShot(clinkSound);
            Debug.Log("Clink sound played.");
        }
    }
}