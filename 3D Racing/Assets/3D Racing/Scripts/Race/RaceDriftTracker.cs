using UnityEngine;

namespace UnityDrift
{
    public class RaceDriftTracker : MonoBehaviour, IDependency<RaceStateTracker>, IDependency<Car>, IDependency<LoadedRaceSceneInfo>
    {
        public const string ROAD_TAG = "Road";
        public const float DRIFT_MIN_ANGLE = 15f;
        public const float DRIFT_MAX_ANGLE = 100f;
        public const float DRIFT_MIN_SPEED = 40f;
        public const float DRIFTPOINTS_MAX_ANGLE = 75f;
        public const float DRIFTPOINTS_MAX_SPEED = 220f;
        public const float MAX_GENERAL_MULT = 5f;

        [Header("Params")]
        [SerializeField] private float m_BaseMult = 50f;
        [SerializeField] private float m_AngleBonusMult = 1.5f;
        [SerializeField] private float m_SpeedBonusMult = 1f;
        [SerializeField] private float m_GeneralMultRate = 0.03f;
        [SerializeField] private float m_StartDriftAfterFailTime = 2f;
        [SerializeField] private float m_GeneralMultEndTime = 5f;

        private RaceStateTracker m_RaceStateTracker;
        public void Construct(RaceStateTracker obj) => m_RaceStateTracker = obj;

        private Car m_Car;
        public void Construct(Car obj) => m_Car = obj;

        private LoadedRaceSceneInfo m_LoadedRaceSceneInfo;
        public void Construct(LoadedRaceSceneInfo obj) => m_LoadedRaceSceneInfo = obj;

        private float m_CurrentPoints;
        public float CurrentPoints => m_CurrentPoints;

        private float m_TotalPoints;
        public float TotalPoints => m_TotalPoints;

        private float m_GeneralMult = 1f;
        public float GeneralMult => m_GeneralMult;

        private Timer m_DriftStartTimer;
        public bool IsDriftStartTimerFinished => m_DriftStartTimer.IsFinished;

        private Timer m_GeneralMultEndTimer;

        private WheelHit m_WheelHit;

        private WheelCollider[] WheelColliders => m_Car.WheelColliders;

        private void Awake()
        {
            m_TotalPoints = 0;
            m_CurrentPoints = 0;
            m_GeneralMult = 1f;

            InitTimers();
        }

        private void Start()
        {
            enabled = false;

            if (m_LoadedRaceSceneInfo.Info.RaceType != RaceType.Drift)
                return;

            m_RaceStateTracker.EventOnRaceStarted.AddListener(OnRaceStarted);
            m_RaceStateTracker.EventOnRaceFinished.AddListener(OnRaceFinished);
            m_Car.EventOnCollision.AddListener(OnCarCollision);
        }

        private void Update()
        {
            UpdateTimers();

            float angle = Vector3.Angle(m_Car.CarChassis.VelocityDir, m_Car.transform.forward);

            CheckDriftFail(angle);

            if (m_DriftStartTimer.IsFinished && IsOnRoad() == true)
                CheckDrift(angle);
        }

        private void CheckDriftFail(float angle)
        {
            if (m_CurrentPoints != 0 || m_GeneralMult > 1f)
            {
                if (m_GeneralMultEndTimer.IsFinished)
                {
                    Reset();
                    return;
                }

                if (angle > DRIFT_MAX_ANGLE || m_Car.LinearVelocity < DRIFT_MIN_SPEED)
                {
                    StopDrift();
                    return;
                }

                if (IsOnRoad() == false)
                {
                    StopDrift();
                    return;
                }
            }
        }

        private bool IsOnRoad()
        {
            foreach (var wheel in WheelColliders)
            {
                wheel.GetGroundHit(out m_WheelHit);

                if (m_WheelHit.collider == null) return false;

                if (m_WheelHit.collider.tag != ROAD_TAG)
                    return false;
            }

            return true;
        }

        private void CheckDrift(float angle)
        {
            if (angle >= DRIFT_MIN_ANGLE && m_Car.LinearVelocity >= DRIFT_MIN_SPEED)
            {
                CalculateDriftPoints(angle);

                m_GeneralMultEndTimer.Restart();
            }
            else
            {
                m_GeneralMult -= m_GeneralMult * m_GeneralMultRate * Time.deltaTime;
            }

            m_GeneralMult = Mathf.Clamp(m_GeneralMult, 1f, MAX_GENERAL_MULT);
        }

        private void CalculateDriftPoints(float angle)
        {
            float angleBonusNormalize = Mathf.Clamp(angle / DRIFTPOINTS_MAX_ANGLE + 1f, 1f, 2f);
            float speedBonusNormalize = Mathf.Clamp(m_Car.LinearVelocity / DRIFTPOINTS_MAX_SPEED + 1f, 1f, 2f);

            float angleBonus = angleBonusNormalize * m_AngleBonusMult;
            float speedBonus = speedBonusNormalize * m_SpeedBonusMult;

            m_GeneralMult += m_GeneralMult * m_GeneralMultRate * angleBonus * speedBonus * Time.deltaTime;
            m_GeneralMult = Mathf.Clamp(m_GeneralMult, 1f, MAX_GENERAL_MULT);

            float points = m_BaseMult * angleBonus * speedBonus * m_GeneralMult * Time.deltaTime;
            m_CurrentPoints += points;
            m_TotalPoints += points;
        }

        private void StopDrift()
        {
            Reset();
            m_DriftStartTimer.Restart();
        }

        private void OnCarCollision(Collision collision)
        {
            if (collision.transform.tag == ROAD_TAG) return;

            if (m_CurrentPoints != 0 || m_GeneralMult > 1f)
                StopDrift();
        }

        private void Reset()
        {
            m_GeneralMult = 1f;
            m_CurrentPoints = 0;
        }

        private void OnRaceStarted()
        {
            enabled = true;
        }

        private void OnRaceFinished()
        {
            enabled = false;
        }

        private void InitTimers()
        {
            m_DriftStartTimer = new Timer(m_StartDriftAfterFailTime);
            m_GeneralMultEndTimer = new Timer(m_GeneralMultEndTime);

            m_DriftStartTimer.Reset();
            m_GeneralMultEndTimer.Reset();
        }

        private void UpdateTimers()
        {
            m_DriftStartTimer.RemoveTime(Time.deltaTime);
            m_GeneralMultEndTimer.RemoveTime(Time.deltaTime);
        }
    }
}
