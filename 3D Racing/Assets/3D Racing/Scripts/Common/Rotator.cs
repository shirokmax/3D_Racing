using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private Vector3 m_Speed;
    public Vector3 Speed => m_Speed;

    void Update()
    {
        transform.Rotate(m_Speed * Time.deltaTime);
    }

    public void SetSpeedVector(Vector3 vector)
    {
        m_Speed = vector;
    }
}
