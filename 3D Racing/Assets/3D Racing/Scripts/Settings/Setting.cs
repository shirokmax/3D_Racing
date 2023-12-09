using UnityEngine;

public abstract class Setting : ScriptableObject
{
    [SerializeField] protected string m_Title;
    public string Title => m_Title;

    public abstract bool m_IsMinValue { get; }
    public abstract bool m_IsMaxValue { get; }

    public abstract void SetNextValue();
    public abstract void SetPreviousValue();
    public abstract object GetValue();
    public abstract string GetStringValue();
    public abstract void Apply();
    public abstract void Load();
    public abstract void Save();
}
