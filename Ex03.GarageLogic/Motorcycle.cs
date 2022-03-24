using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Motorcycle : Vehicle
    {
        public enum eLicenseKind
        {
            A = 1,
            A2,
            AA,
            B,
        }

        private const int k_WheelsNumber = 2;
        private const float k_MaxAirPressure = 30;
        private const float k_MaxChargeOfEngine = 2.3f;
        private const float k_MaxGasolineCapacity = 5.8f;
        private int m_VolumeEngine;
        private eLicenseKind m_LicenseKind;

        public static string[] DefineLicenseKind()
        {
            return Enum.GetNames(typeof(eLicenseKind));
        }

        public Motorcycle(string i_LicenseNumber) : base(i_LicenseNumber, k_WheelsNumber)
        {
        }

        public int VolumeEngine
        {
            get
            {
                return m_VolumeEngine;
            }

            set
            {
                m_VolumeEngine = value;
            }
        }

        public eLicenseKind LicenseKind
        {
            get
            {
                return m_LicenseKind;
            }

            set
            {
                m_LicenseKind = value;
            }
        }

        public override void CreateEngine(Engine.eEnergyType i_TypeOfEngine)
        {
            DefineEngineOfVehicle(i_TypeOfEngine);
            if(Engine is ElectricalEngine)
            {
                Engine.DefineMaxEnergyCapacity(k_MaxChargeOfEngine);
            }
            else
            {
                Engine.DefineMaxEnergyCapacity(k_MaxGasolineCapacity);
                ((GasolineEngine)Engine).GasolineType = GasolineEngine.eGasolineType.Octan98;
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