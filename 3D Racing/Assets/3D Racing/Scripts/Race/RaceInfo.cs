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

        [SerializeField] private RaceType m_RaceType;
        public RaceType RaceType => m_RaceType;

        [SerializeField] private Sprite m_PreviewSprite;
        public Sprite PreviewSprite => m_PreviewSprite;

        [SerializeField] private bool m_Night;
        public bool Night => m_Night;

        [SerializeField] private bool m_AlwaysUnlocked;
        public bool alwaysUnlocked => m_AlwaysUnlocked;
    }
}
