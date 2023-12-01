using UnityEngine;

public class SettingsLoader : MonoBehaviour
{
    [SerializeField] private Setting[] m_AllSettings;

    private void Awake()
    {
        foreach(var setting in m_AllSettings)
        {
            setting.Load();
            setting.Apply();
        }
    }
}
