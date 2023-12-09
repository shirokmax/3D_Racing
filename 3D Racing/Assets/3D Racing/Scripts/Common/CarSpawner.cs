using UnityEngine;

namespace UnityDrift
{
    public class CarSpawner : MonoBehaviour
    {
        [SerializeField] private CarSelectionSetting m_CarSelectionSetting;
        [SerializeField] private CarPresetSetting m_CarPresetSetting;

        public Car Spawn()
        {
            Car car = Instantiate(m_CarSelectionSetting.CarPrefabs[m_CarSelectionSetting.CurrentCarIndex],
                                  transform.position, 
                                  transform.rotation);

            car.ApplyPreset(m_CarPresetSetting.PresetType);

            return car;
        }
    }
}
