using System;
using UnityEngine;

[CreateAssetMenu]
public class FullScreenSetting : Setting
{
    private bool m_FullScreen = true;

    public override bool m_IsMinValue => !m_FullScreen;

    public override bool m_IsMaxValue => m_FullScreen;

    public override void SetNextValue()
    {
        m_FullScreen = !m_FullScreen;
    }

    public override void SetPreviousValue()
    {
        m_FullScreen = !m_FullScreen;
    }

    public override string GetStringValue()
    {
        return m_FullScreen ? "On" : "Off";
    }

    public override object GetValue()
    {
        return m_FullScreen;
    }

    public override void Apply()
    {
        Screen.fullScreen = m_FullScreen;
        Save();
    }

    public override void Load()
    {
        m_FullScreen = Convert.ToBoolean(PlayerPrefs.GetInt(m_Title, 1));
    }

    public override void Save()
    {
        PlayerPrefs.SetInt(m_Title, m_FullScreen ? 1 : 0);
    }
}
