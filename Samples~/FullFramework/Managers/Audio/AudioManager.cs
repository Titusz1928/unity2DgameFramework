using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Settings")]
    [SerializeField] private string musicFolderPath = "Audio/Music";
    [SerializeField] private string sfxFolderPath = "Audio/SFX";

    private AudioSource musicSource;
    private AudioSource sfxSource;

    // Dictionaries to hold our clips for fast access
    private Dictionary<string, AudioClip> musicLibrary = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioClip> sfxLibrary = new Dictionary<string, AudioClip>();

    private float musicVolume = 1f;
    private float sfxVolume = 1f;
    private bool isMusicOn = true;
    private bool isSFXOn = true;

    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.loop = true;

        sfxSource = gameObject.AddComponent<AudioSource>();

        LoadSettings();
        InitializeLibrary();
    }

    private void InitializeLibrary()
    {
        // Automatically load ALL clips in the specified Resource folders
        AudioClip[] musicClips = Resources.LoadAll<AudioClip>(musicFolderPath);
        foreach (var clip in musicClips) musicLibrary[clip.name] = clip;

        AudioClip[] sfxClips = Resources.LoadAll<AudioClip>(sfxFolderPath);
        foreach (var clip in sfxClips) sfxLibrary[clip.name] = clip;
        
        Debug.Log($"AudioLibrary Loaded: {musicLibrary.Count} Music, {sfxLibrary.Count} SFX clips.");
    }

    // --- GENERIC PUBLIC METHODS ---

    public void PlaySFX(string clipName, float volumeMultiplier = 1f)
    {
        if (!isSFXOn) return;

        if (sfxLibrary.TryGetValue(clipName, out AudioClip clip))
        {
            sfxSource.PlayOneShot(clip, sfxVolume * volumeMultiplier);
        }
        else
        {
            Debug.LogWarning($"SFX '{clipName}' not found in {sfxFolderPath}");
        }
    }

    public void PlayMusic(string clipName, bool fade = true)
    {
        if (musicLibrary.TryGetValue(clipName, out AudioClip clip))
        {
            if (musicSource.clip == clip) return; // Already playing

            if (fade) StartCoroutine(FadeToNewTrack(clip));
            else SwitchMusicInstant(clip);
        }
    }

    private void SwitchMusicInstant(AudioClip clip)
    {
        musicSource.Stop();
        musicSource.clip = clip;
        musicSource.mute = !isMusicOn;
        musicSource.Play();
    }

    private System.Collections.IEnumerator FadeToNewTrack(AudioClip newClip)
    {
        float fadeDuration = 0.8f;
        if (musicSource.isPlaying)
        {
            for (float t = 0; t < fadeDuration; t += Time.deltaTime)
            {
                musicSource.volume = Mathf.Lerp(musicVolume, 0f, t / fadeDuration);
                yield return null;
            }
        }

        SwitchMusicInstant(newClip);

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            musicSource.volume = Mathf.Lerp(0f, musicVolume, t / fadeDuration);
            yield return null;
        }
        musicSource.volume = musicVolume;
    }

    // --- Volume & Toggle Management ---
    public void SetMusicVolume(float volume)
    {
        musicVolume = Mathf.Clamp01(volume);
        musicSource.volume = musicVolume;
        SaveSettings();
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
        SaveSettings();
    }

    public void ToggleMusic(bool isOn)
    {
        isMusicOn = isOn;
        musicSource.mute = !isOn;
        SaveSettings();
    }

    public void ToggleSFX(bool isOn)
    {
        isSFXOn = isOn;
        SaveSettings();
    }

    // --- Getters for UI Sync ---
    public float GetMusicVolume() => musicVolume;
    public float GetSFXVolume() => sfxVolume;
    public bool IsMusicOn() => isMusicOn;
    public bool IsSFXOn() => isSFXOn;

    private void LoadSettings()
    {
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);
        isMusicOn = PlayerPrefs.GetInt("IsMusicOn", 1) == 1;
        isSFXOn = PlayerPrefs.GetInt("IsSFXOn", 1) == 1;

        musicSource.volume = musicVolume;
        musicSource.mute = !isMusicOn;
    }

    private void SaveSettings()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
        PlayerPrefs.SetInt("IsMusicOn", isMusicOn ? 1 : 0);
        PlayerPrefs.SetInt("IsSFXOn", isSFXOn ? 1 : 0);
        PlayerPrefs.Save();
    }

}
