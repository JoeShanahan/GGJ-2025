using UnityEngine;

public class AudioManager : MonoBehaviour {
    public AudioClip[] audioClips;
    private AudioSource audioSource;

    private static AudioManager instance;

    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void PlayAudioClip1() {
        PlayClip(0);
    }

    public void PlayAudioClip2() {
        PlayClip(1);
    }

    public void PlayAudioClip3() {
        PlayClip(2);
    }

    public void PlayAudioClip4() {
        PlayClip(3);
    }

    public void PlayAudioClip5() {
        PlayClip(4);
    }

    public void PlayAudioClip6() {
        PlayClip(5);
    }

    public void PlayAudioClip7() {
        PlayClip(6);
    }

    public void PlayAudioClip8() {
        PlayClip(7);
    }


    private void PlayClip(int index) {
        if (index >= 0 && index < audioClips.Length && audioClips[index] != null) {
            audioSource.PlayOneShot(audioClips[index]);
        } else {
            Debug.LogWarning($"Audio clip got forgor D:");
        }
    }
}