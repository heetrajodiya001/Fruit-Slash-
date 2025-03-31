using UnityEngine;
using UnityEngine.UI;

public class ButtonClickSound : MonoBehaviour
{
    public AudioClip clickSound;
    public AudioClip StartSound;
    public AudioClip EndSound;
    public Slider volumeSlider; // Reference to UI slider

    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

        // Load saved volume or set default to 0.5
        float savedVolume = PlayerPrefs.GetFloat("Volume", 0.5f);
        audioSource.volume = savedVolume;

        if (volumeSlider != null)
        {
            volumeSlider.value = savedVolume;
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }
    }

    public void PlayClickSound()
    {
        PlaySound(clickSound);
    }

    public void ClassicPlayClickSound()
    {
        PlaySound(StartSound);
    }

    public void NewGamePlayClicksound()
    {
        PlaySound(StartSound);
    }

    public void GameExitSound()
    {
        PlaySound(EndSound);
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null && audioSource.enabled && gameObject.activeInHierarchy)
        {
            audioSource.PlayOneShot(clip, audioSource.volume);
        }
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.Save();
    }
}
