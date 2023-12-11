using UnityEngine;

namespace UnityDrift
{
    public class UIRaceButtonsDisplayController : MonoBehaviour, IDependency<RaceCompletion>
    {
        private RaceCompletion m_RaceCompletion;
        public void Construct(RaceCompletion obj) => m_RaceCompletion = obj;

        private UIRaceButton[] m_RaceButtons;

        private void Awake()
        {
            m_RaceButtons = transform.GetComponentsInChildren<UIRaceButton>();

            if (m_RaceButtons.Length == 0)
                Debug.LogError("Button list is empty!");
        }

        private void Start()
        {
            int drawButton = 0;
            bool completed = true;

            while (completed == true && drawButton < m_RaceButtons.Length)
            {
                if (m_RaceButtons[drawButton].RaceInfo.alwaysUnlocked == true)
                    completed = true;
                else
                    completed = m_RaceCompletion.GetCompletion(m_RaceButtons[drawButton].RaceInfo);

                drawButton++;
            }

            for (int i = 0; i < drawButton - 1; i++)
            {
                if (m_RaceButtons[i].RaceInfo.alwaysUnlocked == false)
                    m_RaceButtons[i].SetCompleted();
            }

            if (m_RaceCompletion.GetCompletion(m_RaceButtons[drawButton - 1].RaceInfo) == true)
                m_RaceButtons[drawButton - 1].SetCompleted();


            for (int i = drawButton; i < m_RaceButtons.Length; i++)
            {
                if (m_RaceButtons[i].RaceInfo.alwaysUnlocked == false)
                    m_RaceButtons[i].SetLocked();
            }                 
        }
    }
}
