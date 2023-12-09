using UnityEngine;
using UnityEngine.Events;

namespace UnityDrift
{
    [RequireComponent(typeof(CarChassis))]
    public class Car : MonoBehaviour
    {
        #region Properties
        [SerializeField] private string m_CarName;
        public string CarName => m_CarName;

        [SerializeField] private float m_MaxSpeed;
        public float MaxSpeed => m_MaxSpeed;

        [SerializeField] private float m_MaxSteerAngle;
        public float MaxSteerAngle => m_MaxSteerAngle;

        [SerializeField] private float m_MaxBrakeTorque;
        [SerializeField] private float m_MaxHandbrakeTorque;

        [Space]
        [SerializeField] private CarPreset[] m_CarPresets;

        [Header("Engine")]
        [SerializeField] private AnimationCurve m_EngineTorqueCurve;
        [SerializeField] private float m_EngineMaxTorque;
        [SerializeField] private float m_EngineMinRpm;

        [SerializeField] private float m_EngineMaxRpm;
        public float EngineMaxRpm => m_EngineMaxRpm;

        [Header("Gearbox")]
        [SerializeField] private float[] m_Gears;
        public float[] Gears => m_Gears;

        [SerializeField] private float m_FinalDriveRatio;
        [SerializeField] private float m_RearGear;

        [SerializeField] private float m_UpShiftEngineRpm;
        public float UpShiftEngineRpm => m_UpShiftEngineRpm;

        [SerializeField] private float m_DownShiftEngineRpm;
        public float DownShiftEngineRpm => m_DownShiftEngineRpm;

        public float LinearVelocity => m_CarChassis.LinearVelocity;
        public float NormalizeLinearVelocity => m_CarChassis.LinearVelocity / MaxSpeed;
        public float WheelSpeed => m_CarChassis.GetWheelSpeed();

        public WheelCollider[] WheelColliders => m_CarChassis.GetAllWheelColliders();

        private int m_SelectedGearIndex;
        private float m_SelectedGear;

        private float m_EngineTorque;

        private float m_EngineRpm;
        public float EngineRpm => m_EngineRpm;

        public float ThrottleControl { get; set; }
        public float BrakeControl { get; set; }
        public float HandbrakeControl { get; set; }
        public float SteerControl { get; set; }

        private UnityEvent<string> m_EventOnShiftGear = new UnityEvent<string>();
        public UnityEvent<string> EventOnShiftGear => m_EventOnShiftGear;

        private UnityEvent<Collision> m_EventOnCollision = new UnityEvent<Collision>();
        public UnityEvent<Collision> EventOnCollision => m_EventOnCollision;

        private CarChassis m_CarChassis;
        public CarChassis CarChassis => m_CarChassis;
        public Rigidbody RigidBody => m_CarChassis == null ? GetComponent<CarChassis>().Rigidbody : m_CarChassis.Rigidbody;

        #endregion

        private void Awake()
        {
            m_CarChassis = GetComponent<CarChassis>();
        }

        private void Update()
        {
            UpdateEngineTorque();

            AutoGearShift();

            m_CarChassis.MotorTorque = ThrottleControl * m_EngineTorque;
            m_CarChassis.BrakeTorque = BrakeControl * m_MaxBrakeTorque;
            m_CarChassis.HandbrakeTorque = HandbrakeControl * m_MaxHandbrakeTorque;
            m_CarChassis.SteerAngle = SteerControl * m_MaxSteerAngle;
        }

        public bool ApplyPreset(CarPresetType type)
        {
            foreach(var preset in m_CarPresets)
            {
                if (preset.Type == type)
                    ApplyPresetProperties(preset);

                return true;
            }

            return false;
        }

        private void ApplyPresetProperties(CarPreset props)
        {
            //TODO: применять пресет на все нужные компоненты
        }

        private void UpdateEngineTorque()
        {
            m_EngineRpm = m_EngineMinRpm + Mathf.Abs(m_CarChassis.GetAverageRpm() * m_SelectedGear * m_FinalDriveRatio);
            m_EngineRpm = Mathf.Clamp(m_EngineRpm, m_EngineMinRpm, m_EngineMaxRpm);

            m_EngineTorque = m_EngineTorqueCurve.Evaluate(m_EngineRpm / m_EngineMaxRpm) * m_EngineMaxTorque * m_FinalDriveRatio * Mathf.Sign(m_SelectedGear) * m_Gears[0];
        }

        #region GearBox
        private void ShiftGear(int gearIndex)
        {
            gearIndex = Mathf.Clamp(gearIndex, 0, m_Gears.Length - 1);
            m_SelectedGear = m_Gears[gearIndex];
            m_SelectedGearIndex = gearIndex;

            m_EventOnShiftGear?.Invoke((m_SelectedGearIndex + 1).ToString());
        }

        public void AutoGearShift()
        {
            if (m_SelectedGear < 0) return;

            if (m_EngineRpm >= m_UpShiftEngineRpm)
                UpGear();

            if (m_EngineRpm < m_DownShiftEngineRpm)
                DownGear();
        }

        public void UpGear() => ShiftGear(m_SelectedGearIndex + 1);
        public void DownGear() => ShiftGear(m_SelectedGearIndex - 1);
        public void ShiftToFirstGear() => ShiftGear(0);

        public void ShiftToReverseGear()
        {
            m_SelectedGear = m_RearGear;
            m_EventOnShiftGear?.Invoke("R");
        }
        public void ShiftToNeutral()
        {
            m_SelectedGear = 0;
            m_EventOnShiftGear?.Invoke("N");
        }

        #endregion

        public void Reset()
        {
            m_CarChassis.Reset();

            m_CarChassis.MotorTorque = 0;
            m_CarChassis.BrakeTorque = 0;
            m_CarChassis.SteerAngle = 0;

            ThrottleControl = 0;
            BrakeControl = 0;
            HandbrakeControl = 0;
            SteerControl = 0;
        }

        public void Respawn(Vector3 position, Quaternion rotation)
        {
            Reset();

            transform.position = position;
            transform.rotation = rotation;
        }

        private void OnCollisionEnter(Collision collision)
        {
            m_EventOnCollision?.Invoke(collision);
        }
    }
}
