using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CarRacing
{
    public class UIRaceButton : UISelectableButton
    {
        [SerializeField] private RaceInfo m_RaceInfo;

        [SerializeField] private Image m_PreviewImage;
        [SerializeField] private Text m_Title;

        private void Start()
        {
            ApplyProperties(m_RaceInfo);
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);

            if (m_RaceInfo == null) return;

            SceneManager.LoadScene(m_RaceInfo.SceneName);
        }

        public void ApplyProperties(RaceInfo raceInfo)
        {
            if (raceInfo == null) return;

            m_RaceInfo = raceInfo;

            m_PreviewImage.sprite = m_RaceInfo.PreviewSprite;
            m_Title.text = m_RaceInfo.Title;
        }
    }
}
