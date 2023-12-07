using UnityEngine;

namespace CarRacing
{
    [RequireComponent(typeof(AudioSource))]
    public class UISounds : MonoBehaviour, IDependency<Pauser>
    {
        [SerializeField] private AudioClip m_Click;
        [SerializeField] private AudioClip m_Play;
        [SerializeField] private AudioClip m_Back;
        [SerializeField] private AudioClip m_Hover;
        [SerializeField] private AudioClip m_Pause;

        private AudioSource m_Audio;

        private UIButton[] m_Buttons;

        private Pauser m_Pauser;
        public void Construct(Pauser obj) => m_Pauser = obj;

        public void Awake()
        {
            m_Audio = GetComponent<AudioSource>();
            m_Audio.ignoreListenerPause = true;
        }

        private void Start()
        {            
            m_Buttons = GetComponentsInChildren<UIButton>(true);

            for (int i = 0; i < m_Buttons.Length; i++)
            {
                m_Buttons[i].PointerEnter += OnPointerEnter;
                m_Buttons[i].PointerClick += OnPointerClick;
            }

            m_Pauser.EventOnPauseStateChange += OnPauseStateChange;
        }

        private void OnDestroy()
        {
            for (int i = 0; i < m_Buttons.Length; i++)
            {
                m_Buttons[i].PointerEnter -= OnPointerEnter;
                m_Buttons[i].PointerClick -= OnPointerClick;
            }

            m_Pauser.EventOnPauseStateChange -= OnPauseStateChange;
        }

        private void OnPointerEnter(UIButton button)
        {
            m_Audio.PlayOneShot(m_Hover);
        }

        private void OnPointerClick(UIButton button)
        {
            switch (button.Type)
            {
                case ButtonType.Default:
                    m_Audio.PlayOneShot(m_Click);
                    break;
                case ButtonType.Play:
                    m_Audio.PlayOneShot(m_Play);
                    break;
                case ButtonType.Back:
                    m_Audio.PlayOneShot(m_Back);
                    break;
            }
        }

        private void OnPauseStateChange(bool isPaused)
        {
            if (isPaused == true)
                m_Audio.PlayOneShot(m_Pause);
            else
                m_Audio.PlayOneShot(m_Back);
        }
    }
}
