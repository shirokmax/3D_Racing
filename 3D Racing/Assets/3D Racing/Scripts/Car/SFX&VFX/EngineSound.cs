using UnityEngine;

namespace UnityDrift
{
    [RequireComponent(typeof(AudioSource))]
    public class EngineSound : MonoBehaviour, IDependency<Car>
    {
        [Space]
        [SerializeField] private float m_PitchModifier;
        [SerializeField] private float m_VolumeModifier;
        [SerializeField] private float m_RpmModifier;

        [Space]
        [SerializeField] private float m_BasePitch;
        [SerializeField] private float m_BaseVolume;

        private AudioSource m_AudioSource;

        private Car m_Car;
        public void Construct(Car obj) => m_Car = obj;

        private void Awake()
        {
            m_AudioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            m_AudioSource.pitch = m_BasePitch + m_PitchModifier * (m_Car.EngineRpm / m_Car.EngineMaxRpm) * m_RpmModifier;
            m_AudioSource.volume = m_BaseVolume + m_VolumeModifier * (m_Car.EngineRpm / m_Car.EngineMaxRpm);
        }
    }
}
