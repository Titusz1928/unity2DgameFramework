using UnityEngine;

namespace TitusGames.Framework
{
    public interface IAudioService
    {
        void PlaySFX(string clipName, float volumeMultiplier = 1f);
        void PlayRandomizedSFX(string clipName, float pitchRange = 0.1f, float volumeRange = 0.1f);
        void PlayRandomSFXFromList(string[] clipNames, float pitchRange = 0.1f, float volumeRange = 0.1f);
        void PlayMusic(string clipName, bool fade = true);
        void ResumePreviousMusic(string previousTrack, bool fade = true);
        string GetCurrentTrackName();
        void StopMusic(bool fade = true);
        void SetMusicVolume(float volume);
        void SetSFXVolume(float volume);
        void ToggleMusic(bool isOn);
        void ToggleSFX(bool isOn);
        float GetMusicVolume();
        float GetSFXVolume();
        bool IsMusicOn();
        bool IsSFXOn();
        AudioClip GetClip(string clipName);
    }
}
