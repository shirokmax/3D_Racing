using UnityEngine;

namespace CarRacing
{
    public class MainMenuBootstrap : MonoBehaviour
    {
        [SerializeField] private SettingsLoader m_SettingsLoader;
        [SerializeField] private SpawnAllObjectsInSceneByPropertiesList m_AllObjectsSpawnByProps;
        [SerializeField] private UIButtonSound m_UIButtonSound;

        private void Awake()
        {
            m_SettingsLoader.Initialize();
            m_AllObjectsSpawnByProps.Initialize();
            m_UIButtonSound.Initialize();
        }
    }
}