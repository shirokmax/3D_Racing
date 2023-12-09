using UnityEngine;

namespace UnityDrift
{
    [RequireComponent(typeof(Camera))]
    public class CarCameraFollower : MonoBehaviour, IDependency<Car>
    {
        [Header("Offset")]
        [SerializeField] private float m_ViewHeight;
        [SerializeField] private float m_Height;
        [SerializeField] private float m_Distance;

        [Header("Damping")]
        [SerializeField] private float m_RotationDamping;
        [SerializeField] private float m_HeightDamping;
        [SerializeField] private float m_SpeedThreshold = 5;

        private Car m_Car;
        public void Construct(Car obj) => m_Car = obj;

        private Transform m_Target;
        private Rigidbody m_TargetRigidbody;

        private void Awake()
        {
            m_Target = m_Car.transform;
            m_TargetRigidbody = m_Car.RigidBody;
        }

        private void FixedUpdate()
        {
            Vector3 velocity = m_TargetRigidbody.velocity;
            Vector3 targetRotation = m_Target.eulerAngles;

            if (velocity.magnitude > m_SpeedThreshold)
                targetRotation = Quaternion.LookRotation(velocity, Vector3.up).eulerAngles;

            // Lerp
            float currentAngle = Mathf.LerpAngle(transform.eulerAngles.y, targetRotation.y, m_RotationDamping * Time.fixedDeltaTime);
            float currentHeight = Mathf.Lerp(transform.position.y, m_Target.position.y + m_Height, m_HeightDamping * Time.fixedDeltaTime);

            // Position
            Vector3 positionOffset = Quaternion.Euler(0, currentAngle, 0) * Vector3.forward * m_Distance;
            transform.position = m_Target.position - positionOffset;
            transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);

            // Rotation
            transform.LookAt(m_Target.transform.position + new Vector3(0, m_ViewHeight, 0));
        }
    }
}
