using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    internal class Motorbike : Vehicle
    {
        private const int k_NumberOfWheels = 2;
        private const float k_MaximumAirPressure = 31;
        private const float k_MaximumFuelTrunkCapacity = 6.2f;
        private const Fuel.eFuelType k_FuelType = Fuel.eFuelType.Octan98;
        private const float k_MaximumChargingTimeCapacity = 2.5f;

        private enum eLisenceType
        {
            A = 1,
            A1,
            B1,
            BB
        }

        private eLisenceType m_LisenceType;
        private int m_EngineCapacity;

        public Motorbike(string i_ModelName, string i_LisenceNumber, EnergySource.eEnergySourceType i_EnergyType, string i_WheelManufacturer)
            : base(i_ModelName, i_LisenceNumber, i_EnergyType)
        {
            if (i_EnergyType == EnergySource.eEnergySourceType.Electric)
            {
                Energy.MaximumEnergy = k_MaximumChargingTimeCapacity;
            }
            else
            {
                Energy.MaximumEnergy = k_MaximumFuelTrunkCapacity;
                ((Fuel)Energy).FuelType = k_FuelType;
            }

            Wheels = new List<Wheel>(k_NumberOfWheels);

            for (int i = 0; i < k_NumberOfWheels; i++)
            {
                Wheels.Add(new Wheel(i_WheelManufacturer, k_MaximumAirPressure));
            }
        }

        public override List<string> CreateListOfMessagesForUniqueMembers()
        {
            List<string> messages = new List<string>(2);

            messages.Add($"Please Enter a motobike lisence type:{Environment.NewLine}1. A{Environment.NewLine}2. A1{Environment.NewLine}3. B1{Environment.NewLine}4. BB");
            messages.Add("Please Enter motorbike's engine capacity: ");

            return messages;
        }

        public override void SetUniqueProperties(string i_UserInput, int i_PropertyNumber)
        {
            switch (i_PropertyNumber)
            {
                case 0:
                        setLisenceTypeProperty(i_UserInput);
                        break;
                case 1:
                        setEngineCapacityProperty(i_UserInput);
                        break;
            }
        }

        private void setLisenceTypeProperty(string i_UserInput)
        {
            int lisenceType = int.Parse(i_UserInput);

            if (lisenceType < 1 || lisenceType > Enum.GetNames(typeof(eLisenceType)).Length)
            {
                throw new FormatException();
            }

            m_LisenceType = (eLisenceType)lisenceType;

        }

        private void setEngineCapacityProperty(string i_UserInput)
        {
            int engineCapacity = int.Parse(i_UserInput);

            if(engineCapacity <= 0)
            {
                throw new FormatException();
            }
            else
            {
                m_EngineCapacity = engineCapacity;
            }            
        }

        public override string ToString()
        {
            return base.ToString() + Environment.NewLine + "Motorbike lisence type: " + m_LisenceType + Environment.NewLine + "Motorbike engine capacity: " + m_EngineCapacity;
        }
    }
}
