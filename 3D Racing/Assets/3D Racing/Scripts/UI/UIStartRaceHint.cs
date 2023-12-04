using UnityEngine;

namespace CarRacing
{
    public class UIStartRaceHint : MonoBehaviour, IDependency<RaceStateTracker>
    {
        private RaceStateTracker m_RaceStateTracker;
        public void Construct(RaceStateTracker obj) => m_RaceStateTracker = obj;

        private void Start()
        {
            m_RaceStateTracker.EventOnCountdownStarted.AddListener(OnCountdownStarted);

            enabled = true;
            gameObject.SetActive(true);
        }

        private void OnCountdownStarted()
        {
            gameObject.SetActive(false);
            enabled = false;
        }
    }
}
