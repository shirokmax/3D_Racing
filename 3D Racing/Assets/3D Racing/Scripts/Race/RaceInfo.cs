using UnityEngine;

namespace UnityDrift
{
    [CreateAssetMenu]
    public class RaceInfo : ScriptableObject
    {
        [SerializeField] private string m_SceneName;
        public string SceneName => m_SceneName;

        [SerializeField] private Sprite m_PreviewSprite;
        public Sprite PreviewSprite => m_PreviewSprite;

        [SerializeField] private string m_Title;
        public string Title => m_Title;

        [SerializeField] private RaceType m_RaceType;
        public RaceType RaceType => m_RaceType;

        [SerializeField] private bool m_Night;
        public bool Night => m_Night;
    }
}
