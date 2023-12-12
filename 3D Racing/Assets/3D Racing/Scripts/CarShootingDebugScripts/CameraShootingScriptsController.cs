using UnityEngine;

namespace UnityDrift
{
    public class CameraShootingScriptsController : MonoBehaviour
    {
        [SerializeField] private GameObject m_RaceGUI;
        [SerializeField] private KeyCode m_DisableAllCamerasKey;

        private CameraShootingCar[] m_Cameras;

        private void Awake()
        {
            m_Cameras = FindObjectsOfType<CameraShootingCar>();
        }

        private void Start()
        {
            foreach (var camera in m_Cameras)
            {
                camera.SetMainGUI(m_RaceGUI);
                camera.DisableCamera();
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(m_DisableAllCamerasKey))
                DisableAllCameras();
        }

        private void DisableAllCameras()
        {
            foreach (var camera in m_Cameras)
                camera.DisableCamera();

            m_RaceGUI.SetActive(true);
        }
    }
}
