using UnityEngine;
using UnityEngine.SceneManagement;

namespace CarRacing
{
    [RequireComponent(typeof(AudioSource))]
    public class BGM : MonoBehaviour, IDependency<RaceStateTracker>
    {
        [SerializeField] private AudioClip m_MainMenu;
        [SerializeField] private AudioClip[] m_Race;

        private AudioSource m_Audio;

        private string m_LastLoadedSceneName = string.Empty;

        private RaceStateTracker m_RaceStateTracker;
        public void Construct(RaceStateTracker obj)
        {
            m_RaceStateTracker = obj;

            m_RaceStateTracker.EventOnPreparationStarted.AddListener(OnRacePreparationStarted);
            m_RaceStateTracker.EventOnCountdownStarted.AddListener(PlayRandomRaceMusic);
        }

        private void Awake()
        {
            m_Audio = GetComponent<AudioSource>();

            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            if (scene.name != m_LastLoadedSceneName)
            {
                m_LastLoadedSceneName = scene.name;

                if (scene.name == "MainMenu")
                    PlayMainMenuMusic();
            }
        }

        private void OnRacePreparationStarted()
        {
            m_Audio.Stop();
        }

        private void PlayMainMenuMusic()
        {
            m_Audio.clip = m_MainMenu;
            m_Audio.Play();
        }

        private void PlayRandomRaceMusic()
        {
            m_Audio.clip = m_Race[Random.Range(0, m_Race.Length)];
            m_Audio.Play();
        }
    }
}
