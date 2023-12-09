using UnityEngine;
using UnityEngine.UI;

namespace UnityDrift
{
    public class UISettingButton : UISelectableButton, IScriptableObjectProperty
    {
        [SerializeField] private Setting m_Setting;

        [Space]
        [SerializeField] private Text m_TitleText;
        [SerializeField] private Text m_ValueText;
        [SerializeField] private Image m_PreviousButtonImage;
        [SerializeField] private Image m_NextButtonImage;

        private void Start()
        {
            ApplyProperty(m_Setting);
        }

        public void ApplyProperty(ScriptableObject property)
        {
            if (property == null) return;

            if (property is Setting == false) return;

            m_Setting = property as Setting;

            UpdateInfo();
        }

        public void SetNextValueSetting()
        {
            m_Setting?.SetNextValue();
            m_Setting.Apply();
            UpdateInfo();
        }
        public void SetPreviousValueSetting()
        {
            m_Setting?.SetPreviousValue();
            m_Setting.Apply();
            UpdateInfo();
        }

        private void UpdateInfo()
        {
            m_TitleText.text = m_Setting.Title;
            m_ValueText.text = m_Setting.GetStringValue();

            m_PreviousButtonImage.enabled = !m_Setting.m_IsMinValue;
            m_NextButtonImage.enabled = !m_Setting.m_IsMaxValue;
        }
    }
}
