using UnityEngine;

namespace CarRacing
{
    [RequireComponent(typeof(CarCameraController))]
    public abstract class CarCameraComponent : MonoBehaviour
    {
        protected Car m_Car;
        protected Camera m_Camera;

        public virtual void SetProperties(Car car, Camera camera)
        {
            m_Car = car;
            m_Camera = camera;
        }
    }
}
