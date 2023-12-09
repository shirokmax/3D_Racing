using UnityEngine;

namespace UnityDrift
{
    public class CarCameraShaker : CarCameraComponent
    {
        [SerializeField][Range(0f, 1f)] private float m_NormalizeSpeedShake;
        [SerializeField] private float m_ShakeAmount;

        private void Update()
        {
            if (m_Car.NormalizeLinearVelocity >= m_NormalizeSpeedShake)
                transform.position += Random.insideUnitSphere * m_ShakeAmount * Time.deltaTime;
        }
    }
}