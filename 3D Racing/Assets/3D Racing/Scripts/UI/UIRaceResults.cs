using UnityDrift;
using UnityEngine;
using UnityEngine.UI;

public class UIRaceResults : MonoBehaviour, IDependency<RaceResults>, IDependency<LoadedRaceInfo>
{
    [SerializeField] private GameObject m_RaceResultsPanel;

    [Space]
    [SerializeField] private GameObject m_TimeRecordPanel;
    [SerializeField] private GameObject m_DriftRecordPanel;

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

    private LoadedRaceInfo m_LoadedRaceSceneInfo;
    public void Construct(LoadedRaceInfo obj) => m_LoadedRaceSceneInfo = obj;

    private float m_CurrentTimeRecord;
    private float m_CurrentDriftRecord;

    private void Start()
    {
        m_TimeRecordPanel.SetActive(false);
        m_DriftRecordPanel.SetActive(false);

        m_TimeNewRecordText.enabled = false;
        m_DriftNewRecordText.enabled = false;

        m_RaceResults.EventOnResultsUpdated.AddListener(OnResultsUpdated);

        if (m_LoadedRaceSceneInfo.Info.RaceType == RaceType.Race)
        {
            m_CurrentTimeRecord = m_RaceResults.GetAbsoluteTimeRecord();
            m_RaceRecordTimeText.text = StringTime.SecondToTimeString(m_CurrentTimeRecord);
        }

        if (m_LoadedRaceSceneInfo.Info.RaceType == RaceType.Drift)
        {
            m_CurrentDriftRecord = m_RaceResults.GetAbsoluteDriftRecord();
            m_RaceRecordDriftText.text = ((int)m_CurrentDriftRecord).ToString();
        }
    }

    private void OnResultsUpdated()
    {
        m_RaceResultsPanel.SetActive(true);

        if (m_LoadedRaceSceneInfo.Info.RaceType == RaceType.Race)
        {
            m_TimeRecordPanel.SetActive(true);

            m_RacePlayerTimeText.text = StringTime.SecondToTimeString(m_RaceResults.CurrentTime);

            if (m_RaceResults.CurrentTime < m_CurrentTimeRecord)
                m_TimeNewRecordText.enabled = true;
        }

        if (m_LoadedRaceSceneInfo.Info.RaceType == RaceType.Drift)
        {
            m_DriftRecordPanel.SetActive(true);

            m_RacePlayerDriftText.text = ((int)m_RaceResults.CurrentDriftPoints).ToString();

            if ((int)m_RaceResults.CurrentDriftPoints > (int)m_CurrentDriftRecord)
                m_DriftNewRecordText.enabled = true;
        }
    }
}
