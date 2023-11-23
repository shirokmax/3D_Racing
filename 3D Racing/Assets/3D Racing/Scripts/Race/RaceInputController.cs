using UnityEngine;

namespace CarRacing
{
    public class RaceInputController : MonoBehaviour, IDependency<RaceStateTracker>, IDependency<CarInputControl>
    {
        private CarInputControl m_CarControl;
        public void Construct(CarInputControl obj) => m_CarControl = obj;

        private RaceStateTracker m_RaceStateTracker;
        public void Construct(RaceStateTracker obj) => m_RaceStateTracker = obj;

        private void Start()
        {
            m_RaceStateTracker.EventOnRaceStarted.AddListener(OnRaceStarted);
            m_RaceStateTracker.EventOnRaceFinished.AddListener(OnRaceFinished);

            m_CarControl.enabled = false;
        }

        private void OnRaceStarted()
        {
            m_CarControl.enabled = true;
        }

        private void OnRaceFinished()
        {
            m_CarControl.Stop();
            m_CarControl.enabled = false;
        }
    }
}
