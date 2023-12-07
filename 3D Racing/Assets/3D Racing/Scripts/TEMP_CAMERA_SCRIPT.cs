using UnityEngine;

namespace CarRacing
{
    public class TEMP_CAMERA_SCRIPT : MonoBehaviour, IDependency<Car>
    {
        [SerializeField] private Camera m_Camera;
        [SerializeField] private GameObject m_MainGUI;

        [Space]
        [SerializeField] private KeyCode m_CameraEnableKey;
        [SerializeField] private KeyCode m_CameraDisableKey;

        private Car m_Car;
        public void Construct(Car obj) => m_Car = obj;

        void Update()
        {
            transform.LookAt(m_Car.transform.position);

            if (Input.GetKeyDown(m_CameraDisableKey))
            {
                m_Camera.enabled = false;
                m_MainGUI.SetActive(true);
            }

            if (Input.GetKeyDown(m_CameraEnableKey))
            {
                m_Camera.enabled = true;
                m_MainGUI.SetActive(false);
            }
        }
    }
}
