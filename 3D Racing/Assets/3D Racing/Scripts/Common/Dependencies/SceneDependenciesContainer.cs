using CarRacing;
using UnityEngine;

namespace CarRacing
{
    public class SceneDependenciesContainer : Dependency
    {
        [SerializeField] private Car m_Car;
        [SerializeField] private CarInputControl m_CarInputControl;
        [SerializeField] private CarCameraController m_CarCameraController;
        [SerializeField] private TrackpointCircuit m_TrackpointCircuit;
        [SerializeField] private RaceStateTracker m_RaceStateTracker;
        [SerializeField] private RaceTimeTracker m_RaceTimeTracker;
        [SerializeField] private RaceResults m_RaceResults;
        [SerializeField] private RaceDriftTracker m_RaceDriftTracker;

        protected override void BindAll(MonoBehaviour monoBehaviourInScene)
        {
            Bind<Car>(m_Car, monoBehaviourInScene);
            Bind<CarInputControl>(m_CarInputControl, monoBehaviourInScene);
            Bind<CarCameraController>(m_CarCameraController, monoBehaviourInScene);
            Bind<TrackpointCircuit>(m_TrackpointCircuit, monoBehaviourInScene);
            Bind<RaceStateTracker>(m_RaceStateTracker, monoBehaviourInScene);
            Bind<RaceTimeTracker>(m_RaceTimeTracker, monoBehaviourInScene);
            Bind<RaceResults>(m_RaceResults, monoBehaviourInScene);
            Bind<RaceDriftTracker>(m_RaceDriftTracker, monoBehaviourInScene);
        }

        private void Awake()
        {
            FindAllObjectsToBind();
        }
    }
}
