using UnityEngine;

public class SettingsLoader : MonoBehaviour
{
    [SerializeField] private Setting[] m_AllSettings;

    private static SettingsLoader m_Instance;

    private void Awake()
    {
        if (m_Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        m_Instance = this;

        foreach (var setting in m_AllSettings)
        {
            setting.Load();
            setting.Apply();
        }
    }
}
