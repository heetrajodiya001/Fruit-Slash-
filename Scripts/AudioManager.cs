using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Audio Clips")]
    public AudioClip[] musicClips;
    public AudioClip[] sfxClips;

    private Dictionary<string, AudioClip> musicDict = new();
    private Dictionary<string, AudioClip> sfxDict = new();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitDictionaries();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void InitDictionaries()
    {
        foreach (var clip in musicClips)
        {
            if (clip != null && !musicDict.ContainsKey(clip.name))
                musicDict.Add(clip.name, clip);
        }
        foreach (var clip in sfxClips)
        {
            if (clip != null && !sfxDict.ContainsKey(clip.name))
                sfxDict.Add(clip.name, clip);
        }
    }

    // --- Music ---
    public void PlayMusic(string name, bool loop = true)
    {
        if (musicDict.ContainsKey(name))
        {
            musicSource.clip = musicDict[name];
            musicSource.loop = loop;
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning("Music clip not found: " + name);
        }
    }

    public void StopMusic() => musicSource.Stop();

    // --- SFX ---
    public void PlaySFX(string name)
    {
        if (sfxDict.ContainsKey(name))
        {
            sfxSource.PlayOneShot(sfxDict[name]);
        }
        else
        {
            Debug.LogWarning("SFX clip not found: " + name);
        }
    }

    /*
     * AudioManager.Instance.PlayMusic("MainTheme");
    AudioManager.Instance.PlaySFX("Hit");
    AudioManager.Instance.SetMusicVolume(0.5f);
    */
    // --- Volume Control ---
    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
        PlayerPrefs.SetFloat("sfxVolume", volume);
    }

    void Start()
    {
        musicSource.volume = PlayerPrefs.GetFloat("musicVolume", 1f);
        sfxSource.volume = PlayerPrefs.GetFloat("sfxVolume", 1f);
    }
    /* PlayerController.cs → Jump sound:
    AudioManager.Instance.PlaySFX("Jump");*/

    /* GameOver.cs → Background music stop:
    AudioManager.Instance.StopMusic();
     */
    /*
     UIManager.cs → Button click:
    AudioManager.Instance.PlaySFX("Click");
    */
    /*SettingsMenu.cs → Volume slider:
     AudioManager.Instance.SetMusicVolume(musicSlider.value);
*/
}
