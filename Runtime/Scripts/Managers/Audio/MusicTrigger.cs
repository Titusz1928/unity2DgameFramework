using UnityEngine;

namespace TitusGames.Framework
{
    public class MusicTrigger : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private string trackName;
        [SerializeField] private bool fade = true;

        [Header("Scene Transition")]
        [Tooltip("If true, the music will stop when this scene is unloaded.")]
        [SerializeField] private bool stopOnSceneExit = false;

        private IAudioService _audioService;

        void Start()
        {
            // Resolve the service from the locator
            _audioService = ServiceLocator.Current.Get<IAudioService>();

            if (_audioService != null && !string.IsNullOrEmpty(trackName))
            {
                _audioService.PlayMusic(trackName, fade);
            }
        }

        private void OnDestroy()
        {
            // Use the local reference to clean up if needed
            if (stopOnSceneExit && _audioService != null)
            {
                _audioService.StopMusic(fade);
            }
        }
    }
}