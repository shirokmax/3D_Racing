using UnityEngine;
using DG.Tweening;

namespace CarRacing
{
    public class SizePulsatingAnimator : PulsatingAnimator
    {
        public override void Animate()
        {
            m_Sequence = DOTween.Sequence();

            m_Sequence.
                Append(transform.DOScale(Vector3.one * m_EndValue, m_Time).SetEase(m_InEase)).
                Insert(m_Time, transform.DOScale(Vector3.one, m_Time).SetEase(m_OutEase)).
                OnComplete(Animate);
        }
    }
}
