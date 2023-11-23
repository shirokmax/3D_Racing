using UnityEngine;
using UnityEngine.UI;

namespace CarRacing
{
    public class UICarGearboxIndicator : MonoBehaviour, IDependency<Car>
    {
        [SerializeField] private Text m_GearText;

        private Car m_Car;
        public void Construct(Car obj) => m_Car = obj;

        private void Start()
        {
            m_Car.EventOnShiftGear.AddListener(OnShiftGear);
        }

        private void OnShiftGear(string gearName)
        {
            m_GearText.text = gearName;
        }
    }
}
