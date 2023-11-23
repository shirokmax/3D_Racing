using UnityEngine;

namespace CarRacing
{
    public class CameraPathFollower : CarCameraComponent
    {
        [SerializeField] private Transform m_LookTarget;
        [SerializeField] private float m_Height;
        [SerializeField] private float m_MovementSpeed;

        [SerializeField] private float m_MinDistance;
        [SerializeField] private float m_MaxDistance;
        [SerializeField] private float m_DistanceChangeSpeed;

        private float m_Distance;
        private bool m_DistanceChangeFlag = true;

        private void Start()
        {
            m_Distance = (m_MinDistance + m_MaxDistance) / 2;
        }

        private void FixedUpdate()
        {          
            transform.position += transform.right * m_MovementSpeed * Time.fixedDeltaTime;
            
            if (m_DistanceChangeFlag == true)
            {
                m_Distance += m_DistanceChangeSpeed * Time.fixedDeltaTime;

                if (m_Distance > m_MaxDistance)
                    m_DistanceChangeFlag = false;
            }

            if (m_DistanceChangeFlag == false)
            {
                m_Distance -= m_DistanceChangeSpeed * Time.fixedDeltaTime;

                if (m_Distance < m_MinDistance)
                    m_DistanceChangeFlag = true;

            }

            transform.LookAt(m_LookTarget);
            Vector3 positionOffset = transform.rotation * Vector3.forward * m_Distance;

            Vector3 newPos = m_LookTarget.position - positionOffset;
            newPos = new Vector3(newPos.x, m_LookTarget.position.y + m_Height, newPos.z);

            transform.position = Vector3.Lerp(transform.position, newPos, Time.fixedDeltaTime);
        }

        public void SetLookTarget(Transform target)
        {
            m_LookTarget = target;
        }
    }
}
