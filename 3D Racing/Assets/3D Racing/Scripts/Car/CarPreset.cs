using UnityEngine;

namespace UnityDrift
{
    public class CarPreset : ScriptableObject
    {
        [SerializeField] private CarPresetType m_Type;
        public CarPresetType Type => m_Type;

                           // Поля настроек машины //

        // CarChassis:
        // сделать массив WheelAxles"Setting", потом делать апплай по циклу, если настроек меньше чем wheelAxles.Lengs машины, то return. Поля настроек начиная с IsMotor
        // CenterOfMass - сделать метод для его изменения и тоже апплаить
        // остальное все, кроме wheelBaseLength

        // Car:
        // Все настройки, кроме CarName, EngineTorqueCurve
        // В разделе GearBox - только Up/Down ShiftEngineRpm
    }
}
