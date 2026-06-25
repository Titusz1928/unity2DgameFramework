using UnityEngine;

namespace TitusGames.Framework
{
    public class UIRootProvider : MonoBehaviour
    {
        public Transform windowRoot;

        private void Start()
        {
            var windowService = ServiceLocator.Current.Get<IWindowService>();

            if (windowService != null)
            {
                windowService.RegisterUIRoot(windowRoot);
            }
        }
    }
}
