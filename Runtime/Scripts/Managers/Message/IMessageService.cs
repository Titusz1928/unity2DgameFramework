using UnityEngine;

namespace TitusGames.Framework
{
    public interface IMessageService
    {
        void ShowMessage(string key, string iconName = "", string customPrefabName = "");
        void ShowMessageDirectly(string message, string iconName = "", string customPrefabName = "");
        void ShowMessageWithSprite(string text, Sprite explicitIcon, string customPrefabName = "", bool useLocalization = true);
        void RegisterContainer(RectTransform container);
    }
}