using System;
using UnityEngine;

namespace UnityDrift
{
    public class SwitchCarLights : MonoBehaviour, IDependency<LoadedRaceSceneInfo>
    {
        [SerializeField] private CarLightsSetting m_CarLightsSettings;
        [SerializeField] private GameObject m_DefaultLight;
        [SerializeField] private GameObject m_NeonLight;

        private bool m_Neon;

        private LoadedRaceSceneInfo m_LoadedSceneInfo;
        public void Construct(LoadedRaceSceneInfo obj) => m_LoadedSceneInfo = obj;

        private void Start()
        {
            m_Neon = Convert.ToBoolean(PlayerPrefs.GetInt(m_CarLightsSettings.Title, 0));

            m_DefaultLight.SetActive(false);
            m_NeonLight.SetActive(false);

            if (m_LoadedSceneInfo.Info?.Night == true)
                SwitchLights();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
                SwitchLights();
        }

        private void SwitchLights()
        {
            if (m_Neon == true)
                m_NeonLight.SetActive(!m_NeonLight.activeSelf);
            else
                m_DefaultLight.SetActive(!m_DefaultLight.activeSelf);
        }
    }
}
