using UnityEngine;

namespace UnityDrift
{
    public class UIMiniMap : MonoBehaviour, IDependency<RaceStateTracker>
    {
        [SerializeField] private GameObject m_MiniMapPanel;

        private RaceStateTracker m_RaceStateTracker;
        public void Construct(RaceStateTracker obj) => m_RaceStateTracker = obj;

        private void Start()
        {
            m_MiniMapPanel.SetActive(false);

            m_RaceStateTracker.EventOnRaceStarted.AddListener(OnRaceStarted);
            m_RaceStateTracker.EventOnRaceFinished.AddListener(OnRaceFinished);
        }

        private void OnDestroy()
        {
            m_RaceStateTracker.EventOnRaceStarted.RemoveListener(OnRaceStarted);
            m_RaceStateTracker.EventOnRaceFinished.RemoveListener(OnRaceFinished);
        }

        private void OnRaceStarted()
        {
            m_MiniMapPanel.SetActive(true);
        }

        private void OnRaceFinished()
        {
            m_MiniMapPanel.SetActive(false);
        }
    }
}
