using DG.Tweening;
using UnityEngine;

namespace CarRacing
{
    [RequireComponent(typeof(RectTransform))]
    public class UISlideAnimator : MonoBehaviour
    {
        [SerializeField] private Vector2 m_HiddenPosition;
        [SerializeField] private float m_DurationTime;
        [SerializeField] private Ease m_Ease;

        private RectTransform m_Rect;
        private Vector2 m_EnabledPosition;

        private Tween m_TweenLink;

        private void Awake()
        {
            m_Rect = GetComponent<RectTransform>();
            m_EnabledPosition = m_Rect.anchoredPosition;
        }

        private void OnEnable()
        {
            if (m_TweenLink != null)
            {
                m_TweenLink.Kill();
                m_TweenLink = null;
            }

            ResetPosition();
            m_TweenLink = m_Rect.DOAnchorPos(m_EnabledPosition, m_DurationTime).SetEase(m_Ease);
        }

        private void ResetPosition()
        {
            m_Rect.anchoredPosition = m_HiddenPosition;
        }
    }
}
