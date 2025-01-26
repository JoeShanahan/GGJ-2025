using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] audioClips;
    private AudioSource audioSource;

    private static AudioManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void PlayAudioClip1()
    {
        PlayClip(0);
    }

    public void PlayAudioClip2()
    {
        PlayClip(1);
    }

    public void PlayAudioClip3()
    {
        PlayClip(2);
    }

    public void PlayAudioClip4()
    {
        PlayClip(3);
    }

    public void PlayAudioClip5()
    {
        PlayClip(4);
    }

    public void PlayAudioClip6()
    {
        PlayClip(5);
    }

    public void PlayAudioClip7()
    {
        PlayClip(6);
    }

    public void PlayAudioClip8()
    {
        PlayClip(7);
    }

    public void PlayAudioClip9()
    {
        PlayClip(8);
    }

    public void PlayAudioClip10()
    {
        PlayClip(9);
    }

    public void PlayAudioClip11()
    {
        PlayClip(10);
    }

    public void PlayAudioClip12()
    {
        PlayClip(11);
    }

    public void PlayAudioClip13()
    {
        PlayClip(12);
    }

    public void PlayAudioClip14()
    {
        PlayClip(13);
    }

    public void PlayAudioClip15()
    {
        PlayClip(14);
    }

    public void PlayAudioClip16()
    {
        PlayClip(15);
    }

    public void PlayAudioClip17()
    {
        PlayClip(16);
    }

    public void PlayAudioClip18()
    {
        PlayClip(17);
    }

    public void PlayAudioClip19()
    {
        PlayClip(18);
    }

    public void PlayAudioClip20()
    {
        PlayClip(19);
    }

    public void PlayAudioClip21()
    {
        PlayClip(20);
    }

    public void PlayAudioClip22()
    {
        PlayClip(21);
    }

    public void PlayAudioClip23()
    {
        PlayClip(22);
    }

    public void PlayAudioClip24()
    {
        PlayClip(23);
    }

    public void PlayAudioClip25()
    {
        PlayClip(24);
    }

    public void PlayAudioClip26()
    {
        PlayClip(25);
    }

    public void PlayAudioClip27()
    {
        PlayClip(26);
    }

    public void PlayAudioClip28()
    {
        PlayClip(27);
    }

    public void PlayAudioClip29()
    {
        PlayClip(28);
    }

    public void PlayAudioClip30()
    {
        PlayClip(29);
    }

    public void PlayAudioClip31()
    {
        PlayClip(30);
    }

    public void PlayAudioClip32()
    {
        PlayClip(31);
    }

    public void PlayAudioClip33()
    {
        PlayClip(32);
    }

    public void PlayAudioClip34()
    {
        PlayClip(33);
    }

    public void PlayAudioClip35()
    {
        PlayClip(34);
    }

    private void PlayClip(int index)
    {
        if (index >= 0 && index < audioClips.Length && audioClips[index] != null)
        {
            audioSource.PlayOneShot(audioClips[index]);
        }
        else
        {
            Debug.LogWarning($"Audio clip got forgor D:");
        }
    }
}