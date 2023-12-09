using UnityEngine;

namespace UnityDrift
{
    [RequireComponent(typeof(AudioSource))]
    public class GearBoxSound : MonoBehaviour, IDependency<Car>
    {
        private AudioSource m_AudioSource;

        private Car m_Car;
        public void Construct(Car obj) => m_Car = obj;

        private void Awake()
        {
            m_AudioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            m_Car.EventOnShiftGear.AddListener(OnShiftGear);
        }

        private void OnShiftGear(string gearName)
        {
            m_AudioSource.Play();
        }
    }
}
