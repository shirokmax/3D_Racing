using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UnityDrift
{
    public class UIButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] private Image m_Image;
        public Image Image => m_Image;

        [SerializeField] private SoundType m_SoundType;
        public SoundType SoundType => m_SoundType;

        [Space]
        [SerializeField] private bool m_Interactable = true;
        public bool Interactable 
        {
            get => m_Interactable;
            set => m_Interactable = value;
        }

        [SerializeField] private bool m_Clickable = true;
        public bool Clickable
        {
            get => m_Clickable;
            set => m_Clickable = value;
        }

        [SerializeField] private bool m_Hoverable = true;
        public bool Hoverable
        {
            get => m_Hoverable;
            set => m_Hoverable = value;
        }

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
