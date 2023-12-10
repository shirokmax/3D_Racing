using System;

using UnityEngine;

namespace UnityDrift
{
    [CreateAssetMenu]
    public class CarLightsSetting : Setting
    {
        private bool m_Neon;

        public override bool m_IsMinValue => !m_Neon;

        public override bool m_IsMaxValue => m_Neon;

        public override void SetNextValue()
        {
            m_Neon = !m_Neon;
        }

        public override void SetPreviousValue()
        {
            m_Neon = !m_Neon;
        }

        public override string GetStringValue()
        {
            return m_Neon ? "Neon" : "Default";
        }

        public override object GetValue()
        {
            return m_Neon;
        }

        public override void Apply()
        {
            Save();
        }

        public override void Load()
        {
            m_Neon = Convert.ToBoolean(PlayerPrefs.GetInt(m_Title, 1));
        }

        public override void Save()
        {
            PlayerPrefs.SetInt(m_Title, m_Neon ? 1 : 0);
        }
    }
}
