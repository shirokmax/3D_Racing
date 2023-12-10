using UnityEngine;

namespace UnityDrift
{
    [CreateAssetMenu]
    public class CarPresetSetting : Setting
    {
        private CarPresetType m_PresetType;
        public CarPresetType PresetType => m_PresetType;

        public override bool m_IsMinValue => m_PresetType == 0;

        public override bool m_IsMaxValue => m_PresetType.Next() == 0;

        public override void SetNextValue()
        {
            if (m_IsMaxValue == false)
                m_PresetType = m_PresetType.Next();
        }

        public override void SetPreviousValue()
        {
            if (m_IsMinValue == false)
                m_PresetType = m_PresetType.Previous();
        }

        public override string GetStringValue()
        {
            return m_PresetType.ToString();
        }

        public override object GetValue()
        {
            return m_PresetType;
        }

        public override void Apply()
        {
            Save();
        }

        public override void Load()
        {
            m_PresetType = (CarPresetType)PlayerPrefs.GetInt(m_Title, (int)(CarPresetType.Drift));
        }

        public override void Save()
        {
            PlayerPrefs.SetInt(m_Title, (int)m_PresetType);
        }
    }
}
