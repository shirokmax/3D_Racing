using UnityEngine;

namespace UnityDrift
{
    [CreateAssetMenu]
    public class CarSelectionSetting : Setting
    {
        [SerializeField] private Car[] m_CarPrefabs;
        public Car[] CarPrefabs => m_CarPrefabs;

        private int m_CurrentCarIndex;
        public int CurrentCarIndex => m_CurrentCarIndex;

        public override bool m_IsMinValue => m_CurrentCarIndex == 0;

        public override bool m_IsMaxValue => m_CurrentCarIndex == m_CarPrefabs.Length - 1;

        public override void SetNextValue()
        {
            if (m_IsMaxValue == false)
                m_CurrentCarIndex++;
        }

        public override void SetPreviousValue()
        {
            if (m_IsMinValue == false)
                m_CurrentCarIndex--;
        }

        public override string GetStringValue()
        {
            return m_CarPrefabs[m_CurrentCarIndex].CarName;
        }

        public override object GetValue()
        {
            return m_CarPrefabs[m_CurrentCarIndex];
        }

        public override void Apply()
        {
            Save();
        }

        public override void Load()
        {
            PlayerPrefs.GetInt(m_Title, 0);
        }

        public override void Save()
        {
            PlayerPrefs.SetInt(m_Title, m_CurrentCarIndex);
        }
    }
}
