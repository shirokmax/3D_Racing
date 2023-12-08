using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

namespace CarRacing
{
    [RequireComponent(typeof(RectTransform))]
    public class UIRectSlideAnimator : MonoBehaviour
    {
        [SerializeField] private Vector2 m_EndPosition;
        [SerializeField] private float m_DurationTime;
        [SerializeField] private Ease m_Ease;

        [Space]
        [SerializeField] private bool m_AnimateOnEnable;

        private RectTransform m_Rect;
        private Vector2 m_StartPosition;

        private Tween m_TweenLink;

        private void Awake()
        {
            m_Rect = GetComponent<RectTransform>();

            m_StartPosition = m_Rect.anchoredPosition;

            UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode arg1)
        {
            if (scene.name == GlobalConsts.MAIN_MENU_SCENE_NAME)
                return;

            KillReset();
        }

        public void Animate()
        {
            KillReset();

            m_TweenLink = m_Rect.DOAnchorPos(m_EndPosition, m_DurationTime).SetEase(m_Ease);
        }

        private void OnEnable()
        {
            if (m_AnimateOnEnable == false) 
                return;

            Animate();
        }

        private void OnDestroy()
        {
            UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;

            KillReset();
        }

        private void KillReset()
        {
            if (m_TweenLink.IsActive())
                m_TweenLink.Kill();

            m_Rect.anchoredPosition = m_StartPosition;
        }
    }
}