using UnityEngine;

namespace UnityDrift
{
    public class UIMainMenuGUI : MonoBehaviour
    {
        [SerializeField] private GameObject m_MainMenuCanvas;
        [SerializeField] private GameObject m_MainCanvas;
        [SerializeField] private UISelectableButtonContainer m_MenuItemsBarContainer;
        [SerializeField] private UISounds m_Sounds;

        [SerializeField] private GameObject[] m_LevelPanels;

        private void Start()
        {
            m_MainCanvas.SetActive(false);
            m_MainMenuCanvas.SetActive(true);

            foreach (var panel in m_LevelPanels)
                panel.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (m_MainCanvas.activeSelf == true && IsLevelPanelsClosed() == true)
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

        private bool IsLevelPanelsClosed()
        {
            foreach (var panel in m_LevelPanels)
            {
                if (panel.activeSelf == true)
                {
                    panel.SetActive(false);
                    m_Sounds.PlaySound(SoundType.Back);

                    return false;
                }
            }

            return true;
        }
    }
}
