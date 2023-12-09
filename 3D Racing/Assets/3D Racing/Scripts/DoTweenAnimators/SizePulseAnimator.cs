using UnityEngine;
using DG.Tweening;

namespace UnityDrift
{
    public class SizePulseAnimator : PulseAnimator
    {
        public override void Animate()
        {
            if (m_Sequence.IsActive())
                m_Sequence.Kill();

            m_Sequence = DOTween.Sequence();

            transform.localScale = Vector3.one;

            m_Sequence.
                Append(transform.DOScale(Vector3.one * m_EndValue, m_Time).SetEase(m_InEase)).
                Insert(m_Time, transform.DOScale(Vector3.one, m_Time).SetEase(m_OutEase));
        }
    }
}
