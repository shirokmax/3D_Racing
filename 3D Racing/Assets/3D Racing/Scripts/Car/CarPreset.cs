using UnityEngine;

namespace UnityDrift
{
    public class CarPreset : ScriptableObject
    {
        [SerializeField] private CarPresetType m_Type;
        public CarPresetType Type => m_Type;

                           // ���� �������� ������ //

        // CarChassis:
        // ������� ������ WheelAxles"Setting", ����� ������ ������ �� �����, ���� �������� ������ ��� wheelAxles.Lengs ������, �� return. ���� �������� ������� � IsMotor
        // CenterOfMass - ������� ����� ��� ��� ��������� � ���� ��������
        // ��������� ���, ����� wheelBaseLength

        // Car:
        // ��� ���������, ����� CarName, EngineTorqueCurve
        // � ������� GearBox - ������ Up/Down ShiftEngineRpm
    }
}
