using System;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        public enum eColor
        {
            Red = 1,
            White,
            Black,
            Blue,
        }

        public enum eDoorsNumber
        {
            Two = 1,
            Three,
            Four,
            Five,
        }

        private eColor m_ColorInVehicle;
        private eDoorsNumber m_DoorsNumber;
        private const int k_WheelsNumber = 4;
        private float m_MaxChargeOfEngine = 2.6f;
        private float m_MaxGasolineCapacity = 48;
        private const float k_MaxAirPressure = 29;

        public static string[] DefineNumberOFCarDoors()
        {
            return Enum.GetNames(typeof(eDoorsNumber));
        }

        public static string[] DefineNumberOFCarColor()
        {
            return Enum.GetNames(typeof(eColor));
        }

        public Car(string i_LicenseNumber) : base(i_LicenseNumber, k_WheelsNumber)
        {
        }

        public eColor Color
        {
            get
            {
                return m_ColorInVehicle;
            }

            set
            {
                m_ColorInVehicle = value;
            }
        }

        public eDoorsNumber DoorsNumber
        {
            get
            {
                return m_DoorsNumber;
            }

            set
            {
                m_DoorsNumber = value;
            }
        }

        public override void CreateEngine(Engine.eEnergyType i_TypeOfEngine)
        {
            DefineEngineOfVehicle(i_TypeOfEngine);
            if(Engine is ElectricalEngine)
            {
                Engine.DefineMaxEnergyCapacity(m_MaxChargeOfEngine);
            }
            else
            {
                Engine.DefineMaxEnergyCapacity(m_MaxGasolineCapacity);
                ((GasolineEngine)Engine).GasolineType = GasolineEngine.eGasolineType.Octan95;
            }
        }

        public override void InitializeWheels()
        {
            FillAirInWheels(k_MaxAirPressure);
        }

        public override void FillAirInWheels(float i_PressureOfAirWheel)
        {
            for(int i = 0; i < SetWheels.Length; i++)
            {
                SetWheels[i].MaxAirPressureOfWheel = i_PressureOfAirWheel;
            }
        }
    }
}