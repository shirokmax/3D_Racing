using UnityEngine;
using UnityEngine.Events;

namespace CarRacing
{
    public class Pauser : MonoBehaviour
    {
        public event UnityAction<bool> EventOnPauseStateChange;

        private bool m_IsPaused;
        public bool IsPaused => m_IsPaused;

        public void Pause()
        {
            if (IsPaused == true) return;

            Time.timeScale = 0;
            m_IsPaused = true;
            EventOnPauseStateChange?.Invoke(m_IsPaused);
        }

        public void UnPause()
        {
            if (IsPaused == false) return;

            Time.timeScale = 1;
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
