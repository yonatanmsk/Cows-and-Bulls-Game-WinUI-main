using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        private bool m_IsTransportingRefrigeratedContents;
        private float m_TransportingContentsVolume;
        internal const int k_WheelsNumber = 16;
        internal const float k_PressureOfAirWheel = 25;
        internal const float m_MaxGasolineCapacity = 130;

        public Truck(string i_NumberLicenseOfVehicle) : base(i_NumberLicenseOfVehicle, k_WheelsNumber)
        {
        }

        public bool TransportingRefrigeratedContents
        {
            get
            {
                return m_IsTransportingRefrigeratedContents;
            }

            set
            {
                m_IsTransportingRefrigeratedContents = value;
            }
        }

        public float TrunkTransportingContentsVolume
        {
            get
            {
                return m_TransportingContentsVolume;
            }

            set
            {
                m_TransportingContentsVolume = value;
            }
        }

        public override void CreateEngine(Engine.eEnergyType i_TypeOfEngine)
        {
            DefineEngineOfVehicle(i_TypeOfEngine);
            Engine.DefineMaxEnergyCapacity(m_MaxGasolineCapacity);
            ((GasolineEngine)Engine).GasolineType = GasolineEngine.eGasolineType.Soler;
        }

        public override void InitializeWheels()
        {
            FillAirInWheels(k_PressureOfAirWheel);
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