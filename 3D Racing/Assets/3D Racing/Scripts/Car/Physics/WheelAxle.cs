using System;
using UnityEngine;

namespace UnityDrift
{
    [Serializable]
    public class WheelAxle
    {
        [Serializable]
        private class Wheel
        {
            public WheelCollider collider;
            public Transform mesh;
        }

        #region Properties
        [SerializeField] private Wheel m_LeftWheel;
        [SerializeField] private Wheel m_RightWheel;

        [SerializeField] private bool m_IsMotor;
        public bool IsMotor => m_IsMotor;

        [SerializeField] private bool m_IsSteer;
        public bool IsSteer => m_IsSteer;

        [Space]
        [SerializeField] private float m_WheelBaseWidth;

        [Space]
        [SerializeField] private float m_AntiRollForce;
        [SerializeField] private float m_AdditionalWheelDownForce;

        [Space]
        [SerializeField] private float m_BaseForwardStiffness = 1.5f;
        [SerializeField] private float m_StabilityForwardFactor = 1.0f;

        [Space]
        [SerializeField] private float m_BaseSidewaysStiffness = 2.0f;
        [SerializeField] private float m_StabilitySidewaysFactor = 1.0f;

        public WheelCollider LeftWheel => m_LeftWheel.collider;
        public WheelCollider RightWheel => m_RightWheel.collider;

        private WheelHit m_LeftWheelHit;
        private WheelHit m_RightWheelHit;

        #endregion

        #region Public API
        public void UpdateAxle()
        {
            UpdateWheelHits();

            ApplyAntiRoll();
            CorrectStiffness();
            ApplyDownForce();

            SyncMeshTransform();
        }

        public void ConfigureVehicleSubsteps(float speedThreshold, int speedBelowThreshold, int stepsAboveThreshold)
        {
            m_LeftWheel.collider.ConfigureVehicleSubsteps(speedThreshold, speedBelowThreshold, stepsAboveThreshold);
            m_RightWheel.collider.ConfigureVehicleSubsteps(speedThreshold, speedBelowThreshold, stepsAboveThreshold);
        }

        /// <summary>
        /// Поворот колес с учетом угла Аккермана.
        /// </summary>
        /// <param name="steerAngle"></param>
        public void ApplySteerAngle(float steerAngle, float wheelBaseLength)
        {
            if (m_IsSteer == false) return;

            float radius = Mathf.Abs(wheelBaseLength * Mathf.Tan(Mathf.Deg2Rad * (90 - Mathf.Abs(steerAngle))));
            float angleSign = Mathf.Sign(steerAngle);

            if (steerAngle > 0)
            {
                m_LeftWheel.collider.steerAngle = Mathf.Rad2Deg * Mathf.Atan(wheelBaseLength / (radius + (m_WheelBaseWidth * 0.5f))) * angleSign;
                m_RightWheel.collider.steerAngle = Mathf.Rad2Deg * Mathf.Atan(wheelBaseLength / (radius - (m_WheelBaseWidth * 0.5f))) * angleSign;
            }
            else if (steerAngle < 0)
            {
                m_LeftWheel.collider.steerAngle = Mathf.Rad2Deg * Mathf.Atan(wheelBaseLength / (radius - (m_WheelBaseWidth * 0.5f))) * angleSign;
                m_RightWheel.collider.steerAngle = Mathf.Rad2Deg * Mathf.Atan(wheelBaseLength / (radius + (m_WheelBaseWidth * 0.5f))) * angleSign;
            }
            else
            {
                m_LeftWheel.collider.steerAngle = 0;
                m_RightWheel.collider.steerAngle = 0;
            }
        }

        public void ApplyMotorTorque(float motorTorque)
        {
            if (m_IsMotor == false) return;

            m_LeftWheel.collider.motorTorque = motorTorque;
            m_RightWheel.collider.motorTorque = motorTorque;
        }

        public void ApplyBrakeTorque(float brakeTorque)
        {
            if (m_IsMotor == false) return;

            m_LeftWheel.collider.brakeTorque = brakeTorque;
            m_RightWheel.collider.brakeTorque = brakeTorque;
        }

        public float GetAverageRpm()
        {
            return (m_LeftWheel.collider.rpm + m_RightWheel.collider.rpm) * 0.5f;
        }

