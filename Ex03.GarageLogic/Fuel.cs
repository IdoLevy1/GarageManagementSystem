using System;

namespace Ex03.GarageLogic
{
    public class Fuel : EnergySource
    {
        public enum eFuelType
        {
            Soler,
            Octan95,
            Octan96,
            Octan98
        }

        private eFuelType m_FuelType;

        public Fuel(eEnergySourceType i_SourceType)
            : base(i_SourceType) 
        {
        }

        public eFuelType FuelType
        {
            get { return m_FuelType; }
            set { m_FuelType = value; }
        }

        public void FuelVehicle(float i_FuelToAdd, eFuelType i_FuelType)
        {
            if (i_FuelType != m_FuelType)
            {
                throw new ArgumentException("Wrong fuel type to this vehicle !");
            }
            else if (MaximumEnergy >= CurrentEnergy + i_FuelToAdd)
            {
                CurrentEnergy += i_FuelToAdd;
                SetEnergyPercent();
            }
            else
            {
                throw new ValueOutOfRangeException(MaximumEnergy - CurrentEnergy, 0);
            }
        }

        public override string ToString()
        {
            return $"Fuel type: {m_FuelType}{Environment.NewLine}" + base.ToString();
        }
    }
}
