using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Nova.Engine
{
    public sealed class NovaSceneLoader : MonoBehaviour
    {
        public void Load(string sceneName, LoadSceneMode mode = LoadSceneMode.Single,
            Action<float> onProgress = null, Action onCompleted = null)
        {
            if (string.IsNullOrWhiteSpace(sceneName))
            {
                Debug.LogError("[NOVA] Scene name is empty.");
                return;
            }

            StartCoroutine(LoadRoutine(sceneName, mode, onProgress, onCompleted));
        }

        private IEnumerator LoadRoutine(string sceneName, LoadSceneMode mode,
            Action<float> onProgress, Action onCompleted)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, mode);

            if (operation == null)
            {
                Debug.LogError($"[NOVA] Could not start loading scene: {sceneName}");
                yield break;
            }

            while (!operation.isDone)
            {
                onProgress?.Invoke(Mathf.Clamp01(operation.progress / 0.9f));
                yield return null;
            }

            onProgress?.Invoke(1f);
            onCompleted?.Invoke();
        }
    }
}
