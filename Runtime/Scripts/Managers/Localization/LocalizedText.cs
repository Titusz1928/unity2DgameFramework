using TMPro;
using UnityEngine;

namespace TitusGames.Framework
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class LocalizedText : MonoBehaviour
    {
        public string key;

        private TextMeshProUGUI textComponent;
        private ILocalizationService _localizationService;

        private void Awake()
        {
            textComponent = GetComponent<TextMeshProUGUI>();
        }

        private void Start()
        {
            // Initial translation grab when first booting or spawning
            if (_localizationService == null)
            {
                _localizationService = ServiceLocator.Current.Get<ILocalizationService>();
            }
            UpdateText();
        }

        private void OnEnable()
        {
            if (_localizationService == null)
            {
                _localizationService = ServiceLocator.Current.Get<ILocalizationService>();
            }

            if (_localizationService != null)
            {
                UpdateText();
                _localizationService.OnLanguageChanged += UpdateText; // Subscribe to instance event
            }
        }

        private void OnDisable()
        {
            if (_localizationService != null)
            {
                _localizationService.OnLanguageChanged -= UpdateText; // Unsubscribe to prevent memory leaks
            }
        }

        private void UpdateText()
        {
            if (textComponent != null && _localizationService != null)
            {
                textComponent.text = _localizationService.GetLocalizedValue(key);
            }
        }

        public void SetKey(string newKey)
        {
            key = newKey;
            UpdateText();
        }
    }
}