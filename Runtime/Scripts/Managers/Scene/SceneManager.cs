using UnityEngine;
using UnityEngine.SceneManagement;

namespace TitusGames.Framework
{
public class SceneManagerEX : MonoBehaviour, ISceneService
{

    void Awake()
    {
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
}
