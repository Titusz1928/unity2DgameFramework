using UnityEngine;
using UnityEngine.InputSystem;

namespace TitusGames.Framework
{
    [RequireComponent(typeof(PlayerInput))]
    [AddComponentMenu("TitusGames/Framework/Input Linker")]
    public class InputLinker : MonoBehaviour
    {
        private PlayerInput playerInput;
        private IWindowService _windowService;

        private void Awake()
        {
            playerInput = GetComponent<PlayerInput>();
        }

        private void Start()
        {
            _windowService = ServiceLocator.Current.Get<IWindowService>();
            _windowService?.RegisterPlayerInput(playerInput);
        }

        private void OnDestroy()
        {
            _windowService?.RegisterPlayerInput(null);
        }
    }
}