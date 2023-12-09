using UnityEngine;

namespace UnityDrift
{
    public class CarCameraFovCorrector : CarCameraComponent
    {
        [Space]
        [SerializeField] private float m_MinFieldOfView;
        [SerializeField] private float m_MaxFieldOfView;

        private void Update()
        {
            m_Camera.fieldOfView = Mathf.Lerp(m_MinFieldOfView, m_MaxFieldOfView, m_Car.NormalizeLinearVelocity);
        }
    }
}
