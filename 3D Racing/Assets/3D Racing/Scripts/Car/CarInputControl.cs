using UnityEngine;
using UnityEngine.SceneManagement;

namespace CarRacing
{
    public class CarInputControl : MonoBehaviour, IDependency<Car>
    {
        [SerializeField] private AnimationCurve m_BreakCurve;
        [SerializeField] private AnimationCurve m_SteerCurve;

        [Space]
        [SerializeField] private float m_SteerSensetivity = 3f;
        [SerializeField] private float m_AutoSteerInterpolation = 5f;
        [SerializeField] private float m_SteerAutoAngleFactor = 0.8f;
        [SerializeField] private float m_WheelSpeedThreshold = 50;

        [SerializeField][Range(0.0f, 1.0f)] private float m_AutoBrakeStrength = 0.4f;

        private Car m_Car;
        public void Construct(Car obj) => m_Car = obj;

        private float m_VerticalAxis;
        private float m_HorizontalAxis;
        private float m_HandbrakeAxis;

        private bool isCarStay => m_Car.WheelSpeed > -8f && m_Car.WheelSpeed < 8f;

        private void Update()
        {
            UpdateAxis();

            UpdateThrottleAndBrakes();
            UpdateSteer();

            UpdateAutoBrake();
        }

        /// <summary>
        /// Ручное переключение передач.
        /// </summary>
        private void ManualGearShift()
        {
            if (Input.GetKeyDown(KeyCode.E))
                m_Car.UpGear();

            if (Input.GetKeyDown(KeyCode.Q))
                m_Car.DownGear();
        }

        public void UpdateAxis()
        {
            UpdateHorizontalAxis();
            m_VerticalAxis = Input.GetAxis("Vertical");
            m_HandbrakeAxis = Input.GetAxis("Jump");
        }

        public void UpdateHorizontalAxis()
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                if (Input.GetKey(KeyCode.A))
                    m_HorizontalAxis -= Time.deltaTime * m_SteerSensetivity;

                if (Input.GetKey(KeyCode.D))
                    m_HorizontalAxis += Time.deltaTime * m_SteerSensetivity;

                m_HorizontalAxis = Mathf.Clamp(m_HorizontalAxis, -1, 1);
            }
            else if (isCarStay == false && m_Car.WheelSpeed > m_WheelSpeedThreshold)
            {
                float autoAngle = Mathf.Clamp(-Vector3.SignedAngle(m_Car.CarChassis.VelocityDir, m_Car.transform.forward, Vector3.up), -m_Car.MaxSteerAngle, m_Car.MaxSteerAngle) * Mathf.Sign(m_Car.WheelSpeed);

                m_HorizontalAxis = Mathf.Lerp(m_HorizontalAxis, autoAngle * m_SteerAutoAngleFactor / m_Car.MaxSteerAngle, Time.deltaTime * m_AutoSteerInterpolation);
            }
            else
            {
                m_HorizontalAxis = Mathf.Lerp(m_HorizontalAxis, 0, Time.deltaTime * m_AutoSteerInterpolation);
            }
        }

        private void UpdateThrottleAndBrakes()
        {
            if (Mathf.Sign(m_VerticalAxis) == Mathf.Sign(m_Car.WheelSpeed) || isCarStay)
            {
                m_Car.ThrottleControl = Mathf.Abs(m_VerticalAxis);
                m_Car.BrakeControl = 0;
            }
            else
            {
                m_Car.ThrottleControl = 0;
                m_Car.BrakeControl = m_BreakCurve.Evaluate(m_Car.WheelSpeed / m_Car.MaxSpeed);
            }

            m_Car.HandbrakeControl = m_HandbrakeAxis;

            // Gears
            if (isCarStay)
            {
                if (m_VerticalAxis > 0)
                    m_Car.ShiftToFirstGear();

                if (m_VerticalAxis < 0)
                    m_Car.ShiftToReverseGear();
            }
        }

        private void UpdateSteer()
        {
            m_Car.SteerControl = m_SteerCurve.Evaluate(m_Car.WheelSpeed / m_Car.MaxSpeed) * m_HorizontalAxis;
        }

        private void UpdateAutoBrake()
        {
            if (m_VerticalAxis == 0)
                m_Car.BrakeControl = m_BreakCurve.Evaluate(m_Car.WheelSpeed / m_Car.MaxSpeed) * m_AutoBrakeStrength;
        }

        public void Reset()
        {
            m_HorizontalAxis = 0;
            m_VerticalAxis = 0;
            m_HandbrakeAxis = 0;
        }

        public void Stop()
        {
            Reset();

            m_Car.ThrottleControl = 0;
            m_Car.SteerControl= 0;
            m_Car.HandbrakeControl = 0;
            m_Car.BrakeControl = 1;
        }
    }
}
