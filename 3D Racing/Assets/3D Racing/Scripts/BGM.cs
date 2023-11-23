using UnityEngine;

namespace CarRacing
{
    [RequireComponent(typeof(AudioSource))]
    public class BGM : MonoSingleton<BGM>, IDependency<RaceStateTracker>
    {
        private RaceStateTracker m_RaceStateTracker;
        public void Construct(RaceStateTracker obj) => m_RaceStateTracker = obj;

        private AudioSource m_Audio;

        protected override void Awake()
        {
            base.Awake();

            m_Audio = GetComponent<AudioSource>();
        }

        private void Start()
        {
            m_RaceStateTracker.EventOnPrepararionStarted.AddListener(OnPrepararionStarted);
        }

        private void OnPrepararionStarted()
        {
            m_Audio.Play();
        }
    }
}
