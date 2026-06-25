using System;
using System.Collections.Generic;

namespace TitusGames.Framework
{
    public interface ILocalizationService
    {
        List<string> LanguageCodes { get; }
        List<string> LanguageDisplayNames { get; }
        string CurrentLanguageCode { get; }

        event Action OnLanguageChanged;

        void Initialize();
        void LoadLanguage(string languageCode);
        void SetLanguageIndex(int index);
        string GetLocalizedValue(string key);
    }
}