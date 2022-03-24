using System;

namespace Ex03.GarageLogic
{
    public class StoredVehicleInGarage
    {
        public enum eConditionInGarage
        {
            InRepair = 1,
            Fixed,
            Paid,
        }

        private string m_OwnerName;
        private string m_OwnerPhone;
        private eConditionInGarage m_ConditionOfVehicle;
        private Vehicle m_VehicleInGarage;

        public Vehicle VehicleInGarage
        {
            get
            {
                return m_VehicleInGarage;
            }

            set
            {
                m_VehicleInGarage = value;
            }
        }

        public eConditionInGarage VehicleCondition
        {
            get
            {
                return m_ConditionOfVehicle;
            }

            set
            {
                m_ConditionOfVehicle = value;
            }
        }

        public string OwnerName
        {
            get
            {
                return m_OwnerName;
            }
        }

        public string OwnerPhone
        {
            get
            {
                return m_OwnerPhone;
            }
        }

        public StoredVehicleInGarage(string i_OwnerName, string i_OwnerPhone, eConditionInGarage i_ConditionOfVehicle, Vehicle i_VehicleInGarage)
        {
            m_OwnerName = i_OwnerName;
            m_OwnerPhone = i_OwnerPhone;
            m_ConditionOfVehicle = i_ConditionOfVehicle;
            m_VehicleInGarage = i_VehicleInGarage;
        }
    }
}