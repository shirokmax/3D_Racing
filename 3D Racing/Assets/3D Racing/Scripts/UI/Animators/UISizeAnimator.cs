using DG.Tweening;
using UnityEngine;

namespace CarRacing
{
    public class UISizeAnimator : MonoBehaviour
    {
        [SerializeField] private float m_SizeMult = 0.5f;
        [SerializeField] private float m_Time = 0.2f;

        private Sequence m_Sequence;

        public void Animate()
        {
            if (m_Sequence.IsActive())
                m_Sequence.Kill();

            m_Sequence = DOTween.Sequence();

            transform.localScale = Vector3.one;

            m_Sequence.
                Append(transform.DOScale(Vector3.one * m_SizeMult, m_Time)).
                Insert(m_Time, transform.DOScale(Vector3.one, m_Time));
        }

        private void OnDestroy()
        {
            if (m_Sequence.IsActive())
                m_Sequence.Kill();
        }
    }
}
