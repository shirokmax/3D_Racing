using UnityEngine;

namespace UnityDrift
{
    public class LoadedRaceInfo : MonoBehaviour
    {
        [SerializeField] private RaceInfo m_DefaultInfo;

        private RaceInfo m_Info;
        public RaceInfo Info => m_Info;

        private void Awake()
        {
            m_Info = m_DefaultInfo;
        }

        public void SetInfo(RaceInfo info)
        {
            m_Info = info;
        }
    }
}
