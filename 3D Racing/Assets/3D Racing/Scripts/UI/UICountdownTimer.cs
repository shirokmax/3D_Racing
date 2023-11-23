using UnityEngine;
using UnityEngine.UI;

namespace CarRacing
{
    [RequireComponent(typeof(Text))]
    public class UICountdownTimer : MonoBehaviour, IDependency<RaceStateTracker>
    {
        [Space]
        [SerializeField]  private AudioSource m_TickSound;

        private RaceStateTracker m_RaceStateTracker;
        public void Construct(RaceStateTracker obj) => m_RaceStateTracker = obj;

        private Text m_TimerText;

        private void Awake()
        {
            m_TimerText = GetComponent<Text>();
        }

        private void Start()
        {
            m_RaceStateTracker.EventOnPrepararionStarted.AddListener(OnPreparationStarted);
            m_RaceStateTracker.EventOnRaceStarted.AddListener(OnRaceStarted);
            m_RaceStateTracker.CountdownTimer.EventOnTimerSecondsTicked.AddListener(OnTimerSecondsTicked);

            m_TimerText.enabled = false;
        }

        private void OnPreparationStarted()
        {
            m_TimerText.enabled = true;
            enabled = true;

            if (m_TickSound != null)
                m_TickSound.Play();
        }

        private void OnRaceStarted()
        {
            m_TimerText.enabled = false;
            enabled = false;
        }

        private void OnTimerSecondsTicked(string tick)
        {
            m_TimerText.text = tick;

            if (tick == "0")
                m_TimerText.text = "GO!";
        }
    }
}
