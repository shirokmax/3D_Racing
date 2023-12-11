using UnityEngine;

namespace UnityDrift
{
    [CreateAssetMenu]
    public class RaceInfo : ScriptableObject
    {
        [SerializeField] private string m_SceneName;
        public string SceneName => m_SceneName;

        [SerializeField] private string m_SaveName;
        public string SaveName => m_SaveName;

        [SerializeField] private string m_Title;
        public string Title => m_Title;

        [Space]
        [SerializeField] private RaceType m_RaceType;
        public RaceType RaceType => m_RaceType;

        [SerializeField] private TrackType m_TrackType;
        public TrackType TrackType => m_TrackType;

        [SerializeField] private int m_LapsToComplete;
        public int LapsToComplete => m_LapsToComplete;

        [SerializeField] private float m_GoldTime;
        public float GoldTime => m_GoldTime;

        [SerializeField] private float m_GoldDrift;
        public float GoldDrift => m_GoldDrift;

        [Space]
        [SerializeField] private Sprite m_PreviewSprite;
        public Sprite PreviewSprite => m_PreviewSprite;

        [SerializeField] private bool m_Night;
        public bool Night => m_Night;

        [SerializeField] private bool m_AlwaysUnlocked;
        public bool alwaysUnlocked => m_AlwaysUnlocked;
    }
}
