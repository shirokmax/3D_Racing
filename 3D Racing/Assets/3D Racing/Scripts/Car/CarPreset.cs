using UnityEngine;

namespace UnityDrift
{
    [CreateAssetMenu]
    public class CarPreset : ScriptableObject
    {
        [SerializeField] private CarPresetType m_Type;
        public CarPresetType Type => m_Type;

        [Header("Car Chassis")]
        [SerializeField] private WheelAxle[] m_WheelAxles;
        public WheelAxle[] WheelAxles => m_WheelAxles;

        [SerializeField] private Vector3 m_CenterOfMassPosition;
        public Vector3 CenterOfMassPosition => m_CenterOfMassPosition;

        [Space]
        [SerializeField] private float m_DownForceMin;
        public float DownForceMin => m_DownForceMin;

        [SerializeField] private float m_DownForceMax;
        public float DownForceMax => m_DownForceMax;

        [SerializeField] private float m_DownForceFactor;
        public float DownForceFactor => m_DownForceFactor;

        [Space]
        [SerializeField] private float m_AngularDragMin;
        public float AngularDragMin => m_AngularDragMin;

        [SerializeField] private float m_AngularDragMax;
        public float AngularDragMax => m_AngularDragMax;

        [SerializeField] private float m_AngularDragFactor;
        public float AngularDragFactor => m_AngularDragFactor;

        [Header("Car")]
        [SerializeField] private float m_MaxSpeed;
        public float MaxSpeed => m_MaxSpeed;

        [SerializeField] private float m_MaxSteerAngle;
        public float MaxSteerAngle => m_MaxSteerAngle;

        [SerializeField] private float m_MaxBrakeTorque;
        public float MaxBrakeTorque => m_MaxBrakeTorque;

        [SerializeField] private float m_MaxHandbrakeTorque;
        public float MaxHandbrakeTorque => m_MaxHandbrakeTorque;

        [Space]
        [SerializeField] private float m_EngineMaxTorque;
        public float EngineMaxTorque => m_EngineMaxTorque;

        [SerializeField] private float m_EngineMinRpm;
        public float EngineMinRpm => m_EngineMinRpm;

        [SerializeField] private float m_EngineMaxRpm;
        public float EngineMaxRpm => m_EngineMaxRpm;

        [Space]
        [SerializeField] private float m_UpShiftEngineRpm;
        public float UpShiftEngineRpm => m_UpShiftEngineRpm;

        [SerializeField] private float m_DownShiftEngineRpm;
        public float DownShiftEngineRpm => m_DownShiftEngineRpm;
    }
}
