using UnityEngine;

namespace CarRacing
{
    [RequireComponent(typeof(Rigidbody))]
    public class CarChassis : MonoBehaviour
    {
        [SerializeField] private WheelAxle[] m_WheelAxles;
        public WheelAxle[] WheelAxles => m_WheelAxles;

        [Space]
        [SerializeField] private float m_WheelBaseLength;

        [Space]
        [SerializeField] private Transform m_CenterOfMass;

        [Header("Down Force")]
        [SerializeField] private float m_DownForceMin;
        [SerializeField] private float m_DownForceMax;
        [SerializeField] private float m_DownForceFactor;

        [Header("Angular Drag")]
        [SerializeField] private float m_AngularDragMin;
        [SerializeField] private float m_AngularDragMax;
        [SerializeField] private float m_AngularDragFactor;

        // DEBUG
        public float MotorTorque;
        public float BrakeTorque;
        public float HandbrakeTorque;
        public float SteerAngle;

        public float LinearVelocity => m_RigidBody.velocity.magnitude * 3.6f;
        public Vector3 VelocityDir => m_RigidBody.velocity.normalized;

        private Rigidbody m_RigidBody;
        public Rigidbody Rigidbody => m_RigidBody == null ? GetComponent<Rigidbody>() : m_RigidBody;

        private void Awake()
        {
            m_RigidBody = GetComponent<Rigidbody>();

            if (m_CenterOfMass != null)
                m_RigidBody.centerOfMass = m_CenterOfMass.localPosition;

            //foreach (var axle in m_WheelAxles)
            //    axle.ConfigureVehicleSubsteps(50, 50, 50);
        }

        private void FixedUpdate()
        {
            UpdateAngularDrag();
            UpdateDownForce();
            UpdateWheelAxles();
        }

        public float GetAverageRpm()
        {
            float sum = 0;

            foreach(var axle in m_WheelAxles)
                sum += axle.GetAverageRpm();

            return sum / m_WheelAxles.Length;
        }

        public float GetWheelSpeed()
        {
            return GetAverageRpm() * m_WheelAxles[0].GetWheelRadius() * 2 * 0.1885f;
        }

        private void UpdateAngularDrag()
        {
            m_RigidBody.angularDrag = Mathf.Clamp(m_AngularDragFactor * LinearVelocity, m_AngularDragMin, m_AngularDragMax);
        }

        private void UpdateDownForce()
        {
            float downForce = Mathf.Clamp(m_DownForceFactor * LinearVelocity, m_DownForceMin, m_DownForceMax);
            m_RigidBody.AddForce(-transform.up * downForce);
        }

        private void UpdateWheelAxles()
        {
            int amountMotorWheels = 0;

            for (int i = 0; i < m_WheelAxles.Length; i++)
            {
                if (m_WheelAxles[i].IsMotor == true)
                    amountMotorWheels += 2;
            }

            for (int i = 0; i < m_WheelAxles.Length; i++)
            {
                m_WheelAxles[i].UpdateAxle();

                m_WheelAxles[i].ApplyMotorTorque(MotorTorque / amountMotorWheels);
                m_WheelAxles[i].ApplyBrakeTorque(BrakeTorque + HandbrakeTorque);
                m_WheelAxles[i].ApplySteerAngle(SteerAngle, m_WheelBaseLength);
            }
        }

        public WheelCollider[] GetAllWheelColliders()
        {
            int wheelsCount = m_WheelAxles.Length * 2;

            WheelCollider[] wheelColliders = new WheelCollider[wheelsCount];

            int j = 0;

            for (int i = 0; i < m_WheelAxles.Length; i++)
            {
                wheelColliders[j] = m_WheelAxles[i].LeftWheel;
                j++;
                wheelColliders[j] = m_WheelAxles[i].RightWheel;
                j++;
            }

            return wheelColliders;
        }

        public void Reset()
        {
            m_RigidBody.velocity = Vector3.zero;
            m_RigidBody.angularVelocity = Vector3.zero;
        }
    }
}
