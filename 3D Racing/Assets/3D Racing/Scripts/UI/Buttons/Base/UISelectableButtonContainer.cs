using UnityEngine;

namespace UnityDrift
{
    public class UISelectableButtonContainer : MonoBehaviour
    {
        [SerializeField] private bool m_Interactable = true;
        public bool Interactable => m_Interactable;
        public void SetInteractable(bool interactable) => m_Interactable = interactable;

        private UISelectableButton[] m_Buttons;

        private int m_SelectButtonIndex = 0;

        private void Awake()
        {
            m_Buttons = transform.GetComponentsInChildren<UISelectableButton>();
            
            if (m_Buttons.Length == 0)
                Debug.LogError("Button list is empty!");
        }

        private void Start()
        {
            foreach (var button in m_Buttons)
                button.PointerEnter += OnPointerEnter;

            if (m_Interactable == true)
                m_Buttons[m_SelectButtonIndex].EnableFocuse();
        }

        private void OnDestroy()
        {
            foreach (var button in m_Buttons)
                button.PointerEnter -= OnPointerEnter;
        }

        private void OnPointerEnter(UIButton button)
        {
            SelectButton(button);
        }

        private void SelectButton(UIButton button)
        {
            if (m_Interactable == false) return;

            for (int i = 0; i < m_Buttons.Length; i++)
            {
                if (m_Buttons[i] == button)
                {
                    m_Buttons[m_SelectButtonIndex].DisableFocuse();

                    m_SelectButtonIndex = i;
                    button.EnableFocuse();
                    break;
                }
            }
        }

        public void SelectFirstButton()
        {
            SelectButton(m_Buttons[0]);
        }

        public void SelectNext() { }
        public void SelectPrevious() { }
    }
}
