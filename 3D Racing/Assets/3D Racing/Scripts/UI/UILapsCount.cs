using UnityEngine;
using UnityEngine.UI;

namespace UnityDrift
{
    public class UILapsCount : MonoBehaviour, IDependency<TrackpointCircuit>, IDependency<RaceStateTracker>
    {
        [SerializeField] private GameObject m_LapsCountPanel;
        [SerializeField] private Text m_LapsCountText;

        private TrackpointCircuit m_TrackpointCircuit;
        public void Construct(TrackpointCircuit obj) => m_TrackpointCircuit = obj;

        private RaceStateTracker m_RaceStateTracker;
        public void Construct(RaceStateTracker obj) => m_RaceStateTracker = obj;

        private void Start()
        {
            m_LapsCountPanel.SetActive(false);

            if (m_TrackpointCircuit.TrackType == TrackType.Circular)
                m_TrackpointCircuit.EventOnLapCompleted.AddListener(OnLapCompleted);

            m_RaceStateTracker.EventOnRaceStarted.AddListener(OnRaceStarted);
            m_RaceStateTracker.EventOnRaceFinished.AddListener(OnRaceFinished);
        }

        private void OnDestroy()
        {
            if (m_TrackpointCircuit.TrackType == TrackType.Circular)
                m_TrackpointCircuit.EventOnLapCompleted.RemoveListener(OnLapCompleted);

            m_RaceStateTracker.EventOnRaceStarted.RemoveListener(OnRaceStarted);
            m_RaceStateTracker.EventOnRaceFinished.RemoveListener(OnRaceFinished);
        }

        private void OnRaceStarted()
        {
            switch (m_TrackpointCircuit.TrackType)
            {
                case TrackType.Circular:
                    m_LapsCountText.text = $"Laps 1/{m_RaceStateTracker.LapsToComplete}";
                    break;
                case TrackType.Sprint:
                    m_LapsCountText.text = "Laps 1/1";
                    break;
                case TrackType.None:
                    m_LapsCountPanel.SetActive(false);
                    return;
            }
               
            m_LapsCountPanel.SetActive(true);
        }

        private void OnLapCompleted(int lapsCount)
        {
            m_LapsCountText.text = $"Laps {lapsCount + 1}/{m_RaceStateTracker.LapsToComplete}";
        }

        private void OnRaceFinished()
        {
            m_LapsCountPanel.SetActive(false);
        }
    }
}
