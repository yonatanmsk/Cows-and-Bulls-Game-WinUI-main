using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public static class FactoryOfVehicles
    {
        public enum eTypeOfVehicle
        {
            Truck = 1,
            ElectricCar,
            ElectricMotorcycle,
            GasolineCar,
            GasolineMotorcycle,
        }

        public static Vehicle CreateVehicle(string i_LicenseNumber, eTypeOfVehicle i_TypeOfVehicle)
        {
            Vehicle newVehicle = null;

            if(i_TypeOfVehicle == eTypeOfVehicle.GasolineCar || i_TypeOfVehicle == eTypeOfVehicle.ElectricCar) 
            {
                newVehicle = new Car(i_LicenseNumber);
            }
            else if(i_TypeOfVehicle == eTypeOfVehicle.ElectricMotorcycle || i_TypeOfVehicle == eTypeOfVehicle.GasolineMotorcycle)
            {
                newVehicle = new Motorcycle(i_LicenseNumber);
            }
            else if(i_TypeOfVehicle == eTypeOfVehicle.Truck)
            {
                newVehicle = new Truck(i_LicenseNumber);
            }

            return newVehicle;
        }

        public static string[] GetVehicleOptions()
        {
            return Enum.GetNames(typeof(eTypeOfVehicle));
        }
    }
}