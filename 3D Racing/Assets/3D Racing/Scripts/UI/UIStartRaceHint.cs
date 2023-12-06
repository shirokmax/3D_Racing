using UnityEngine;

namespace CarRacing
{
    public class UIStartRaceHint : MonoBehaviour, IDependency<RaceStateTracker>
    {
        [SerializeField] private GameObject m_StartRaceHintPanel;

        private RaceStateTracker m_RaceStateTracker;
        public void Construct(RaceStateTracker obj) => m_RaceStateTracker = obj;

        private void Start()
        {
            m_RaceStateTracker.EventOnCountdownStarted.AddListener(OnCountdownStarted);

            m_StartRaceHintPanel.SetActive(true);
        }

        private void OnCountdownStarted()
        {
            m_StartRaceHintPanel.SetActive(false);
            enabled = false;
        }
    }
}
