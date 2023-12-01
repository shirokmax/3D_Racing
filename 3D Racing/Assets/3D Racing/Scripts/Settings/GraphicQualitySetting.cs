using UnityEngine;

[CreateAssetMenu]
public class GraphicQualitySetting : Setting
{
    private int m_CurrentLevelIndex;
    public override bool m_IsMinValue => m_CurrentLevelIndex == 0;
    public override bool m_IsMaxValue => m_CurrentLevelIndex == QualitySettings.names.Length - 1;

    public override void SetNextValue()
    {
        if (m_IsMaxValue == false)
            m_CurrentLevelIndex++;
    }

    public override void SetPreviousValue()
    {
        if (m_IsMinValue == false)
            m_CurrentLevelIndex--;
    }

    public override string GetStringValue()
    {
        return QualitySettings.names[m_CurrentLevelIndex];
    }

    public override object GetValue()
    {
        return QualitySettings.names[m_CurrentLevelIndex];
    }

    public override void Apply()
    {
        QualitySettings.SetQualityLevel(m_CurrentLevelIndex);
        Save();
    }

    public override void Load()
    {
        m_CurrentLevelIndex = PlayerPrefs.GetInt(m_Title, QualitySettings.names.Length - 1);
    }

    private void Save()
    {
        PlayerPrefs.SetInt(m_Title, m_CurrentLevelIndex);
    }
}
