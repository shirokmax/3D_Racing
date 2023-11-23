using UnityEngine;
using UnityEngine.Events;

namespace CarRacing
{
    public class TrackPoint : MonoBehaviour
    {
        private UnityEvent<TrackPoint> m_EventOnTriggered = new UnityEvent<TrackPoint>();
        public UnityEvent<TrackPoint> EventOnTriggered => m_EventOnTriggered;

        [SerializeField] private TrackPoint m_Next;
        public TrackPoint Next => m_Next;

        [SerializeField] private bool m_IsFirst;
        public bool IsFirst => m_IsFirst;

        [SerializeField] private bool m_IsLast;
        public bool IsLast => m_IsLast;

        [Header("Visualization")]
        [SerializeField] private GameObject m_TargetVisualization;
        [SerializeField] private GameObject m_StartCanvas;
        [SerializeField] private GameObject m_FinishCanvas;

        protected bool m_IsTarget;
        public bool IsTarget => m_IsTarget;

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.root.GetComponent<Car>() == null) return;

            m_EventOnTriggered?.Invoke(this);
        }

        public void Passed()
        {
            m_IsTarget = false;
            m_TargetVisualization.SetActive(false);
        }

        public void AssignAsTarget()
        {
            m_IsTarget = true;
            m_TargetVisualization.SetActive(true);
        }

        public void SetNext(TrackPoint point)
        {
            m_Next = point;
        }

        public void SetFirst()
        {
            m_IsFirst = true;
            m_StartCanvas.SetActive(true);
        }

        public void SetLast()
        {
            m_IsLast = true;
            m_FinishCanvas.SetActive(true);
        }

        public void SetCircularFirst()
        {
            SetFirst();
            m_IsLast = true;
        }

        public void Reset()
        {
            m_Next = null;
            m_IsTarget = false;
            m_IsFirst = false;
            m_IsLast = false;

            m_TargetVisualization.SetActive(false);
            m_StartCanvas.SetActive(false);
            m_FinishCanvas.SetActive(false);
        }
    }
}
