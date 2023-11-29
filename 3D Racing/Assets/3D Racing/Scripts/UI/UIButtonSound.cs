using UnityEngine;

namespace CarRacing
{
    [RequireComponent(typeof(AudioSource))]
    public class UIButtonSound : MonoBehaviour
    {
        [SerializeField] private AudioClip m_Click;
        [SerializeField] private AudioClip m_Play;
        [SerializeField] private AudioClip m_Back;
        [SerializeField] private AudioClip m_Hover;

        private AudioSource m_Audio;

        private UIButton[] m_Buttons;

        private void Awake()
        {
            m_Audio = GetComponent<AudioSource>();

            m_Buttons = GetComponentsInChildren<UIButton>(true);
        }

        private void Start()
        {
            for (int i = 0; i < m_Buttons.Length; i++)
            {
                m_Buttons[i].PointerEnter += OnPointerEnter;
                m_Buttons[i].PointerClick += OnPointerClick;
            }
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
                default:
                    m_Audio.PlayOneShot(m_Click);
                    break;
            }
        }
    }
}
