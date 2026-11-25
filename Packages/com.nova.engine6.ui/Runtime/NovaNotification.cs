using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Nova.Engine6.UI
{
    [DisallowMultipleComponent]
    public sealed class NovaNotification : MonoBehaviour
    {
        [SerializeField] private GameObject panel;
        [SerializeField] private Text messageText;
        [SerializeField, Min(0f)] private float defaultDuration = 2.5f;

        private Coroutine hideRoutine;

        private void Awake()
        {
            if (panel != null) panel.SetActive(false);
        }

        public void Show(string message)
        {
            ShowFor(message, defaultDuration);
        }

        public void ShowFor(string message, float duration)
        {
            if (messageText != null) messageText.text = message ?? string.Empty;
            if (panel != null) panel.SetActive(true);

            if (hideRoutine != null) StopCoroutine(hideRoutine);
            if (duration > 0f) hideRoutine = StartCoroutine(HideAfter(duration));
        }

        public void Hide()
        {
            if (hideRoutine != null) StopCoroutine(hideRoutine);
            hideRoutine = null;
            if (panel != null) panel.SetActive(false);
        }

        private IEnumerator HideAfter(float seconds)
        {
            yield return new WaitForSecondsRealtime(seconds);
            if (panel != null) panel.SetActive(false);
            hideRoutine = null;
        }
    }
}
