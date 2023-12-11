using UnityEngine;
using UnityEngine.UI;

namespace UnityDrift
{
    [RequireComponent(typeof(Text))]
    public class UITrackTime : MonoBehaviour, IDependency<RaceStateTracker>, IDependency<RaceTimeTracker>, IDependency<LoadedRaceInfo>
    {
        private Text m_TimeText;

        private RaceStateTracker m_RaceStateTracker;
        public void Construct(RaceStateTracker obj) => m_RaceStateTracker = obj;

        private RaceTimeTracker m_RaceTimeTracker;
        public void Construct(RaceTimeTracker obj) => m_RaceTimeTracker = obj;

        private LoadedRaceInfo m_LoadedRaceSceneInfo;
        public void Construct(LoadedRaceInfo obj) => m_LoadedRaceSceneInfo = obj;

        private void Awake()
        {
            m_TimeText = GetComponent<Text>();
        }

        private void Start()
        {
            m_TimeText.enabled = false;
            enabled = false;

            if (m_LoadedRaceSceneInfo.Info.RaceType != RaceType.Race)
                return;

            m_RaceStateTracker.EventOnRaceStarted.AddListener(OnRaceStarted);
            m_RaceStateTracker.EventOnRaceFinished.AddListener(OnRaceFinished);
        }

        private void Update()
        {
            m_TimeText.text = StringTime.SecondToTimeString(m_RaceTimeTracker.CurrentTime);
        }

        private void OnRaceStarted()
        {
            m_TimeText.enabled = true;
            enabled = true;
        }

        private void OnRaceFinished()
        {
            m_TimeText.enabled = false;
            enabled = false;
        }
    }
}