using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class GasolineEngine : Engine
    {
        public enum eGasolineType
        {
            Soler = 1,
            Octan95,
            Octan96,
            Octan98,
        }

        private float m_MaxFuelOfEngine;
        private float m_CurrentFuelOfEngine;
        private eGasolineType m_GasolineType;

        public eGasolineType GasolineType
        {
            get
            {
                return m_GasolineType;
            }

            set
            {
                m_GasolineType = value;
            }
        }

        public float CurrentGasolineOfEngine
        {
            get
            {
                return m_CurrentFuelOfEngine;
            }

            set
            {
                m_CurrentFuelOfEngine = value;
            }
        }

        public static string[] ShowGasolineTypes()
        {
            return Enum.GetNames(typeof(eGasolineType));
        }

        public override void DefineMaxEnergyCapacity(float i_MaxEnergy)
        {
            m_MaxFuelOfEngine = i_MaxEnergy;
        }

        public override void FillEnergyAtEngine(float i_ExtraEnergy)    
        {
            string alertOfInvalidInsertion;

            if(m_CurrentFuelOfEngine + i_ExtraEnergy <= m_MaxFuelOfEngine)
            {
                m_CurrentFuelOfEngine += i_ExtraEnergy;
            }
            else
            {
                alertOfInvalidInsertion = string.Format("You have {0} amount of battery. The amount need to be between: ", m_CurrentFuelOfEngine);
                throw new ValueOutOfRangeException(alertOfInvalidInsertion, m_MaxFuelOfEngine, 0);
            }
        }

        public void FillGasolineAtVehicle(float i_AmountOfGasolineToAdd, eGasolineType i_KindOfGasoline)
        {
            if(m_GasolineType != i_KindOfGasoline)
            {
                throw new ArgumentException("This vehicle is not using this kind of gasoline!");
            }

            FillEnergyAtEngine(i_AmountOfGasolineToAdd);
        }
    }
}
