using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace UnityDrift
{
    public class RaceResults : MonoBehaviour, IDependency<RaceStateTracker>, IDependency<RaceTimeTracker>, IDependency<RaceDriftTracker>, IDependency<LoadedRaceInfo>, IDependency<RaceCompletion>
    {
        public const string TIME_SAVE_MARK = "_player_best_time";
        public const string DRIFT_SAVE_MARK = "_player_best_drift";
        public const string COMPLETE_SAVE_MARK = "_complete";

        private RaceStateTracker m_RaceStateTracker;
        public void Construct(RaceStateTracker obj) => m_RaceStateTracker = obj;

        private RaceTimeTracker m_RaceTimeTracker;
        public void Construct(RaceTimeTracker obj) => m_RaceTimeTracker = obj;

        private RaceDriftTracker m_RaceDriftTracker;
        public void Construct(RaceDriftTracker obj) => m_RaceDriftTracker = obj;

        private LoadedRaceInfo m_LoadedRaceInfo;
        public void Construct(LoadedRaceInfo obj) => m_LoadedRaceInfo = obj;

        private RaceCompletion m_RaceCompletion;
        public void Construct(RaceCompletion obj) => m_RaceCompletion = obj;

        private float m_GoldTime;
        public float GoldTime => m_GoldTime;

        private float m_GoldDrift;
        public float GoldDrift => m_GoldDrift;

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

            m_GoldTime = m_LoadedRaceInfo.Info.GoldTime;
            m_GoldDrift = m_LoadedRaceInfo.Info.GoldDrift;

            m_RaceStateTracker.EventOnRaceFinished.AddListener(OnRaceFinished);
        }

        private void OnDestroy()
        {
            // Сохранение рекорда дрифта для сцен Free Drift
            if (m_LoadedRaceInfo.Info.TrackType == TrackType.None &&
                m_LoadedRaceInfo.Info.RaceType == RaceType.Drift)
            {
                if (m_RaceDriftTracker.TotalPoints > m_PlayerRecordDrift)
                {
                    m_PlayerRecordDrift = m_RaceDriftTracker.TotalPoints;
                    SaveDriftRecord();
                }
            }
        }

        private void OnRaceFinished()
        {
            if (m_LoadedRaceInfo.Info.RaceType == RaceType.Race)
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

            if (m_LoadedRaceInfo.Info.RaceType == RaceType.Drift)
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
            m_PlayerRecordTime = m_RaceCompletion.GetBestPlayerTime(m_LoadedRaceInfo.Info);
            m_PlayerRecordDrift = m_RaceCompletion.GetBestPlayerDrift(m_LoadedRaceInfo.Info);
        }

        private void SaveTimeRecord()
        {
            m_RaceCompletion.SaveRecordTime(m_LoadedRaceInfo.Info, m_PlayerRecordTime);
        }

        private void SaveDriftRecord()
        {
            m_RaceCompletion.SaveRecordDrift(m_LoadedRaceInfo.Info, m_PlayerRecordDrift);
        }

        private void SaveComplete()
        {
            m_RaceCompletion.SaveCompletion(m_LoadedRaceInfo.Info);
        }
    }
}