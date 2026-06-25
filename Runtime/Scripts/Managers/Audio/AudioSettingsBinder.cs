using UnityEngine;
using UnityEngine.UI;

namespace TitusGames.Framework
{
    public class AudioSettingsBinder : MonoBehaviour
    {
        [Header("UI References")]
        public Slider musicVolumeSlider;
        public Slider sfxVolumeSlider;
        public Toggle musicToggle;
        public Toggle sfxToggle;

        private IAudioService _audioService;

        private void Start()
        {
            // Resolve the service directly
            _audioService = ServiceLocator.Current.Get<IAudioService>();

            // Initialize UI values
            musicVolumeSlider.value = _audioService.GetMusicVolume();
            sfxVolumeSlider.value = _audioService.GetSFXVolume();

            // Setup toggles (assuming toggle = mute based on your previous logic)
            musicToggle.isOn = !_audioService.IsMusicOn();
            sfxToggle.isOn = !_audioService.IsSFXOn();

            // Add listeners
            musicVolumeSlider.onValueChanged.AddListener(v => _audioService.SetMusicVolume(v));
            sfxVolumeSlider.onValueChanged.AddListener(v => _audioService.SetSFXVolume(v));
            musicToggle.onValueChanged.AddListener(isMuted => _audioService.ToggleMusic(!isMuted));
            sfxToggle.onValueChanged.AddListener(isMuted => _audioService.ToggleSFX(!isMuted));
        }

        private void OnDisable()
        {
            // Always clean up listeners to prevent memory leaks or unexpected behavior
            if (musicVolumeSlider != null) musicVolumeSlider.onValueChanged.RemoveAllListeners();
            if (sfxVolumeSlider != null) sfxVolumeSlider.onValueChanged.RemoveAllListeners();
            if (musicToggle != null) musicToggle.onValueChanged.RemoveAllListeners();
            if (sfxToggle != null) sfxToggle.onValueChanged.RemoveAllListeners();
        }
    }
}