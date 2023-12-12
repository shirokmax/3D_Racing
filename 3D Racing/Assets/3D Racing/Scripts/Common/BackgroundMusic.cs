using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace UnityDrift
{
    [RequireComponent(typeof(AudioSource))]
    public class BackgroundMusic : MonoBehaviour, IDependency<RaceStateTracker>
    {
        [SerializeField] private AudioClip m_MainMenu;
        [SerializeField] private AudioClip[] m_RaceClips;

        public event UnityAction<string> EventOnMusicStartPlaying;
        public event UnityAction EventOnChangeSong;

        private AudioSource m_Audio;

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
            AutoChangeSong();

            if (m_CanChangeSong)
                ChangeSong();
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            if (scene.name == GlobalConsts.MAIN_MENU_SCENE_NAME)
                PlayMainMenuMusic();
        }

        private void OnRacePreparationStarted()
        {
            m_Audio.Stop();
            m_CanChangeSong = false;
        }

        private void AutoChangeSong()
        {
            if (m_Audio.clip == null) return;

            if (m_Audio.time == m_Audio.clip.length)
            {
                if (SceneManager.GetActiveScene().name == GlobalConsts.MAIN_MENU_SCENE_NAME)
                    PlayMainMenuMusic();
                else
                    PlayRandomRaceMusic();
            }
        }

        private void ChangeSong()
        {
            if (SceneManager.GetActiveScene().name != GlobalConsts.MAIN_MENU_SCENE_NAME)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    PlayRandomRaceMusic();
                    EventOnChangeSong?.Invoke();
                }
            }
        }

        private void PlayMainMenuMusic()
        {
            if (m_MainMenu == null) return;

            m_Audio.clip = m_MainMenu;
            m_Audio.Play();

            EventOnMusicStartPlaying?.Invoke(m_Audio.clip.name);
        }

        private void PlayRandomRaceMusic()
        {
            if (m_RandomClips.Count == 0) return;

            m_CanChangeSong = true;

            if (m_RandomClips.Count == 0)
                InitializeRandomClipsList();

            int clipRandomIndex = Random.Range(0, m_RandomClips.Count);

            m_Audio.clip = m_RandomClips[clipRandomIndex];
            m_Audio.Play();

            m_RandomClips.RemoveAt(clipRandomIndex);

            EventOnMusicStartPlaying?.Invoke(m_Audio.clip.name);
        }

        private void InitializeRandomClipsList()
        {
            m_RandomClips = new List<AudioClip>();

            foreach (var clip in m_RaceClips)
                m_RandomClips.Add(clip);
        }
    }
}
