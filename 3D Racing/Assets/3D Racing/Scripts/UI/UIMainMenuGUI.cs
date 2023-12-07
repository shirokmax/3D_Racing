using UnityEngine;

namespace CarRacing
{
    public class UIMainMenuGUI : MonoBehaviour
    {
        [SerializeField] private GameObject m_MainMenuCanvas;
        [SerializeField] private GameObject m_MainCanvas;
        [SerializeField] private UISelectableButtonContainer m_MenuItemsBarContainer;
        [SerializeField] private UISounds m_Sounds;

        private void Start()
        {
            m_MainCanvas.SetActive(false);
            m_MainMenuCanvas.SetActive(true);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (m_MainCanvas.activeSelf == true)
                {
                    ExitToMainMenu();
                    m_Sounds.PlaySound(SoundType.Back);
                }
            }
        }

        public void ExitToMainMenu()
        {
            m_MenuItemsBarContainer.SelectFirstButton();
            m_MainCanvas.SetActive(false);
            m_MainMenuCanvas.SetActive(true);
        }
    }
}
