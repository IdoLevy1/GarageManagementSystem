using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    internal class Car : Vehicle
    {
        private const int k_NumberOfWheels = 4;
        private const float k_MaximumAirPressure = 29;
        private const float k_MaximumFuelTrunkCapacity = 38f;
        private const Fuel.eFuelType k_FuelType = Fuel.eFuelType.Octan95;
        private const float k_MaximumChargingTimeCapacity = 3.3f;

        private enum eColor
        {
            Red = 1,
            White,
            Green,
            Blue
        }

        private enum eNumberOfDoors
        {
            Two = 1,
            Three,
            Four,
            Five
        }

        private eColor m_Color;
        private eNumberOfDoors m_NumberOfDoors; 

        public Car(string i_ModelName, string i_LisenceNumber, EnergySource.eEnergySourceType i_EnergyType, string i_WheelManufacturer)
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

            for(int i = 0; i < k_NumberOfWheels; i++)
            {
                Wheels.Add(new Wheel(i_WheelManufacturer, k_MaximumAirPressure));
            }
        }

        public override List<string> CreateListOfMessagesForUniqueMembers()
        {
            List<string> messages = new List<string>(2);

            messages.Add(string.Format(@"
Please choose car's color:
1. Red
2. White
3. Green
4. Blue"));
            messages.Add(string.Format(@"
Please choose car's color:
1. Two
2. Three
3. Four
4. Five"));

            return messages;
        }

        public override void SetUniqueProperties(string i_UserInput, int i_PropertyNumber)
        {
            switch (i_PropertyNumber)
            {
                case 0:
                        setColorProperty(i_UserInput);
                        break;                    
                case 1:
                        setNumberOfDoorsProperty(i_UserInput);
                        break;
            }
        }

        private void setColorProperty(string i_UserInput)
        {
            int color = int.Parse(i_UserInput);

            if (color < 1 || color > Enum.GetNames(typeof(eColor)).Length)
            {
                throw new FormatException();
            }

            m_Color = (eColor)color;
        }

        private void setNumberOfDoorsProperty(string i_UserInput)
        {
            int numberOfDoors = int.Parse(i_UserInput);

            if (numberOfDoors < 1 || numberOfDoors > Enum.GetNames(typeof(eNumberOfDoors)).Length)
            {
                throw new FormatException();
            }

            m_NumberOfDoors = (eNumberOfDoors)numberOfDoors;  
        }

        public override string ToString()
        {
            return base.ToString() + Environment.NewLine + "Car color: " + m_Color + Environment.NewLine + "Number of doors: " + m_NumberOfDoors;
        }
    }
}
