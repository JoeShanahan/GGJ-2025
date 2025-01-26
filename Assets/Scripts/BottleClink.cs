using UnityEngine;

public class BottleClink : MonoBehaviour
{
    [Header("Audio Settings")]
    public AudioClip clinkSound;
    public float collisionCooldown = 0.5f;

    private AudioSource audioSource;
    private float lastCollisionTime = -Mathf.Infinity;

    private void Start()
    {
        audioSource = Camera.main.GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("AudioSource component is missing from the main camera.");
            return;
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
