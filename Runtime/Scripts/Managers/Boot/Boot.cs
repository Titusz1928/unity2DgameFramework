using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace TitusGames.Framework
{
    public class Boot : MonoBehaviour
    {
        [Header("UI References")]
        public CanvasGroup bootCanvas; // assign your boot canvas in inspector

        [Header("Fade Settings")]
        public float fadeDuration = 1f;

        private IEnumerator Start()
        {
            // Make sure canvas is visible at start
            bootCanvas.alpha = 0f;
            bootCanvas.gameObject.SetActive(true);

            // Fade in
            yield return StartCoroutine(FadeCanvas(0f, 1f, fadeDuration));

            // === Initialize the Service Locator infrastructure ===
            ServiceLocator.Initialize();

            // 1. Initialize Window Service
            GetOrRegisterService<IWindowService>("WindowManager", () =>
            {
                var windowObj = new GameObject("WindowManager");
                var service = windowObj.AddComponent<WindowManager>();
                DontDestroyOnLoad(windowObj);
                return service;
            });

            // 2. Initialize Localization Service (with initialization callback execution)
            var localizationService = GetOrRegisterService<ILocalizationService>("LocalizationManager", () =>
            {
                var locObj = new GameObject("LocalizationManager");
                var service = locObj.AddComponent<LocalizationManager>();
                DontDestroyOnLoad(locObj);
                return service;
            });
            localizationService.Initialize();

            // 3. Initialize Message Service (Auto-locating fallback layout container components)
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

            // 4. Initialize Scene Service
            GetOrRegisterService<ISceneService>("SceneManagerEX", () =>
            {
                var sceneObj = new GameObject("SceneManagerEX");
                var service = sceneObj.AddComponent<SceneManagerEX>();
                DontDestroyOnLoad(sceneObj);
                return service;
            });

            // 5. Initialize Audio Service
            GetOrRegisterService<IAudioService>("AudioManager", () =>
            {
                var audioObj = new GameObject("AudioManager");
                var service = audioObj.AddComponent<AudioManager>();
                DontDestroyOnLoad(audioObj);
                return service;
            });

            // Optional small delay while visible
            yield return new WaitForSeconds(0.5f);

            // Fade out
            yield return StartCoroutine(FadeCanvas(1f, 0f, fadeDuration));

            // Fetch the scene service from the locator to load the main menu
            ServiceLocator.Current.Get<ISceneService>().LoadScene("MainMenu");
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

        private IEnumerator FadeCanvas(float start, float end, float duration)
        {
            float elapsed = 0f;
            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / duration);
                bootCanvas.alpha = Mathf.Lerp(start, end, t);
                yield return null;
            }
            bootCanvas.alpha = end;
        }
    }
}