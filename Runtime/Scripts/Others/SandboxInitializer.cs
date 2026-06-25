using UnityEngine;

namespace TitusGames.Framework
{
    public class SandboxInitializer : MonoBehaviour
    {
        private void Awake()
        {
            InitializeGlobalManagers();
        }

        private void InitializeGlobalManagers()
        {
            // Initialize infrastructure registry container
            ServiceLocator.Initialize();

            // 1. Scene Service
            GetOrRegisterService<ISceneService>("SceneManagerEX", () =>
            {
                var sceneObj = new GameObject("SceneManagerEX");
                var service = sceneObj.AddComponent<SceneManagerEX>();
                DontDestroyOnLoad(sceneObj);
                return service;
            });

            // 2. Window Service
            GetOrRegisterService<IWindowService>("WindowManager", () =>
            {
                var windowObj = new GameObject("WindowManager");
                var service = windowObj.AddComponent<WindowManager>();
                DontDestroyOnLoad(windowObj);
                return service;
            });

            // 3. Localization Service
            var localizationService = GetOrRegisterService<ILocalizationService>("LocalizationManager", () =>
            {
                var locObj = new GameObject("LocalizationManager");
                var service = locObj.AddComponent<LocalizationManager>();
                DontDestroyOnLoad(locObj);
                return service;
            });
            localizationService.Initialize();

            // 4. Audio Service
            GetOrRegisterService<IAudioService>("AudioManager", () =>
            {
                var audioObj = new GameObject("AudioManager");
                var service = audioObj.AddComponent<AudioManager>();
                DontDestroyOnLoad(audioObj);
                return service;
            });

            // 5. Message Service
            GetOrRegisterService<IMessageService>("MessageManager", () =>
            {
                var msgObj = new GameObject("MessageManager");
                var service = msgObj.AddComponent<MessageManager>();
                DontDestroyOnLoad(msgObj);

                var container = GameObject.Find("MessageContainer");
                if (container != null)
                    service.RegisterContainer(container.GetComponent<RectTransform>());

                return service;
            });

            Debug.Log("<color=cyan>[Sandbox] Global Infrastructure & Services Initialized Cleanly via ServiceLocator.</color>");
        }

        /// <summary>
        /// Attempts to fetch a service from the service locator registry container. 
        /// If missing, triggers a factory callback routine to build and register it safely.
        /// </summary>
        private T GetOrRegisterService<T>(string name, System.Func<T> factory) where T : class
        {
            try
            {
                return ServiceLocator.Current.Get<T>();
            }
            catch
            {
                T service = factory();
                ServiceLocator.Current.Register<T>(service);
                return service;
            }
        }
    }
}