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
                if (m_MainCanvas.activeSelf == true && CloseLevelPanels() == false)
                {
                    ExitToMainMenu();
                    m_Sounds.PlaySound(SoundType.Back);
                }
            }

#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.F5))
                PlayerPrefs.DeleteAll();
#endif
        }

        public void ExitToMainMenu()
        {
            m_MenuItemsBarContainer.SelectFirstButton();
            m_MainCanvas.SetActive(false);
            m_MainMenuCanvas.SetActive(true);
        }

        private bool CloseLevelPanels()
        {
            bool wasClosed = false;

            foreach (var panel in m_LevelPanels)
            {
                if (panel.activeSelf == true)
                {
                    panel.SetActive(false);
                    wasClosed = true;
                }
            }

            if (wasClosed)
            {
                m_MenuItemsBarContainer.SetInteractable(true);
                m_Sounds.PlaySound(SoundType.Back);
            }

            return wasClosed;
        }
    }
}
