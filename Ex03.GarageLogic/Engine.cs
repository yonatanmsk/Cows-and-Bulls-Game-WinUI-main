using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public abstract class Engine
    {
        public enum eEnergyType
        {
            Electric = 1,
            Gasoline,
        }

        public abstract void FillEnergyAtEngine(float i_ExtraEnergy);

        public abstract void DefineMaxEnergyCapacity(float i_MaxEnergy);
    }
}