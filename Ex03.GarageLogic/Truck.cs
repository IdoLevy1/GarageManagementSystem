using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    internal class Truck : Vehicle
    {
        private const int k_NumberOfWheels = 16;
        private const float k_MaximumAirPressure = 24;
        private const float k_MaximumFuelTrunkCapacity = 120f;
        private const Fuel.eFuelType k_FuelType = Fuel.eFuelType.Soler;

        private bool m_IsCarriesRefrigeratedStock;
        private float m_TrunkCapacity;

        public Truck(string i_ModelName, string i_LisenceNumber, EnergySource.eEnergySourceType i_EnergyType, string i_WheelManufacturer)
            : base(i_ModelName, i_LisenceNumber, i_EnergyType)
        {
            Energy.MaximumEnergy = k_MaximumFuelTrunkCapacity;
            ((Fuel)Energy).FuelType = k_FuelType;
            Wheels = new List<Wheel>(k_NumberOfWheels);

            for (int i = 0; i < k_NumberOfWheels; i++)
            {
                Wheels.Add(new Wheel(i_WheelManufacturer, k_MaximumAirPressure));
            }
        }
        
        public override List<string> CreateListOfMessagesForUniqueMembers()
        {
            List<string> messages = new List<string>(2);

            messages.Add($"Please Enter if the truck carries refrigerated stock:{Environment.NewLine}1. Yes{Environment.NewLine}2. No");
            messages.Add("Please Enter Truck's trunk capacity: ");

            return messages;
        }

        public override void SetUniqueProperties(string i_UserInput, int i_PropertyNumber)
        {
            switch (i_PropertyNumber)
            {
                case 0:
                        setRefrigeratedStockProperty(i_UserInput);
                        break;
                case 1:
                        setTrunkCapacityProperty(i_UserInput);
                        break;
            }
        }

        private void setRefrigeratedStockProperty(string i_UserInput)
        {
            if (i_UserInput == "1")
            {
                m_IsCarriesRefrigeratedStock = true;
            }
            else if (i_UserInput == "2")
            {
                m_IsCarriesRefrigeratedStock = false;
            }
            else 
            {
                throw new FormatException();
            }
        }

        private void setTrunkCapacityProperty(string i_UserInput)
        {
            float trunkCapacity = float.Parse(i_UserInput);
            
            if(trunkCapacity > 0)
            {
                m_TrunkCapacity = trunkCapacity;
            }
            else
            {
                throw new FormatException();
            }
        }

        public override string ToString()
        {
            string isRefrigeratedStock = m_IsCarriesRefrigeratedStock ? "Truck carries refrigerated stock" : "Truck isn't carry refrigerated stock";

            return base.ToString() + Environment.NewLine + isRefrigeratedStock + Environment.NewLine + "Truck trunk capacity: " + m_TrunkCapacity;
        }
    }
}
