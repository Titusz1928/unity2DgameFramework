using UnityEngine;

namespace TitusGames.Framework
{
    [AddComponentMenu("TitusGames/Framework/Global Cancel Handler")]
    public class GlobalCancelHandler : MonoBehaviour, ICancelInputHandler
    {
        [Header("Chain Settings")]
        [Tooltip("The system window prefab (e.g., Pause Menu) to spawn when Cancel is pressed and no windows are open.")]
        [SerializeField] private GameObject primarySystemMenuPrefab;

        private GameObject activeMenuInstance;
        private IWindowService _windowService;

        private void Start()
        {
            _windowService = ServiceLocator.Current.Get<IWindowService>();
            _windowService?.SetNextHandler(this);
        }

        public bool HandleCancel()
        {
            // If the system menu isn't open yet, spawn it through the WindowManager stack
            if (activeMenuInstance == null && primarySystemMenuPrefab != null)
            {
                activeMenuInstance = _windowService.OpenWindow(primarySystemMenuPrefab);
                return true; // Input completely handled/consumed
            }

            return false; // Allow remaining systems further down the line to receive fallback events
        }
    }
}