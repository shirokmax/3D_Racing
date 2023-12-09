using UnityEngine;
using UnityEngine.UI;

namespace UnityDrift
{
    [RequireComponent(typeof(AudioSource))]
    public class UIBackgroundMusicInfo : MonoBehaviour
    {
        [SerializeField] private BackgroundMusic m_BGM;
        [SerializeField] private Text m_Text;
        [SerializeField] private UIRectSlideAnimator m_SlideAnimator;
        [SerializeField] private UIImageFadePulseAnimator m_FadePulseAnimator;
        [SerializeField] private AudioClip m_SwitchClipSound;

        private AudioSource m_Audio;

        private void Awake()
        {
            m_Audio = GetComponent<AudioSource>();
            m_Text.enabled = true;

            m_BGM.EventOnMusicStartPlaying += OnMusicStartPlaying;
            m_BGM.EventOnChangeSong += OnChangeSong;
        }

        private void OnDestroy()
        {
            m_BGM.EventOnMusicStartPlaying -= OnMusicStartPlaying;
            m_BGM.EventOnChangeSong -= OnChangeSong;
        }

        private void OnChangeSong()
        {
            if (m_SwitchClipSound != null)
                m_Audio.PlayOneShot(m_SwitchClipSound);
        }

        private void OnMusicStartPlaying(string clipName)
        {
            m_Text.text = clipName;
            m_SlideAnimator.Animate();
            m_FadePulseAnimator.Animate();
        }
    }
}
