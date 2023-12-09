using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace UnityDrift
{
    public class Pauser : MonoBehaviour
    {
        public event UnityAction<bool> EventOnPauseStateChange;

        private bool m_IsPaused;
        public bool IsPaused => m_IsPaused;

        private void Awake()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            UnPause();
        }

        public void Pause()
        {
            if (IsPaused == true) return;

            Time.timeScale = 0;
            AudioListener.pause = true;

            m_IsPaused = true;
            EventOnPauseStateChange?.Invoke(m_IsPaused);
        }

        public void UnPause()
        {
            if (IsPaused == false) return;

            Time.timeScale = 1;
            AudioListener.pause = false;

            m_IsPaused = false;
            EventOnPauseStateChange?.Invoke(m_IsPaused);
        }

        public void ChangePauseState()
        {
            if (m_IsPaused == false)
                Pause();
            else 
                UnPause();
        }
    }
}
