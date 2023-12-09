using UnityEngine;

namespace UnityDrift
{
    public class LoadedRaceSceneInfo : MonoBehaviour
    {
        private RaceInfo m_Info;
        public RaceInfo Info => m_Info;

        public void SetInfo(RaceInfo info)
        {
            m_Info = info;
        }
    }
}
