using UnityEngine;
using UnityEngine.UI;

namespace UnityDrift
{
    [RequireComponent(typeof(Text))]
    public class UITrackTime : MonoBehaviour, IDependency<RaceStateTracker>, IDependency<RaceTimeTracker>
    {
        private Text m_TimeText;

        private RaceStateTracker m_RaceStateTracker;
        public void Construct(RaceStateTracker obj) => m_RaceStateTracker = obj;

        private RaceTimeTracker m_RaceTimeTracker;
        public void Construct(RaceTimeTracker obj) => m_RaceTimeTracker = obj;

        private void Awake()
        {
            m_TimeText = GetComponent<Text>();
        }

        private void Start()
        {
            m_RaceStateTracker.EventOnRaceStarted.AddListener(OnRaceStarted);
            m_RaceStateTracker.EventOnRaceFinished.AddListener(OnRaceFinished);

            m_TimeText.enabled = false;
            enabled = false;
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