using UnityEngine;

namespace CarRacing
{
    public class WheelEffect : MonoBehaviour
    {
        [SerializeField] private WheelCollider[] m_Wheels;
        [SerializeField] private ParticleSystem[] m_WheelsSmoke;
        [SerializeField] private GameObject m_SkidPrefab;
        [SerializeField] private AudioSource m_Audio;

        [Space]
        [SerializeField] private float m_ForwardSlipLimit;
        [SerializeField] private float m_SidewaysSlipLimit;

        private Transform[] m_SkidTrails;
        private WheelHit m_WheelHit;

        private void Awake()
        {
            m_SkidTrails = new Transform[m_Wheels.Length];
        }

        private void Update()
        {
            bool isSlip = false;

            for (int i = 0; i < m_Wheels.Length; i++)
            {
                m_Wheels[i].GetGroundHit(out m_WheelHit);

                if (m_Wheels[i].isGrounded && (Mathf.Abs(m_WheelHit.forwardSlip) > m_ForwardSlipLimit || Mathf.Abs(m_WheelHit.sidewaysSlip) > m_SidewaysSlipLimit))
                {
                    if (m_SkidTrails[i] == null)
                        m_SkidTrails[i] = Instantiate(m_SkidPrefab).transform;

                    isSlip = true;

                    if (m_Audio.isPlaying == false)
                    {
                        m_Audio.time = Random.Range(0f, m_Audio.clip.length);
                        m_Audio.Play();
                    }

                    m_SkidTrails[i].position = m_Wheels[i].transform.position - m_WheelHit.normal * m_Wheels[i].radius;
                    m_SkidTrails[i].forward = -m_WheelHit.normal;

                    m_WheelsSmoke[i].transform.position = m_SkidTrails[i].transform.position;

                    if (m_WheelsSmoke[i].isEmitting == false)
                        m_WheelsSmoke[i].Play();
                }
                else
                {
                    m_SkidTrails[i] = null;
                    m_WheelsSmoke[i].Stop();
                }
            }

            if (isSlip == false)
                m_Audio.Stop();
        }
    }
}
