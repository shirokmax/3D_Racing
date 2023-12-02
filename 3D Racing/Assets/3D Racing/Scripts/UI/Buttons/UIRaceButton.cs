using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CarRacing
{
    public class UIRaceButton : UISelectableButton, IScriptableObjectProperty
    {
        [SerializeField] private RaceInfo m_RaceInfo;

        [SerializeField] private Image m_PreviewImage;
        [SerializeField] private Text m_Title;

        private void Start()
        {
            ApplyProperty(m_RaceInfo);
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);

            if (m_RaceInfo == null) return;

            SceneManager.LoadScene(m_RaceInfo.SceneName);
        }

        public void ApplyProperty(ScriptableObject property)
        {
            if (property == null) return;

            if (property is RaceInfo == false) return;

            m_RaceInfo = property as RaceInfo;

            m_PreviewImage.sprite = m_RaceInfo.PreviewSprite;
            m_Title.text = m_RaceInfo.Title;
        }
    }
}
