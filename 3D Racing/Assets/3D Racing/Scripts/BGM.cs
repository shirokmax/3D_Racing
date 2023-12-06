using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

namespace CarRacing
{
    [RequireComponent(typeof(AudioSource))]
    public class BGM : MonoBehaviour, IDependency<RaceStateTracker>
    {
        [SerializeField] private AudioClip m_MainMenu;
        [SerializeField] private AudioClip[] m_RaceClips;

        private AudioSource m_Audio;

        private string m_LastLoadedSceneName = string.Empty;
        private bool m_CanChangeSong;

        private RaceStateTracker m_RaceStateTracker;
        public void Construct(RaceStateTracker obj)
        {
            m_RaceStateTracker = obj;

            m_RaceStateTracker.EventOnPreparationStarted.AddListener(OnRacePreparationStarted);
            m_RaceStateTracker.EventOnCountdownStarted.AddListener(PlayRandomRaceMusic);
        }

        private List<AudioClip> m_RandomClips;

        private void Awake()
        {
            m_Audio = GetComponent<AudioSource>();

            InitializeRandomClipsList();

            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void Update()
        {
            if (m_CanChangeSong)
                ChangeSong();
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

                if (scene.name == GlobalConsts.MAIN_MENU_SCENE_NAME)
                    PlayMainMenuMusic();
            }
        }

        private void OnRacePreparationStarted()
        {
            m_Audio.Stop();
            m_CanChangeSong = false;
        }

        private void ChangeSong()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (SceneManager.GetActiveScene().name != GlobalConsts.MAIN_MENU_SCENE_NAME)
                    PlayRandomRaceMusic();
            }
        }

        private void PlayMainMenuMusic()
        {
            m_Audio.clip = m_MainMenu;
            m_Audio.Play();
        }

        private void PlayRandomRaceMusic()
        {
            if (!m_Audio.isPlaying && m_Audio.time != 0)
                return;

            m_CanChangeSong = true;

            if (m_RandomClips.Count == 0)
                InitializeRandomClipsList();

            int clipRandomIndex = Random.Range(0, m_RandomClips.Count);

            m_Audio.clip = m_RandomClips[clipRandomIndex];
            m_Audio.Play();

            m_RandomClips.RemoveAt(clipRandomIndex);
        }

        private void InitializeRandomClipsList()
        {
            m_RandomClips = new List<AudioClip>();

            foreach (var clip in m_RaceClips)
                m_RandomClips.Add(clip);
        }
    }
}
