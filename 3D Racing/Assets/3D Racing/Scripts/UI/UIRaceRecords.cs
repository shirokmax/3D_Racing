using UnityEngine;
using UnityEngine.UI;

namespace CarRacing
{
    public class UIRaceRecords : MonoBehaviour, IDependency<RaceStateTracker>, IDependency<RaceDriftTracker>, IDependency<RaceResults>
    {
        [SerializeField] private GameObject m_Panels;

        [Space]
        [SerializeField] private GameObject m_GoldTimeRecordPanel;
        [SerializeField] private GameObject m_PlayerTimeRecordPanel;
        [SerializeField] private GameObject m_BestDriftRecordPanel;
        [SerializeField] private GameObject m_PlayerDriftRecordPanel;

        [Space]
        [SerializeField] private Text m_GoldTimeRecordText;
        [SerializeField] private Text m_PlayerTimeRecordText;
        [SerializeField] private Text m_DriftRecordText;
        [SerializeField] private Text m_PlayerDriftRecordText;

        private RaceStateTracker m_RaceStateTracker;
        public void Construct(RaceStateTracker obj) => m_RaceStateTracker = obj;

        private RaceDriftTracker m_RaceDriftTracker;
        public void Construct(RaceDriftTracker obj) => m_RaceDriftTracker = obj;

        private RaceResults m_RaceResults;
        public void Construct(RaceResults obj) => m_RaceResults = obj;

        private void Start()
        {
            m_RaceStateTracker.EventOnRaceStarted.AddListener(OnRaceStarted);
            m_RaceStateTracker.EventOnRaceFinished.AddListener(OnRaceFinished);

            m_Panels.SetActive(true);
            DisableRecordPanels();
            enabled = false;
        }

        private void Update()
        {
            m_PlayerDriftRecordText.text = ((int)m_RaceDriftTracker.TotalPoints).ToString();
        }

        private void OnRaceStarted()
        {
            enabled = true;
            m_BestDriftRecordPanel.SetActive(true);
            m_PlayerDriftRecordPanel.SetActive(true);

            if (m_RaceResults.PlayerRecordTime > m_RaceResults.GoldTime || m_RaceResults.IsTimeRecordSetted == false)
            {
                m_GoldTimeRecordPanel.SetActive(true);
                m_GoldTimeRecordText.text = StringTime.SecondToTimeString(m_RaceResults.GoldTime);
            }

            if (m_RaceResults.IsTimeRecordSetted == true)
            {
                m_PlayerTimeRecordPanel.SetActive(true);
                m_PlayerTimeRecordText.text = StringTime.SecondToTimeString(m_RaceResults.PlayerRecordTime);
            }

            if (m_RaceResults.PlayerRecordDrift > m_RaceResults.GoldDrift)
                m_DriftRecordText.text = ((int)m_RaceResults.PlayerRecordDrift).ToString();
            else
                m_DriftRecordText.text = ((int)m_RaceResults.GoldDrift).ToString();
        }

        private void OnRaceFinished()
        {
            enabled = false;
            DisableRecordPanels();
        }

        private void DisableRecordPanels()
        {
            m_GoldTimeRecordPanel.SetActive(false);
            m_PlayerTimeRecordPanel.SetActive(false);
            m_BestDriftRecordPanel.SetActive(false);
            m_PlayerDriftRecordPanel.SetActive(false);
        }
    }
}
