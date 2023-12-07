using DG.Tweening;

public abstract class PulsatingAnimator : PulseAnimator
{
    protected void Start()
    {
        m_Sequence = DOTween.Sequence();

        Animate();
    }
}
