using UnityEngine;

namespace UnityDrift
{
    [RequireComponent(typeof(Camera))]
    public class CarCameraController : MonoBehaviour, IDependency<RaceStateTracker>, IDependency<Car>
    {
        [SerializeField] private CarCameraFollower m_CameraFollow;
        [SerializeField] private CarCameraFovCorrector m_CameraFovCorrector;
        [SerializeField] private CarCameraShaker m_CameraShaker;
        [SerializeField] private CameraPathFollower m_CameraPathFollower;

        private Car m_Car;
        public void Construct(Car obj) => m_Car = obj;

        private RaceStateTracker m_RaceStateTracker;
        public void Construct(RaceStateTracker obj) => m_RaceStateTracker = obj;

        private void Start()
        {
            m_RaceStateTracker.EventOnCountdownStarted.AddListener(OnCountdownStarted);
            m_RaceStateTracker.EventOnRaceFinished.AddListener(OnRaceFinished);

            m_CameraFollow.enabled = false;
            m_CameraPathFollower.enabled = true;
        }

        private void OnCountdownStarted()
        {
            m_CameraFollow.enabled = true;
            m_CameraPathFollower.enabled = false;
        }

        private void OnRaceFinished()
        {
            m_CameraFollow.enabled = false;
            m_CameraPathFollower.enabled = true;

            m_CameraPathFollower.SetLookTarget(m_Car.transform);
        }
    }
}
