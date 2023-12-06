using System;
using UnityEngine;

namespace CarRacing
{
    public class UIPausePanel : MonoBehaviour, IDependency<Pauser>
    {
        [SerializeField] private GameObject m_PausePanel;

        private Pauser m_Pauser;
        public void Construct(Pauser obj) => m_Pauser = obj;

        private void Start()
        {
            m_PausePanel.SetActive(false);

            m_Pauser.EventOnPauseStateChange += OnPauseStateChange;            
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                m_Pauser.ChangePauseState();
        }

        private void OnDestroy()
        {
            m_Pauser.EventOnPauseStateChange -= OnPauseStateChange;
        }

        public void UnPause()
        {
            m_Pauser.UnPause();
        }

        private void OnPauseStateChange(bool isPaused)
        {
            m_PausePanel.SetActive(isPaused);
        }
    }
}
