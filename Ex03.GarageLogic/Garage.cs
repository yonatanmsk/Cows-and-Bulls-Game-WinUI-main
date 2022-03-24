using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private List<StoredVehicleInGarage> m_Garage;

        public Garage()
        {
            m_Garage = new List<StoredVehicleInGarage>();
        }

        public List<StoredVehicleInGarage> GarageOfVehicles
        {
            get
            {
                return m_Garage;
            }
        }

        public void ChangeConditionInGarage(StoredVehicleInGarage i_VehicleToChangeCondition, StoredVehicleInGarage.eConditionInGarage i_ConditionToChange)
        {
            i_VehicleToChangeCondition.VehicleCondition = i_ConditionToChange;
        }

        public Engine.eEnergyType DefineSourceOfEnergyByVehicleType(FactoryOfVehicles.eTypeOfVehicle i_TypeOfVehicle)
        {
            Engine.eEnergyType kindOfEnergy;

            if(i_TypeOfVehicle == FactoryOfVehicles.eTypeOfVehicle.ElectricCar || i_TypeOfVehicle == FactoryOfVehicles.eTypeOfVehicle.ElectricMotorcycle)
            {
                kindOfEnergy = Engine.eEnergyType.Electric;
            }
            else
            {
                kindOfEnergy = Engine.eEnergyType.Gasoline;
            }

            return kindOfEnergy;
        }

        public void FillGasolineInVehicle(StoredVehicleInGarage i_Vehicle, GasolineEngine.eGasolineType i_GasolineType, float i_GasolineToFill)
        {
            GasolineEngine gasolineTypeOfEngine = i_Vehicle.VehicleInGarage.Engine as GasolineEngine;

            if(gasolineTypeOfEngine != null)
            {
                gasolineTypeOfEngine.FillGasolineAtVehicle(i_GasolineToFill, i_GasolineType);
            }
            else
            {
                throw new ArgumentException("The engine of the vehicle you inserted is not from gasoline type!");
            }
        }

        public void FillTimeBatteryInVehicle(StoredVehicleInGarage i_Vehicle, float i_AmountOfTimeToAddToEngine)
        {
            ElectricalEngine electricalEngine = i_Vehicle.VehicleInGarage.Engine as ElectricalEngine;

            if(electricalEngine != null)
            {
                electricalEngine.FillEnergyAtEngine(i_AmountOfTimeToAddToEngine);
            }
            else
            {
                throw new ArgumentException("The engine of the vehicle you inserted is not from electrical type!");
            }
        }

        public string TakeInformationOfClientFromGarage(StoredVehicleInGarage i_VehicleToTakeinformation)
        {
            string allInformationOfClient, engineInformation, wheelInformation, vehicleSpecificDetails;

            engineInformation = i_VehicleToTakeinformation.VehicleInGarage.GetInformationOfEngine(i_VehicleToTakeinformation);
            wheelInformation = i_VehicleToTakeinformation.VehicleInGarage.GetInformationOfWheels(i_VehicleToTakeinformation);
            vehicleSpecificDetails = i_VehicleToTakeinformation.VehicleInGarage.GetInformationOfSpecificVehicle(i_VehicleToTakeinformation);
            allInformationOfClient = string.Format(
@"License number: {0}
Name of client: {1}
Phone number Of Client: {2}
Model of vehicle: {3}
Condition Of Car: {4}
{5}
{6}
{7}",
i_VehicleToTakeinformation.VehicleInGarage.LicenseNumber,
i_VehicleToTakeinformation.OwnerName,
i_VehicleToTakeinformation.OwnerPhone,
i_VehicleToTakeinformation.VehicleInGarage.ModelOfVehicle,
i_VehicleToTakeinformation.VehicleCondition.ToString(),
engineInformation,
wheelInformation,
vehicleSpecificDetails);

            return allInformationOfClient;
        }
    }
}