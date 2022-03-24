using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        public struct Wheel
        {
            private string m_NameOfManufacturer;
            private float m_CurrentAirPressure;
            private float m_MaxAirPressureByManufacturer;

            public float MaxAirPressureOfWheel
            {
                get
                {
                    return m_MaxAirPressureByManufacturer;
                }

                set
                {
                    m_MaxAirPressureByManufacturer = value;
                }
            }

            public string ManufacturerName
            {
                get
                {
                    return m_NameOfManufacturer;
                }

                set
                {
                    m_NameOfManufacturer = value;
                }
            }

            public float CurrentAirPressure
            {
                get
                {
                    return m_CurrentAirPressure;
                }

                set
                {
                    m_CurrentAirPressure = value;
                }
            }

            public void FillAirPressureInWheels(float i_AmountOfAirToAddToWheel)
            {
                string alertOfInvalidInsertion;

                if(m_CurrentAirPressure + i_AmountOfAirToAddToWheel <= m_MaxAirPressureByManufacturer)
                {
                    m_CurrentAirPressure += i_AmountOfAirToAddToWheel;
                }
                else
                {
                    alertOfInvalidInsertion = string.Format("You have {0} amount of air pressure. The amount need to be between: ", m_CurrentAirPressure);
                    throw new ValueOutOfRangeException(alertOfInvalidInsertion, m_MaxAirPressureByManufacturer, 0);
                }
            }
        }

        private readonly string r_LicenseNumber;
        private readonly Wheel[] r_SetOfWheels;
        private Engine m_EngineOfVehicle;
        private string m_NameOfModel;

        public Vehicle(string i_LicenseNumber, int i_NumberOfWheels)
        {
            r_LicenseNumber = i_LicenseNumber;
            r_SetOfWheels = new Wheel[i_NumberOfWheels];
        }

        public abstract void FillAirInWheels(float i_PressureOfAirWheel);

        public abstract void InitializeWheels();

        public Engine Engine
        {
            get
            {
                return m_EngineOfVehicle;
            }
        }

        public Wheel[] SetWheels
        {
            get
            {
                return r_SetOfWheels;
            }
        }

        public string ModelOfVehicle
        {
            get
            {
                return m_NameOfModel;
            }

            set
            {
                m_NameOfModel = value;
            }
        }

        public string LicenseNumber
        {
            get
            {
                return r_LicenseNumber;
            }
        }

        public abstract void CreateEngine(Engine.eEnergyType i_Engine);

        public void DefineEngineOfVehicle(Engine.eEnergyType i_SourceOfEnergy)
        {
            if(i_SourceOfEnergy == Engine.eEnergyType.Electric)
            {
                m_EngineOfVehicle = new ElectricalEngine();
            }
            else
            {
                m_EngineOfVehicle = new GasolineEngine();
            }
        }

        public void FillEnergyAtEngine(float i_EnergyToAdd)
        {
            m_EngineOfVehicle.FillEnergyAtEngine(i_EnergyToAdd);
        }

        public void WriteDetailsOfWheels(string i_NameOfManufacturOfWheels, float i_ExactlyAirPressure)
        {
            for(int i = 0; i < r_SetOfWheels.Length; i++)
            {
                r_SetOfWheels[i].FillAirPressureInWheels(i_ExactlyAirPressure);
                r_SetOfWheels[i].ManufacturerName = i_NameOfManufacturOfWheels;
            }
        }

        public string GetInformationOfWheels(StoredVehicleInGarage i_VehicleToTakeinformation)
        {
            string currentWheelOfVehicle;
            int numberOfWheel = 1;
            StringBuilder informationOfWheels = new StringBuilder("The information of the wheels is: ");

            foreach(Wheel wheel in i_VehicleToTakeinformation.VehicleInGarage.r_SetOfWheels)
            {
                informationOfWheels.Append(Environment.NewLine);
                currentWheelOfVehicle = string.Format(
@"Wheel number {0}: Current air pressure: {1}, Name of manufacturer: {2}",
numberOfWheel,
wheel.CurrentAirPressure.ToString(),
wheel.ManufacturerName);
                informationOfWheels.Append(currentWheelOfVehicle);
                numberOfWheel++;
            }

            return informationOfWheels.ToString();
        }

        //$G$ DSN-999 (-7) these methods should not be implemented in the base class.
        public string GetInformationOfEngine(StoredVehicleInGarage i_VehicleToTakeinformation)
        {
            StringBuilder informationOfEngine = new StringBuilder("The information of the engine is: ");
            string engineInformation;

            informationOfEngine.Append(Environment.NewLine);
            if(i_VehicleToTakeinformation.VehicleInGarage.Engine is GasolineEngine)
            {
                engineInformation = string.Format(
@"The gasoline type is: {0} 
The current gasoline amount is: {1}",
((GasolineEngine)i_VehicleToTakeinformation.VehicleInGarage.Engine).GasolineType.ToString(),
((GasolineEngine)i_VehicleToTakeinformation.VehicleInGarage.Engine).CurrentGasolineOfEngine.ToString());
                informationOfEngine.Append(engineInformation);
            }
            else if(i_VehicleToTakeinformation.VehicleInGarage.Engine is ElectricalEngine)
            {
                engineInformation = string.Format(
                    "The current time of the engine battery is: {0}",
                    ((ElectricalEngine)i_VehicleToTakeinformation.VehicleInGarage.Engine).TimeOfEngineBattery.ToString());
                informationOfEngine.Append(engineInformation);
            }

            return informationOfEngine.ToString();
        }

        public string GetInformationOfSpecificVehicle(StoredVehicleInGarage i_VehicleToTakeInformation)
        {
            string isTrnkCooled;
            string specificInformationOfVehicle = null;

            if(i_VehicleToTakeInformation.VehicleInGarage is Car)
            {
                specificInformationOfVehicle = string.Format(
                    "The color of the car is: {0}, and the number of doors is: {1}", 
                    ((Car)i_VehicleToTakeInformation.VehicleInGarage).Color.ToString(),
                    ((Car)i_VehicleToTakeInformation.VehicleInGarage).DoorsNumber.ToString());
            }
            else if(i_VehicleToTakeInformation.VehicleInGarage is Motorcycle)
            {
                specificInformationOfVehicle = string.Format(
                    "The type license of the motorcycle is: {0}, and the engine volume: {1}",
                    ((Motorcycle)i_VehicleToTakeInformation.VehicleInGarage).LicenseKind.ToString(),
                    ((Motorcycle)i_VehicleToTakeInformation.VehicleInGarage).VolumeEngine.ToString());
            }
            else if(i_VehicleToTakeInformation.VehicleInGarage is Truck)
            {
                isTrnkCooled = ((Truck)i_VehicleToTakeInformation.VehicleInGarage).TransportingRefrigeratedContents == true ? "Yes" : "No";
                specificInformationOfVehicle = string.Format(
@"There is a cooled trunk transporting contents: {0}
The Baggage capacity is: {1}",
isTrnkCooled,
((Truck)i_VehicleToTakeInformation.VehicleInGarage).TrunkTransportingContentsVolume.ToString());
            }

            return specificInformationOfVehicle;
        }
    }
}