using DG.Tweening;

using UnityEngine;

namespace CarRacing
{
    public class CarSelectController : MonoBehaviour
    {
        [SerializeField] private CarSelectionSetting m_CarSelectionSetting;
        public CarSelectionSetting CarSelectionSetting => m_CarSelectionSetting;

        [SerializeField] private float m_CarsDistance;
        public float SpawnDistance => m_CarsDistance;

        [SerializeField] private Vector3 m_CarRotatingSpeed = new Vector3(0, 30, 0);
        [SerializeField] private float m_MoveToIndexTime = 1;

        private Car[] Cars;

        private void Start()
        {
            Cars = new Car[m_CarSelectionSetting.CarPrefabs.Length];

            SpawnCars();
            DisableCarsObjects();

            MoveToCurrentIndex();
        }

        private void SpawnCars()
        {
            for (int i = 0; i < m_CarSelectionSetting.CarPrefabs.Length; i++)
            {
                Car car = Instantiate(m_CarSelectionSetting.CarPrefabs[i], gameObject.transform);

                car.transform.localPosition = Vector3.left * m_CarsDistance * i;
                car.gameObject.AddComponent<Rotator>().SetSpeedVector(m_CarRotatingSpeed);

                Cars[i] = car;
            }
        }

        private void DisableCarsObjects()
        {
            foreach (Car car in Cars)
            {
                car.transform.GetComponent<Rigidbody>().isKinematic = true;

                DisableAllParticleSystems(car);
                DisableAllCarMonos<AudioSource>(car);
                DisableAllCarMonos<Canvas>(car);
                DisableAllCarMonos<WheelEffect>(car);
                DisableAllCarMonos<EngineSound>(car);
                DisableAllCarMonos<UICarSpeedIndicator>(car);
                DisableAllCarMonos<UICarEngineIndicator>(car);
                DisableAllCarMonos<UICarGearboxIndicator>(car);
                DisableAllCarMonos<SwitchCarLights>(car);
            }
        }

        private void DisableAllCarMonos<T>(Car car) where T : Behaviour
        {
            T[] audioSources = car.transform.GetComponentsInChildren<T>();

            foreach (T obj in audioSources)
                obj.enabled = false;
        }

        private void DisableAllParticleSystems(Car car)
        {
            ParticleSystem[] particleSystems = car.transform.GetComponentsInChildren<ParticleSystem>();

            foreach (var ps in particleSystems)
            {
                var emission = ps.emission;
                emission.enabled = false;
                ps.Clear();
            }
        }

        public void MoveToCurrentIndex()
        {
            transform.DOLocalMoveX(m_CarSelectionSetting.CurrentCarIndex * m_CarsDistance, m_MoveToIndexTime);
        }
    }
}
