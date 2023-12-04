using UnityEngine;

namespace CarRacing
{
    public enum CarLightType
    {
        Default,
        Neon
    }

    public class SwitchCarLightsTemp : MonoBehaviour
    {
        [SerializeField] private GameObject m_DefaultLight;
        [SerializeField] private GameObject m_NeonLight;

        [SerializeField] private CarLightType m_Type;
        public CarLightType Type => m_Type;
        public void SetLightType(CarLightType type) => m_Type = type;

        private void Update()
        {
            SwitchLight();
        }

        private void SwitchLight()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (m_Type == CarLightType.Default)
                {
                    m_DefaultLight.SetActive(!m_DefaultLight.activeSelf);
                    m_NeonLight.SetActive(false);
                }

                if (m_Type == CarLightType.Neon)
                {
                    m_NeonLight.SetActive(!m_NeonLight.activeSelf);
                    m_DefaultLight.SetActive(false);
                }
            }
        }
    }
}
