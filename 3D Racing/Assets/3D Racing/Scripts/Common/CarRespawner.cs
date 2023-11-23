using UnityEngine;

namespace CarRacing
{
    public class CarRespawner : MonoBehaviour, IDependency<Car>, IDependency<CarInputControl>, IDependency<RaceStateTracker>
    {
        [SerializeField] private float m_RespawnHeight;

        private Car m_Car;
        public void Construct(Car obj) => m_Car = obj;

        private CarInputControl m_CarInputControl;
        public void Construct(CarInputControl obj) => m_CarInputControl = obj;

        private RaceStateTracker m_RaceStateTracker;
        public void Construct(RaceStateTracker obj) => m_RaceStateTracker = obj;

        private TrackPoint m_RespawnPoint;

        private void Start()
        {
            m_RaceStateTracker.EventOnTrackPointPassed.AddListener(TrackPointPassed);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Respawn();
            }
        }

        private void TrackPointPassed(TrackPoint point)
        {
            m_RespawnPoint = point;
        }

        private void Respawn()
        {
            if (m_RespawnPoint == null) return;
            if (m_RaceStateTracker.State != RaceState.Race) return;

            m_Car.Respawn(m_RespawnPoint.transform.position + m_RespawnPoint.transform.up * m_RespawnHeight, 
                          m_RespawnPoint.transform.rotation);

            m_CarInputControl.Reset();
        }
    }
}
