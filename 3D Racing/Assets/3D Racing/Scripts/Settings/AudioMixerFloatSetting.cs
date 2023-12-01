using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu]
public class AudioMixerFloatSetting : Setting
{
    [SerializeField] AudioMixer m_AudioMixer;
    [SerializeField] private string m_ParameterName;

    [SerializeField] private float m_MinRealValue;
    [SerializeField] private float m_MaxRealValue;

    [Space]
    [SerializeField] private float m_DisplayStep;
    [SerializeField] private float m_MinDisplayValue;
    [SerializeField] private float m_MaxDisplayValue;
    
    private float m_CurrentRealValue;

    public override bool m_IsMinValue => m_CurrentRealValue == m_MinRealValue;

    public override bool m_IsMaxValue => m_CurrentRealValue == m_MaxRealValue;

    public override void SetNextValue()
    {
        float displayStepNormalize = Mathf.Abs(m_MaxDisplayValue - m_MinDisplayValue) / m_DisplayStep;

        AddValue(Mathf.Abs(m_MaxRealValue - m_MinRealValue) / displayStepNormalize);
    }

    public override void SetPreviousValue()
    {
        float displayStepNormalize = Mathf.Abs(m_MaxDisplayValue - m_MinDisplayValue) / m_DisplayStep;

        AddValue(-Mathf.Abs(m_MaxRealValue - m_MinRealValue) / displayStepNormalize);
    }

    public override string GetStringValue()
    {
        float t = (m_CurrentRealValue - m_MinRealValue) / (m_MaxRealValue - m_MinRealValue);

        return Mathf.Lerp(m_MinDisplayValue, m_MaxDisplayValue, t).ToString();
    }

    public override object GetValue()
    {
        return m_CurrentRealValue;
    }

    private void AddValue(float value)
    {
        m_CurrentRealValue += value;
        m_CurrentRealValue = Mathf.Clamp(m_CurrentRealValue, m_MinRealValue, m_MaxRealValue);
    }

    public override void Apply()
    {
        m_AudioMixer.SetFloat(m_ParameterName, m_CurrentRealValue);
    }
}
