using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace UnityDrift
{
    public class UIRaceButton : UISelectableButton, IScriptableObjectProperty, IDependency<LoadedRaceInfo>
    {
        [SerializeField] private RaceInfo m_RaceInfo;
        public RaceInfo RaceInfo => m_RaceInfo;

        [SerializeField] private Image m_PreviewImage;
        [SerializeField] private Text m_Title;
        [SerializeField] private GameObject m_CompletedPanel;
        [SerializeField] private GameObject m_LockedPanel;

        private LoadedRaceInfo m_RaceSceneInfo;
        public void Construct(LoadedRaceInfo obj) => m_RaceSceneInfo = obj;

        private void Awake()
        {
            m_CompletedPanel.SetActive(false);
            m_LockedPanel.SetActive(false);

            ApplyProperty(m_RaceInfo);
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);

            if (Interactable == false) return;
            if (Clickable == false) return;

            if (m_RaceInfo == null) return;

            m_RaceSceneInfo.SetInfo(m_RaceInfo);
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

        public void SetCompleted()
        {
            m_CompletedPanel.SetActive(true);
        }

        public void SetLocked()
        {
            m_LockedPanel.SetActive(true);
            Clickable = false;
        }
    }
}
