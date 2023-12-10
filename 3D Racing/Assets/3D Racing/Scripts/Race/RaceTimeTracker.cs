using UnityEngine;

namespace UnityDrift
{
    public class RaceTimeTracker : MonoBehaviour, IDependency<RaceStateTracker>, IDependency<LoadedRaceSceneInfo>
    {
        private RaceStateTracker m_RaceStateTracker;
        public void Construct(RaceStateTracker obj) => m_RaceStateTracker = obj;

        private float m_CurrentTime;
        public float CurrentTime => m_CurrentTime;

        private LoadedRaceSceneInfo m_LoadedRaceSceneInfo;
        public void Construct(LoadedRaceSceneInfo obj) => m_LoadedRaceSceneInfo = obj;

        private void Start()
        {
            enabled = false;

            if (m_LoadedRaceSceneInfo.Info.RaceType != RaceType.Race)
                return;

            m_RaceStateTracker.EventOnRaceStarted.AddListener(OnRaceStarted);
            m_RaceStateTracker.EventOnRaceFinished.AddListener(OnRaceFinished);
        }

        private void Update()
        {
            m_CurrentTime += Time.deltaTime;
        }

        private void OnRaceStarted()
        {
            enabled = true;
            m_CurrentTime = 0;
        }

        private void OnRaceFinished()
        {
            enabled = false;
        }
    }
}
