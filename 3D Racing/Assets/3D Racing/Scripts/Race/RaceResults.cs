using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace UnityDrift
{
    public class RaceResults : MonoBehaviour, IDependency<RaceStateTracker>, IDependency<RaceTimeTracker>, IDependency<RaceDriftTracker>, IDependency<LoadedRaceSceneInfo>
    {
        public const string TIME_SAVE_MARK = " Player_Best_Time";
        public const string DRIFT_SAVE_MARK = " Player_Best_Drift";
        public const string COMPLETE_SAVE_MARK = " Ñomplete";

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

        private LoadedRaceSceneInfo m_LoadedRaceSceneInfo;
        public void Construct(LoadedRaceSceneInfo obj) => m_LoadedRaceSceneInfo = obj;

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

        private void Start()
        {
            Load();

            m_RaceStateTracker.EventOnRaceFinished.AddListener(OnRaceFinished);
        }

        private void OnRaceFinished()
        {
            if (m_LoadedRaceSceneInfo.Info.RaceType == RaceType.Race)
            {
                if (m_RaceTimeTracker.CurrentTime < m_GoldTime)
                    SaveComplete();

                float absoluteTimeRecord = GetAbsoluteTimeRecord();

                if (m_RaceTimeTracker.CurrentTime < absoluteTimeRecord || IsTimeRecordSetted == false)
                {
                    m_PlayerRecordTime = m_RaceTimeTracker.CurrentTime;

                    SaveTimeRecord();
                }

                m_CurrentTime = m_RaceTimeTracker.CurrentTime;
            }

            if (m_LoadedRaceSceneInfo.Info.RaceType == RaceType.Drift)
            {
                if (m_RaceDriftTracker.TotalPoints > m_GoldDrift)
                    SaveComplete();

                float absoluteDriftRecord = GetAbsoluteDriftRecord();

                if (m_RaceDriftTracker.TotalPoints > absoluteDriftRecord)
                {
                    m_PlayerRecordDrift = m_RaceDriftTracker.TotalPoints;

                    SaveDriftRecord();
                }

                m_CurrentDriftPoints = m_RaceDriftTracker.TotalPoints;
            }

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
            m_PlayerRecordTime = PlayerPrefs.GetFloat(m_LoadedRaceSceneInfo.Info.Title + TIME_SAVE_MARK, 0);
            m_PlayerRecordDrift = PlayerPrefs.GetFloat(m_LoadedRaceSceneInfo.Info.Title + DRIFT_SAVE_MARK, 0);
        }

        private void SaveTimeRecord()
        {
            PlayerPrefs.SetFloat(m_LoadedRaceSceneInfo.Info.Title + TIME_SAVE_MARK, m_PlayerRecordTime);
        }

        private void SaveDriftRecord()
        {
            PlayerPrefs.SetFloat(m_LoadedRaceSceneInfo.Info.Title + DRIFT_SAVE_MARK, m_PlayerRecordDrift);
        }

        private void SaveComplete()
        {
            PlayerPrefs.SetInt(m_LoadedRaceSceneInfo.Info.Title + COMPLETE_SAVE_MARK, 1);
        }
    }
}