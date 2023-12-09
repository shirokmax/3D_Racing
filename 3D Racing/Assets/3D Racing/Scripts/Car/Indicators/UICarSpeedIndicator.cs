using UnityEngine;
using UnityEngine.UI;

namespace UnityDrift
{
    public class UICarSpeedIndicator : MonoBehaviour, IDependency<Car>
    {
        [SerializeField] private Text m_SpeedText;

        private Car m_Car;
        public void Construct(Car obj) => m_Car = obj;

        private void Update()
        {
            m_SpeedText.text = m_Car.Speed.ToString("F0");
        }
    }
}
