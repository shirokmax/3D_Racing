using UnityEngine;

namespace CarRacing
{
    public class CarSpawner : MonoBehaviour
    {
        [SerializeField] private CarSelectionSetting m_CarSelectionSetting;

        public Car Spawn()
        {
            int carIndex = PlayerPrefs.GetInt(m_CarSelectionSetting.Title, 0);
            Car car = Instantiate(m_CarSelectionSetting.CarPrefabs[carIndex], transform.position, transform.rotation);

            //TODO: Применять сохраненные настройки пресета и неона

            return car;
        }
    }
}
