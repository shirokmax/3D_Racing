using UnityEngine;

namespace UnityDrift
{
    public class RaceTimeTracker : MonoBehaviour, IDependency<RaceStateTracker>
    {
        private RaceStateTracker m_RaceStateTracker;
        public void Construct(RaceStateTracker obj) => m_RaceStateTracker = obj;

        private float m_CurrentTime;
        public float CurrentTime => m_CurrentTime;

        private void Start()
        {
            m_RaceStateTracker.EventOnRaceStarted.AddListener(OnRaceStarted);
            m_RaceStateTracker.EventOnRaceFinished.AddListener(OnRaceFinished);

            enabled = false;
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
