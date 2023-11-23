using UnityEngine;
using UnityEngine.Events;

namespace CarRacing
{
    public enum RaceState
    {
        Preparation,
        Countdown,
        Race,
        Completed
    }

    public class RaceStateTracker : MonoBehaviour, IDependency<TrackpointCircuit>
    {
        [SerializeField] private SimpleTimer m_CountdownTimer;
        public SimpleTimer CountdownTimer => m_CountdownTimer;

        [SerializeField] private int m_LapsToComplete;

        private TrackpointCircuit m_TrackpointCircuit;
        public void Construct(TrackpointCircuit obj) => m_TrackpointCircuit = obj;

        private RaceState m_State;
        public RaceState State => m_State;

        private UnityEvent m_EventOnPrepararionStarted = new UnityEvent();
        public UnityEvent EventOnPrepararionStarted => m_EventOnPrepararionStarted;

        private UnityEvent m_EventOnRaceStarted = new UnityEvent();
        public UnityEvent EventOnRaceStarted => m_EventOnRaceStarted;

        private UnityEvent m_EventOnRaceFinished = new UnityEvent();
        public UnityEvent EventOnRaceFinished => m_EventOnRaceFinished;

        private UnityEvent<TrackPoint> m_EventOnTrackPointPassed = new UnityEvent<TrackPoint>();
        public UnityEvent<TrackPoint> EventOnTrackPointPassed => m_EventOnTrackPointPassed;

        private UnityEvent<int> m_EventOnLapCompleted = new UnityEvent<int>();
        public UnityEvent<int> EventOnLapCompleted => m_EventOnLapCompleted;

        private void Start()
        {
            StartState(RaceState.Preparation);

            m_TrackpointCircuit.EventOnTrackPointTriggered.AddListener(OnTrackPointTriggered);
            m_TrackpointCircuit.EventOnLapCompleted.AddListener(OnLapCompleted);

            m_CountdownTimer.enabled = false;
            m_CountdownTimer.EventOnFinished.AddListener(OnTimerFinished);
        }

        private void StartState(RaceState state)
        {
            m_State = state;
        }

        private void OnTimerFinished()
        {
            StartRace();
        }

        private void OnTrackPointTriggered(TrackPoint trackPoint)
        {
            m_EventOnTrackPointPassed?.Invoke(trackPoint);
        }

        private void OnLapCompleted(int lapAmount)
        {
            if (m_TrackpointCircuit.TrackType == TrackType.Sprint)
                CompleteRace();

            if (m_TrackpointCircuit.TrackType == TrackType.Circular)
            {
                if (lapAmount == m_LapsToComplete)
                    CompleteRace();
                else
                    CompleteLap(lapAmount);
            }
        }

        public void LaunchPreparationStart()
        {
            if (m_State != RaceState.Preparation) return;
            StartState(RaceState.Countdown);

            m_CountdownTimer.enabled = true;

            m_EventOnPrepararionStarted?.Invoke();
        }

        private void StartRace()
        {
            if (m_State != RaceState.Countdown) return;
            StartState(RaceState.Race);

            m_EventOnRaceStarted?.Invoke();
        }

        private void CompleteRace()
        {
            if (m_State != RaceState.Race) return;
            StartState(RaceState.Completed);

            m_EventOnRaceFinished?.Invoke();
        }

        private void CompleteLap(int lapAmount)
        {
            m_EventOnLapCompleted?.Invoke(lapAmount);
        }
    }
}
