using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using DG.Tweening;

namespace CarRacing
{
    public class UIButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] private SoundType m_SoundType;
        public SoundType SoundType => m_SoundType;

        [Space]
        [SerializeField] private bool m_Interactable = true;
        [SerializeField] private bool m_Clickable = true;
        [SerializeField] private bool m_Hoverable = true;

        public UnityEvent OnClick;

        public event UnityAction<UIButton> PointerEnter;
        public event UnityAction<UIButton> PointerExit;
        public event UnityAction<UIButton> PointerClick;

        private bool m_Focuse;
        public bool Focuse => m_Focuse;

        public virtual void EnableFocuse()
        {
            if (m_Interactable == false) return;

            m_Focuse = true;
        }

        public virtual void DisableFocuse()
        {
            if (m_Interactable == false) return;

            m_Focuse = false;
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            if (m_Interactable == false) return;
            if (m_Hoverable == false) return;
            if (m_Focuse == true) return;

            PointerEnter?.Invoke(this);
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            if (m_Interactable == false) return;
            if (m_Hoverable == false) return;

            PointerExit?.Invoke(this);
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (m_Interactable == false) return;
            if (m_Clickable == false) return;

            OnClick?.Invoke();
            PointerClick?.Invoke(this);
        }
    }
}
