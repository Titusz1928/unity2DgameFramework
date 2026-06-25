using Unity.VectorGraphics;
using UnityEngine;

namespace TitusGames.Framework
{

    public class UI_ExitButton : MonoBehaviour
    {

        private ISceneService _sceneService;

        private void Start()
        {
            // Locate the scene service safely during scene initialization
            _sceneService = ServiceLocator.Current.Get<ISceneService>();
        }

        public void QuitGame()
        {
            if (_sceneService != null)
            {
                _sceneService.QuitGame();
            }
        }
    }
}
