using UnityEngine;
using UnityEngine.UI;

namespace CarRacing
{
    public class UICarEngineIndicator : MonoBehaviour, IDependency<Car>
    {
        [SerializeField] private Image m_GearsFillImage;

        private Car m_Car;
        public void Construct(Car obj) => m_Car = obj;

        private void Update()
        {
            float fill = (m_Car.EngineRpm / m_Car.EngineMaxRpm - 800f / m_Car.EngineMaxRpm) * (1 + (800f / m_Car.EngineMaxRpm));
            m_GearsFillImage.fillAmount = fill;
            m_GearsFillImage.color = new Color(1f, 1f - fill, 0f, 1f);
        }
    }
}
