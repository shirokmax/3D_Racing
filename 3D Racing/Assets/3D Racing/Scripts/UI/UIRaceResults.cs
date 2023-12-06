using CarRacing;
using UnityEngine;
using UnityEngine.UI;

public class UIRaceResults : MonoBehaviour, IDependency<RaceResults>
{
    [SerializeField] private GameObject m_RaceResultsPanel;

    [Space]
    [SerializeField] private Text m_RaceRecordTimeText;
    [SerializeField] private Text m_RacePlayerTimeText;
    [SerializeField] private Text m_TimeNewRecordText;

    [Space]
    [SerializeField] private Text m_RaceRecordDriftText;
    [SerializeField] private Text m_RacePlayerDriftText;
    [SerializeField] private Text m_DriftNewRecordText;

    private RaceResults m_RaceResults;
    public void Construct(RaceResults obj) => m_RaceResults = obj;

    private float m_CurrentTimeRecord;
    private float m_CurrentDriftRecord;

    private void Start()
    {
        m_RaceResultsPanel.SetActive(true);

        m_RaceResults.EventOnResultsUpdated.AddListener(OnResultsUpdated);

        m_TimeNewRecordText.enabled = false;
        m_DriftNewRecordText.enabled = false;

        m_CurrentTimeRecord = m_RaceResults.GetAbsoluteTimeRecord();
        m_RaceRecordTimeText.text = StringTime.SecondToTimeString(m_CurrentTimeRecord);

        m_CurrentDriftRecord = m_RaceResults.GetAbsoluteDriftRecord();
        m_RaceRecordDriftText.text = ((int)m_CurrentDriftRecord).ToString();

        gameObject.SetActive(false);
    }

    private void OnResultsUpdated()
    {
        gameObject.SetActive(true);

        m_RacePlayerTimeText.text = StringTime.SecondToTimeString(m_RaceResults.CurrentTime);

        if (m_RaceResults.CurrentTime < m_CurrentTimeRecord)
            m_TimeNewRecordText.enabled = true;

        m_RacePlayerDriftText.text = ((int)m_RaceResults.CurrentDriftPoints).ToString();

        if ((int)m_RaceResults.CurrentDriftPoints > (int)m_CurrentDriftRecord)
            m_DriftNewRecordText.enabled = true;
    }
}
