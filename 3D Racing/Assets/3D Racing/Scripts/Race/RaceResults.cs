using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace CarRacing
{
    public class RaceResults : MonoBehaviour, IDependency<RaceStateTracker>, IDependency<RaceTimeTracker>, IDependency<RaceDriftTracker>
    {
        public const string TIME_SAVE_MARK = "_player_best_time";
        public const string DRIFT_SAVE_MARK = "_player_best_drift";

        [SerializeField] private float m_GoldTime;
        public float GoldTime => m_GoldTime;

        [SerializeField] private float m_GoldDrift;
        public float GoldDrift => m_GoldDrift;

        private RaceStateTracker m_RaceStateTracker;
        public void Construct(RaceStateTracker obj) => m_RaceStateTracker = obj;

        private RaceTimeTracker m_RaceTimeTracker;
        public void Construct(RaceTimeTracker obj) => m_RaceTimeTracker = obj;

        private RaceDriftTracker m_RaceDriftTracker;
        public void Construct(RaceDriftTracker obj) => m_RaceDriftTracker = obj;

        private float m_PlayerRecordTime;
        public float PlayerRecordTime => m_PlayerRecordTime;

        private float m_CurrentTime;
        public float CurrentTime => m_CurrentTime;

        private float m_PlayerRecordDrift;
        public float PlayerRecordDrift => m_PlayerRecordDrift;

        private float m_CurrentDriftPoints;
        public float CurrentDriftPoints => m_CurrentDriftPoints;

        private UnityEvent m_EventOnResultsUpdated = new UnityEvent();
        public UnityEvent EventOnResultsUpdated => m_EventOnResultsUpdated;

        public bool IsTimeRecordSetted => m_PlayerRecordTime != 0;

        private void Awake()
        {
            Load();
        }

        private void Start()
        {
            m_RaceStateTracker.EventOnRaceFinished.AddListener(OnRaceFinished);
        }

        private void OnRaceFinished()
        {
            float absoluteTimeRecord = GetAbsoluteTimeRecord();
            float absoluteDriftRecord = GetAbsoluteDriftRecord();

            if (m_RaceTimeTracker.CurrentTime < absoluteTimeRecord || IsTimeRecordSetted == false)
            {
                m_PlayerRecordTime = m_RaceTimeTracker.CurrentTime;

                SaveTimeRecord();
            }

            if (m_RaceDriftTracker.TotalPoints > absoluteDriftRecord)
            {
                m_PlayerRecordDrift = m_RaceDriftTracker.TotalPoints;

                SaveDriftRecord();
            }

            m_CurrentTime = m_RaceTimeTracker.CurrentTime;
            m_CurrentDriftPoints = m_RaceDriftTracker.TotalPoints;

            m_EventOnResultsUpdated?.Invoke();
        }

        public float GetAbsoluteTimeRecord()
        {
            if (m_PlayerRecordTime < m_GoldTime && IsTimeRecordSetted)
                return m_PlayerRecordTime;
            else
                return m_GoldTime;
        }

        public float GetAbsoluteDriftRecord()
        {
            if (m_PlayerRecordDrift > m_GoldDrift)
                return m_PlayerRecordDrift;
            else
                return m_GoldDrift;
        }

        private void Load()
        {
            m_PlayerRecordTime = PlayerPrefs.GetFloat(SceneManager.GetActiveScene().buildIndex + TIME_SAVE_MARK, 0);
            m_PlayerRecordDrift = PlayerPrefs.GetFloat(SceneManager.GetActiveScene().buildIndex + DRIFT_SAVE_MARK, 0);
        }

        private void SaveTimeRecord()
        {
            PlayerPrefs.SetFloat(SceneManager.GetActiveScene().buildIndex + TIME_SAVE_MARK, m_PlayerRecordTime);
        }

        private void SaveDriftRecord()
        {
            PlayerPrefs.SetFloat(SceneManager.GetActiveScene().buildIndex + DRIFT_SAVE_MARK, m_PlayerRecordDrift);
        }
    }
}