using UnityEngine;
using UnityEngine.Events;

public class SimpleTimer : MonoBehaviour
{
    [SerializeField] private float m_Time;

    private float m_Value;
    public float Value => m_Value;

    private UnityEvent m_EventOnFinished = new UnityEvent();
    public UnityEvent EventOnFinished => m_EventOnFinished;

    private UnityEvent<string> m_EventOnTimerSecondsTicked = new UnityEvent<string>();
    public UnityEvent<string> EventOnTimerSecondsTicked => m_EventOnTimerSecondsTicked;

    private string m_CurrentSecondsTick = "";

    private void Awake()
    {
        m_Value = m_Time;
    }

    private void Update()
    {
        m_Value -= Time.deltaTime;

        if (m_Value.ToString("F0") != m_CurrentSecondsTick)
        {
            m_CurrentSecondsTick = m_Value.ToString("F0");
            m_EventOnTimerSecondsTicked?.Invoke(m_CurrentSecondsTick);
        }

        if (m_Value <= 0)
        {
            enabled = false;

            m_EventOnFinished?.Invoke();
        }
    }
}
