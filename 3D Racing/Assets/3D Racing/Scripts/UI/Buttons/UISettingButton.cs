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
        [SerializeField] private UIButton m_PreviousButton;
        [SerializeField] private UIButton m_NextButton;

        [Space]
        [SerializeField] private Color m_ValueButtonsStartColor;
        [SerializeField] private Color m_ValueButtonsDisabledColor;

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

            if (m_Setting.m_IsMinValue == true)
            {
                m_PreviousButton.Interactable = false;
                m_PreviousButton.Image.color = m_ValueButtonsDisabledColor;
            }
            else
            {
                m_PreviousButton.Interactable = true;
                m_PreviousButton.Image.color = m_ValueButtonsStartColor;
            }

            if (m_Setting.m_IsMaxValue == true)
            {
                m_NextButton.Interactable= false;
                m_NextButton.Image.color = m_ValueButtonsDisabledColor;
            }
            else
            {
                m_NextButton.Interactable = true;
                m_NextButton.Image.color = m_ValueButtonsStartColor;
            }
        }
    }
}