        public float GetWheelRadius()
        {
            return m_LeftWheel.collider.radius;
        }

        #endregion

        #region Private API
        /// <summary>
        /// Стабилизатор поперечной устойчивости.
        /// </summary>
        private void ApplyAntiRoll()
        {
            float travelL = 1.0f;
            float travelR = 1.0f;

            if (m_LeftWheel.collider.isGrounded)
                travelL = (-m_LeftWheel.collider.transform.InverseTransformPoint(m_LeftWheelHit.point).y - m_LeftWheel.collider.radius) / m_LeftWheel.collider.suspensionDistance;

            if (m_RightWheel.collider.isGrounded)
                travelR = (-m_RightWheel.collider.transform.InverseTransformPoint(m_RightWheelHit.point).y - m_RightWheel.collider.radius) / m_RightWheel.collider.suspensionDistance;

            float forceDir = travelL - travelR;

            if (m_LeftWheel.collider.isGrounded)
                m_LeftWheel.collider.attachedRigidbody.AddForceAtPosition(m_LeftWheel.collider.transform.up * -forceDir * m_AntiRollForce, m_LeftWheel.collider.transform.position);

            if (m_RightWheel.collider.isGrounded)
                m_RightWheel.collider.attachedRigidbody.AddForceAtPosition(m_RightWheel.collider.transform.up * forceDir * m_AntiRollForce, m_RightWheel.collider.transform.position);
        }

        /// <summary>
        /// Корректировка общей силы трения колес.
        /// </summary>
        private void CorrectStiffness()
        {
            WheelFrictionCurve leftForward = m_LeftWheel.collider.forwardFriction;
            WheelFrictionCurve rightForward = m_RightWheel.collider.forwardFriction;

            WheelFrictionCurve leftSideways = m_LeftWheel.collider.sidewaysFriction;
            WheelFrictionCurve rightSideways = m_RightWheel.collider.sidewaysFriction;

            leftForward.stiffness = m_BaseForwardStiffness + Mathf.Abs(m_LeftWheelHit.forwardSlip) * m_StabilityForwardFactor;
            rightForward.stiffness = m_BaseForwardStiffness + Mathf.Abs(m_RightWheelHit.forwardSlip) * m_StabilityForwardFactor;

            leftSideways.stiffness = m_BaseSidewaysStiffness + Mathf.Abs(m_LeftWheelHit.sidewaysSlip) * m_StabilitySidewaysFactor;
            rightSideways.stiffness = m_BaseSidewaysStiffness + Mathf.Abs(m_RightWheelHit.sidewaysSlip) * m_StabilitySidewaysFactor;

            m_LeftWheel.collider.forwardFriction = leftForward;
            m_RightWheel.collider.forwardFriction = rightForward;

            m_LeftWheel.collider.sidewaysFriction = leftSideways;
            m_RightWheel.collider.sidewaysFriction = rightSideways;
        }

        /// <summary>
        /// Прижимная сила.
        /// </summary>
        private void ApplyDownForce()
        {
            if (m_LeftWheel.collider.isGrounded)
                m_LeftWheel.collider.attachedRigidbody.AddForceAtPosition(m_LeftWheelHit.normal * -m_AdditionalWheelDownForce * 
                    m_LeftWheel.collider.attachedRigidbody.velocity.magnitude, m_LeftWheel.collider.transform.position);

            if (m_RightWheel.collider.isGrounded)
                m_RightWheel.collider.attachedRigidbody.AddForceAtPosition(m_RightWheelHit.normal * -m_AdditionalWheelDownForce *
                    m_RightWheel.collider.attachedRigidbody.velocity.magnitude, m_RightWheel.collider.transform.position);
        }

        private void SyncMeshTransform()
        {
            UpdateWheelTransform(m_LeftWheel);
            UpdateWheelTransform(m_RightWheel);
        }

        private void UpdateWheelTransform(Wheel wheel)
        {
            Vector3 position;
            Quaternion rotation;

            wheel.collider.GetWorldPose(out position, out rotation);

            wheel.mesh.position = position;
            wheel.mesh.rotation = rotation;
        }

        private void UpdateWheelHits()
        {
            m_LeftWheel.collider.GetGroundHit(out m_LeftWheelHit);
            m_RightWheel.collider.GetGroundHit(out m_RightWheelHit);
        }

        #endregion
    }
}
