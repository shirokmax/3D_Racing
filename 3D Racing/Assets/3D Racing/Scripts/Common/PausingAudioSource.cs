using UnityEngine;

namespace UnityDrift
{
    [RequireComponent(typeof(AudioSource))]
    public class PausingAudioSource : MonoBehaviour, IDependency<Pauser>
    {
        private AudioSource m_AudioSource;

        private Pauser m_Pauser;
        public void Construct(Pauser obj) => m_Pauser = obj;

        private void Start()
        {
            m_AudioSource = GetComponent<AudioSource>();

            m_Pauser.EventOnPauseStateChange += OnPauseStateChange;
        }

        private void OnDestroy()
        {
            m_Pauser.EventOnPauseStateChange -= OnPauseStateChange;
        }

        private void OnPauseStateChange(bool isPaused)
        {
            if (isPaused == true)
                m_AudioSource.Pause();
            else
                m_AudioSource.UnPause();
        }
    }
}
