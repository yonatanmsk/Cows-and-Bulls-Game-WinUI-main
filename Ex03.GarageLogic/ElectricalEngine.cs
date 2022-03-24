using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class ElectricalEngine : Engine
    {
        private float m_MaxChargeOfEngine;
        private float m_CurrentAmountOfChargingEngine;

        public float TimeOfEngineBattery
        {
            get
            {
                return m_CurrentAmountOfChargingEngine;
            }
        }

        public override void FillEnergyAtEngine(float i_ExtraEnergy)
        {
            string alertOfInvalidInsertion;

            if(m_CurrentAmountOfChargingEngine + i_ExtraEnergy <= m_MaxChargeOfEngine)
            {
                m_CurrentAmountOfChargingEngine += i_ExtraEnergy;
            }
            else
            {
                alertOfInvalidInsertion = string.Format("You have {0} amount of battery. The amount need to be between: ", m_CurrentAmountOfChargingEngine);
                throw new ValueOutOfRangeException(alertOfInvalidInsertion, m_MaxChargeOfEngine, 0);
            }
        }

        public override void DefineMaxEnergyCapacity(float i_MaxEnergy)
        {
            m_MaxChargeOfEngine = i_MaxEnergy;
        }
    }
}