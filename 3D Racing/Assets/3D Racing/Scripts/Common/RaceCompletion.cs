using System;
using UnityEngine;

namespace UnityDrift
{
    public class RaceCompletion : MonoBehaviour
    {
        [Serializable]
        private class RaceComplete
        {
            public RaceInfo raceInfo;
            public bool complete;
            public float playerBestTime;
            public float playerBestDrift;
        }

        public const string FILENAME = "Completion.dat";

        [SerializeField] private RaceComplete[] m_CompletionData;

        private void Awake()
        {
            DataSaver<RaceComplete[]>.TryLoad(FILENAME, ref m_CompletionData);
        }

        public void SaveCompletion(RaceInfo raceInfo)
        {
            foreach (var item in m_CompletionData)
            {
                if (item.raceInfo == raceInfo)
                {
                    if (item.complete == true)
                        return;

                    item.complete = true;

                    DataSaver<RaceComplete[]>.Save(FILENAME, m_CompletionData);
                }
            }
        }

        public void SaveRecordTime(RaceInfo raceInfo, float time)
        {
            foreach (var item in m_CompletionData)
            {
                if (item.raceInfo == raceInfo)
                {
                    item.playerBestTime = time;
                    DataSaver<RaceComplete[]>.Save(FILENAME, m_CompletionData);
                }
            }
        }

        public void SaveRecordDrift(RaceInfo raceInfo, float drift)
        {
            foreach (var item in m_CompletionData)
            {
                if (item.raceInfo == raceInfo)
                {
                    item.playerBestDrift = drift;
                    DataSaver<RaceComplete[]>.Save(FILENAME, m_CompletionData);
                }
            }
        }

        public bool GetCompletion(RaceInfo raceInfo)
        {
            foreach (var item in m_CompletionData)
            {
                if (item.raceInfo == raceInfo)
                    return item.complete;
            }

            return false;
        }

        public float GetBestPlayerTime(RaceInfo raceInfo)
        {
            foreach (var item in m_CompletionData)
            {
                if (item.raceInfo == raceInfo)
                    return item.playerBestTime;
            }

            return 0;
        }

        public float GetBestPlayerDrift(RaceInfo raceInfo)
        {
            foreach (var item in m_CompletionData)
            {
                if (item.raceInfo == raceInfo)
                    return item.playerBestDrift;
            }

            return 0;
        }

        public void ResetProgress()
        {
            foreach (var item in m_CompletionData)
                item.complete = false;
        }
    }
}
