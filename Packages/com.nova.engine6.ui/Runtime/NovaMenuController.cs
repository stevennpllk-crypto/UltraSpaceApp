using UnityEngine;
using UnityEngine.SceneManagement;

namespace Nova.Engine6.UI
{
    public sealed class NovaMenuController : MonoBehaviour
    {
        [SerializeField] private GameObject menuRoot;
        [SerializeField] private bool pauseWhenOpen = true;

        private bool isOpen;

        private void Start()
        {
            SetOpen(false);
        }

        public void Toggle()
        {
            SetOpen(!isOpen);
        }

        public void Open()
        {
            SetOpen(true);
        }

        public void Close()
        {
            SetOpen(false);
        }

        private void SetOpen(bool open)
        {
            isOpen = open;
            if (menuRoot != null) menuRoot.SetActive(open);
            if (pauseWhenOpen) Time.timeScale = open ? 0f : 1f;
        }

        public void LoadScene(string sceneName)
        {
            Time.timeScale = 1f;
            if (!string.IsNullOrEmpty(sceneName))
                SceneManager.LoadScene(sceneName);
        }

        public void QuitGame()
        {
            Time.timeScale = 1f;
            Application.Quit();
        }
    }
}
