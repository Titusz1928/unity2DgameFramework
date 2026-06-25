using UnityEngine;

namespace TitusGames.Framework
{
    public class UI_OpenWindow : MonoBehaviour
    {
        public GameObject windowPrefab;
        private IWindowService _windowService;

        private void Start()
        {
            _windowService = ServiceLocator.Current.Get<IWindowService>();
        }

        public void Open()
        {
            _windowService?.OpenWindow(windowPrefab);
        }
    }
}