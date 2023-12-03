using UnityEngine;

public class SettingsLoader : MonoBehaviour
{
    [SerializeField] private Setting[] m_AllSettings;

    public void Awake()
    {
        foreach (var setting in m_AllSettings)
        {
            setting.Load();
            setting.Apply();
        }
    }
}
