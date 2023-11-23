using UnityEngine;
using UnityEngine.UI;

namespace CarRacing
{
    public class UITrackDrift : MonoBehaviour, IDependency<RaceStateTracker>, IDependency<RaceDriftTracker>
    {
        [SerializeField] private GameObject m_IndicatorPanel;
        [SerializeField] private Image m_CircleFillImage;
        [SerializeField] private Text m_PointsText;
        [SerializeField] private Text m_GeneralMult;

        [Space]
        [SerializeField] private Image m_GoodEmoteImage;
        [SerializeField] private Image m_BadEmoteImage;
        [SerializeField] private GameObject m_EndedDriftPanel;

        private RaceStateTracker m_RaceStateTracker;
        public void Construct(RaceStateTracker obj) => m_RaceStateTracker = obj;

        private RaceDriftTracker m_RaceDriftTracker;
        public void Construct(RaceDriftTracker obj) => m_RaceDriftTracker = obj;

        private void Start()
        {
            m_RaceStateTracker.EventOnRaceStarted.AddListener(OnRaceStarted);
            m_RaceStateTracker.EventOnRaceFinished.AddListener(OnRaceFinished);

            m_GeneralMult.text = ((int)m_RaceDriftTracker.GeneralMult).ToString();

            m_IndicatorPanel.SetActive(false);
            m_EndedDriftPanel.SetActive(false);
            m_GoodEmoteImage.enabled = false;
            m_BadEmoteImage.enabled = false;
            enabled = false;
        }

        private void Update()
        {
            m_PointsText.text = ((int)m_RaceDriftTracker.CurrentPoints).ToString();

            UpdatePanels();

            GeneralMultTextUpdate();
            CircleFillImageUpdate();
        }

        private void UpdatePanels()
        {
            if (m_RaceDriftTracker.IsDriftStartTimerFinished == false)
            {
                m_IndicatorPanel.SetActive(false);
                m_EndedDriftPanel.SetActive(true);
                m_GoodEmoteImage.enabled = false;
                m_BadEmoteImage.enabled = true;
            }
            else if (m_RaceDriftTracker.GeneralMult == 1f)
            {
                DisableAllIndicators();
            }
            else
            {
                m_IndicatorPanel.SetActive(true);
                m_EndedDriftPanel.SetActive(false);
                m_GoodEmoteImage.enabled = true;
                m_BadEmoteImage.enabled = false;
            }
        }

        private void DisableAllIndicators()
        {
            m_IndicatorPanel.SetActive(false);
            m_EndedDriftPanel.SetActive(false);
            m_GoodEmoteImage.enabled = false;
            m_BadEmoteImage.enabled = false;
        }

        private void GeneralMultTextUpdate()
        {
            float mult = m_RaceDriftTracker.GeneralMult;

            int value = (int)(mult * 10 % 10);
            int value2 = (int)(mult * 100 % 10);

            if (value < 2 && value % 2 == 0 && value2 == 0)
            {
                m_GeneralMult.text = "x" + (int)mult;
                return;
            }
            else if (value % 2 == 0 && value2 == 0)
            {
                m_GeneralMult.text = "x" + (int)mult + "." + value;
                return;
            }
        }

        private void CircleFillImageUpdate()
        {
            if (m_RaceDriftTracker.GeneralMult == RaceDriftTracker.MAX_GENERAL_MULT)
            {
                m_CircleFillImage.fillAmount = 1f;
                return;
            }

            float value = (m_RaceDriftTracker.GeneralMult - (int)m_RaceDriftTracker.GeneralMult) * 5;
            float fillAmount = value - (int)value;

            m_CircleFillImage.fillAmount = fillAmount;
        }

        private void OnRaceStarted()
        {
            enabled = true;
        }

        private void OnRaceFinished()
        {
            enabled = false;
            DisableAllIndicators();
        }
    }
}
