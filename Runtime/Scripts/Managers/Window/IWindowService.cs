using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TitusGames.Framework
{
    public interface IWindowService
    {
        int WindowCount { get; }
        bool IsAnyWindowOpen { get; }

        event Action OnWindowClosed;

        void SetNextHandler(ICancelInputHandler next);
        void RegisterPlayerInput(PlayerInput input);
        bool HandleCancel();

        GameObject OpenWindow(GameObject windowPrefab);
        void CloseTopWindow();
        GameObject GetTopWindow();
        void CloseAllWindows();
        void RegisterUIRoot(Transform root);
    }
}