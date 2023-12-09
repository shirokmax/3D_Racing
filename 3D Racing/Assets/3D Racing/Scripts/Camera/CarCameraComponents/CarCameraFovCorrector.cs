using UnityEngine;

namespace UnityDrift
{
    [RequireComponent(typeof(Camera))]
    public class CarCameraFovCorrector : MonoBehaviour, IDependency<Car>
    {
        [Space]
        [SerializeField] private float m_MinFieldOfView;
        [SerializeField] private float m_MaxFieldOfView;

        private Camera m_Camera;

        private Car m_Car;
        public void Construct(Car obj) => m_Car = obj;

        private void Awake()
        {
            m_Camera = GetComponent<Camera>();
        }

        private void Update()
        {
            m_Camera.fieldOfView = Mathf.Lerp(m_MinFieldOfView, m_MaxFieldOfView, m_Car.NormalizeLinearVelocity);
        }
    }
}
