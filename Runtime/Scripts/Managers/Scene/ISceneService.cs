using UnityEngine;

namespace TitusGames.Framework
{
    public interface ISceneService
    {
        void LoadScene(string sceneName);
        void QuitGame();
    }
}
