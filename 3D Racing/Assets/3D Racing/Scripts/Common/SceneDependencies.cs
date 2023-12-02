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
    /// Метод связывания ссылок.
    /// Если монобех реализует интерфейс с нужным типом, то вызываем метод передачи ссылки на нужный объект
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
        // Массив всех монобехов, находящихся на сцене
        MonoBehaviour[] monosInScene = FindObjectsOfType<MonoBehaviour>();

        // Обработка всех монобехов на сцене.
        // Для определения очередности необходимо делать отдельный такой цикл и ставить его в нужном порядке по очередности относительно других циклов.
        foreach(var mono in monosInScene)
            Bind(mono);
    }
}
