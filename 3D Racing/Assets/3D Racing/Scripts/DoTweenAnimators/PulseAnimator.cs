using DG.Tweening;
using UnityEngine;

public abstract class PulseAnimator : MonoBehaviour
{
    [SerializeField] protected float m_EndValue = 0.5f;
    [SerializeField] protected float m_Time = 0.2f;

    [Space]
    [SerializeField] protected Ease m_InEase = Ease.Linear;
    [SerializeField] protected Ease m_OutEase = Ease.Linear;

    protected Sequence m_Sequence;

    public abstract void Animate();

    protected virtual void OnDestroy()
    {
        if (m_Sequence.IsActive())
            m_Sequence.Kill();
    }
}
