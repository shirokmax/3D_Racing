using UnityEngine;

namespace UnityDrift
{
    public class CameraShootingCar : MonoBehaviour, IDependency<Car>
    {
        [SerializeField] private Camera m_Camera;
        [SerializeField] private KeyCode m_CameraEnableKey;

        private GameObject m_MainGUI;

        private Car m_Car;
        public void Construct(Car obj) => m_Car = obj;

        public void SetMainGUI(GameObject mainGUI)
        {
            m_MainGUI = mainGUI;
        }

        private void Update()
        {
            if (m_Car != null)
                transform.LookAt(m_Car.transform.position);

            if (Input.GetKeyDown(m_CameraEnableKey))
                EnableCamera();
        }

        public void EnableCamera()
        {
            m_Camera.enabled = true;
            m_MainGUI.SetActive(false);
        }

        public void DisableCamera()
        {
            m_Camera.enabled = false;
        }
    }
}
