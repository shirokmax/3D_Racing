using UnityEngine;
using UnityEngine.Events;

namespace UnityDrift
{
    public class TrackpointCircuit : MonoBehaviour, IDependency<LoadedRaceInfo>
    {
        [SerializeField] private AudioSource m_rackPointTriggerSound;

        private LoadedRaceInfo m_LoadedRaceInfo;
        public void Construct(LoadedRaceInfo obj) => m_LoadedRaceInfo = obj;

        private TrackType m_TrackType;
        public TrackType TrackType => m_TrackType;

        private TrackPoint[] m_Points;
        private int m_LapsCompleted = -1;

        private UnityEvent<TrackPoint> m_EventOnTrackPointTriggered = new UnityEvent<TrackPoint>();
        public UnityEvent<TrackPoint> EventOnTrackPointTriggered => m_EventOnTrackPointTriggered;

        private UnityEvent<int> m_EventOnLapCompleted = new UnityEvent<int>();
        public UnityEvent<int> EventOnLapCompleted => m_EventOnLapCompleted;

        private void Start()
        {
            m_TrackType = m_LoadedRaceInfo.Info.TrackType;
            BuildCircuit();

            foreach (var point in m_Points)
                point.EventOnTriggered.AddListener(OnTrackPointTriggered);            
        }

        [ContextMenu(nameof(BuildCircuit))]
        private void BuildCircuit()
        {
            m_Points = TrackCircuitBuilder.Build(transform, m_TrackType);
        }

        private void OnTrackPointTriggered(TrackPoint trackPoint)
        {
            if (trackPoint.IsTarget == false) return;

            trackPoint.Passed();
            trackPoint.Next?.AssignAsTarget();

            m_EventOnTrackPointTriggered?.Invoke(trackPoint);

            if (m_rackPointTriggerSound != null)
                m_rackPointTriggerSound.Play();

            if (trackPoint.IsLast)
            {
                m_LapsCompleted++;

                if (m_TrackType == TrackType.Sprint)
                    m_EventOnLapCompleted?.Invoke(m_LapsCompleted);

                if (m_TrackType == TrackType.Circular)
                    if (m_LapsCompleted > 0)
                        m_EventOnLapCompleted?.Invoke(m_LapsCompleted);
            }
        }
    }
}
