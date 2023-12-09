using UnityEngine;

namespace UnityDrift
{
    [RequireComponent(typeof(Camera))]
    public class CarCameraShaker : MonoBehaviour, IDependency<Car>
    {
        [SerializeField][Range(0f, 1f)] private float m_NormalizeSpeedShake;
        [SerializeField] private float m_ShakeAmount;

        private Car m_Car;
        public void Construct(Car obj) => m_Car = obj;

        private void Update()
        {
            if (m_Car.NormalizeLinearVelocity >= m_NormalizeSpeedShake)
                transform.position += Random.insideUnitSphere * m_ShakeAmount * Time.deltaTime;
        }
    }
}