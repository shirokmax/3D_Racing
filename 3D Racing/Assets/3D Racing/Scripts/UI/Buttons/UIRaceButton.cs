using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace UnityDrift
{
    public class UIRaceButton : UISelectableButton, IScriptableObjectProperty, IDependency<LoadedRaceSceneInfo>
    {
        [SerializeField] private RaceInfo m_RaceInfo;

        [SerializeField] private Image m_PreviewImage;
        [SerializeField] private Text m_Title;

        private LoadedRaceSceneInfo m_RaceSceneInfo;
        public void Construct(LoadedRaceSceneInfo obj) => m_RaceSceneInfo = obj;

        private void Start()
        {
            ApplyProperty(m_RaceInfo);
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);

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
    }
}
