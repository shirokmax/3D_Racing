using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace CarRacing
{
    [RequireComponent(typeof(Text))]
    public class UITextFadePulsatingAnimator : PulsatingAnimator
    {
        private Text m_Text;

        private void Awake()
        {
            m_Text = GetComponent<Text>();
        }

        public override void Animate()
        {
            m_Sequence = DOTween.Sequence();

            m_Sequence.
                Append(m_Text.DOFade(m_EndValue, m_Time).SetEase(m_InEase)).
                Insert(m_Time, m_Text.DOFade(1f, m_Time).SetEase(m_OutEase)).
                OnComplete(Animate);
        }
    }
}
