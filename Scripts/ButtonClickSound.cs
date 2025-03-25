using UnityEngine;
using UnityEngine.UI;

public class ButtonClickSound : MonoBehaviour
{
    public AudioClip clickSound;
    public AudioClip startSound;
    public AudioClip endSound;
    public AudioClip explosionSound; // Classic Mode bomb sound
    public AudioClip sliceSound; // New Game Mode bomb sound
    public AudioClip fruitSliceSound; // Fruit slicing sound
    public Slider volumeSlider; // UI Slider to control volume

    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.volume = PlayerPrefs.GetFloat("Volume", 0.5f); // Load saved volume or default 0.5

        if (volumeSlider != null)
        {
            volumeSlider.value = audioSource.volume;
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }
    }

    public void PlayClickSound()
    {
        PlaySound(clickSound);
    }

    public void ClassicPlayClickSound()
    {
        PlaySound(startSound);
    }

    public void NewGamePlayClickSound()
    {
        PlaySound(startSound);
    }

    public void GameExitSound()
    {
        PlaySound(endSound);
    }

    public void PlayExplosionSound()
    {
        PlaySound(explosionSound);
    }

    public void PlaySliceSound()
    {
        PlaySound(sliceSound);
    }

    public void PlayFruitSliceSound()
    {
        PlaySound(fruitSliceSound);
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null && audioSource.enabled && gameObject.activeInHierarchy)
        {
            audioSource.volume = PlayerPrefs.GetFloat("Volume", 0.5f); // Ensure volume is updated
            audioSource.PlayOneShot(clip);
        }
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
        PlayerPrefs.SetFloat("Volume", volume); // Save volume setting
        PlayerPrefs.Save();
    }
}
