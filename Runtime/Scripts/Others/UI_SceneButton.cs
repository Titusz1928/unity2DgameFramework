using UnityEngine;

namespace TitusGames.Framework
{
    public class UI_SceneButton : MonoBehaviour
    {
        public string sceneName;

        private ISceneService _sceneService;

        private void Start()
        {
            // Locate the scene service safely during scene initialization
            _sceneService = ServiceLocator.Current.Get<ISceneService>();
        }

        public void LoadScene()
        {
            if (_sceneService != null && !string.IsNullOrEmpty(sceneName))
            {
                _sceneService.LoadScene(sceneName);
            }
        }
    }
}
