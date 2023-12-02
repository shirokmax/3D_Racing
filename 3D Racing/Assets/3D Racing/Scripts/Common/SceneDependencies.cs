using CarRacing;
using UnityEngine;

public class SceneDependencies : MonoBehaviour
{
    [SerializeField] private Car m_Car;
    [SerializeField] private CarInputControl m_CarInputControl;
    [SerializeField] private CarCameraController m_CarCameraController;
    [SerializeField] private TrackpointCircuit m_TrackpointCircuit;
    [SerializeField] private RaceStateTracker m_RaceStateTracker;
    [SerializeField] private RaceTimeTracker m_RaceTimeTracker;
    [SerializeField] private RaceResults m_RaceResults;
    [SerializeField] private RaceDriftTracker m_RaceDriftTracker;

    /// <summary>
    /// ����� ���������� ������.
    /// ���� ������� ��������� ��������� � ������ �����, �� �������� ����� �������� ������ �� ������ ������
    /// </summary>
    /// <param name="mono"></param>
    private void Bind(MonoBehaviour mono)
    {
        (mono as IDependency<Car>)?.Construct(m_Car);
        (mono as IDependency<CarInputControl>)?.Construct(m_CarInputControl);
        (mono as IDependency<CarCameraController>)?.Construct(m_CarCameraController);
        (mono as IDependency<TrackpointCircuit>)?.Construct(m_TrackpointCircuit);
        (mono as IDependency<RaceStateTracker>)?.Construct(m_RaceStateTracker);
        (mono as IDependency<RaceTimeTracker>)?.Construct(m_RaceTimeTracker);
        (mono as IDependency<RaceResults>)?.Construct(m_RaceResults);
        (mono as IDependency<RaceDriftTracker>)?.Construct(m_RaceDriftTracker);
    }

    private void Awake()
    {
        // ������ ���� ���������, ����������� �� �����
        MonoBehaviour[] monosInScene = FindObjectsOfType<MonoBehaviour>();

        // ��������� ���� ��������� �� �����.
        // ��� ����������� ����������� ���������� ������ ��������� ����� ���� � ������� ��� � ������ ������� �� ����������� ������������ ������ ������.
        foreach(var mono in monosInScene)
            Bind(mono);
    }
}
