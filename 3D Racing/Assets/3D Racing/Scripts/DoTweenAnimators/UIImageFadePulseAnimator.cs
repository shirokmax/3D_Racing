using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

namespace CarRacing
{
    [RequireComponent(typeof(Image))]
    public class UIImageFadePulseAnimator : PulseAnimator
    {
        private Image m_Image;

        private float m_StartFade;

        private void Awake()
        {
            m_Image = GetComponent<Image>();
            m_StartFade = m_Image.color.a;

            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            KillReset();
        }

        public override void Animate()
        {
            KillReset();

            m_Sequence = DOTween.Sequence();

            m_Sequence.
                Append(m_Image.DOFade(m_EndValue, m_Time).SetEase(m_InEase)).
                Insert(m_Time, m_Image.DOFade(m_StartFade, m_Time).SetEase(m_OutEase));
        }

        private void KillReset()
        {
            if (m_Sequence.IsActive())
                m_Sequence.Kill();

            m_Image.color = new Color(m_Image.color.r, m_Image.color.g, m_Image.color.b, m_StartFade);
        }
    }
}
