using UnityEngine;

namespace TitusGames.Framework
{
    public class UI_CloseWindow : MonoBehaviour
    {
        private IWindowService _windowService;

        private void Start()
        {
            _windowService = ServiceLocator.Current.Get<IWindowService>();
        }

        public void Close()
        {
            _windowService?.CloseTopWindow();
        }
    }
}