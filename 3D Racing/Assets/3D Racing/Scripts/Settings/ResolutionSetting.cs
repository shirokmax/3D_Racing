using UnityEngine;

[CreateAssetMenu]
public class ResolutionSetting : Setting
{
    [SerializeField] private Resolution[] m_AvailableResolutions;

    private int m_CurrentResolutionIndex;

    public override bool m_IsMinValue => m_CurrentResolutionIndex == 0;

    public override bool m_IsMaxValue => m_CurrentResolutionIndex == m_AvailableResolutions.Length - 1;

    public override void SetNextValue()
    {
        if (m_IsMaxValue == false)
            m_CurrentResolutionIndex++;
    }

    public override void SetPreviousValue()
    {
        if (m_IsMinValue == false)
            m_CurrentResolutionIndex--;
    }

    public override string GetStringValue()
    {
        if (Application.isPlaying)
            return m_AvailableResolutions[m_CurrentResolutionIndex].width + "x" + m_AvailableResolutions[m_CurrentResolutionIndex].height;

        return string.Empty;
    }

    public override object GetValue()
    {
        return m_AvailableResolutions[m_CurrentResolutionIndex];
    }

    public override void Apply()
    {
        Screen.SetResolution(m_AvailableResolutions[m_CurrentResolutionIndex].width, m_AvailableResolutions[m_CurrentResolutionIndex].height, Screen.fullScreen);
        Save();
    }

    public override void Load()
    {
        InitializeAvailableResolutions();

        m_CurrentResolutionIndex = PlayerPrefs.GetInt(m_Title, GetCurrentResolutionIndex());
    }

    private void Save()
    {
        PlayerPrefs.SetInt(m_Title, m_CurrentResolutionIndex);
    }

    private void InitializeAvailableResolutions()
    {
        m_AvailableResolutions = Screen.resolutions;
    }

    private int GetCurrentResolutionIndex()
    {
        for (int i = 0; i < m_AvailableResolutions.Length; i++)
        {
            if (m_AvailableResolutions[i].width == Screen.currentResolution.width &&
                m_AvailableResolutions[i].height == Screen.currentResolution.height)
            {
                return i;
            }
        }

        return 0;
    }
}
