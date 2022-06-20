using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        private readonly string r_ModelName;
        private readonly string r_LisenceNumber;
        private EnergySource m_Energy;
        private List<Wheel> m_Wheels; 
        
        public Vehicle(string i_ModelName, string i_LisenceNumber, EnergySource.eEnergySourceType i_EnergyType)
        {
            r_ModelName = i_ModelName;
            r_LisenceNumber = i_LisenceNumber;
            
            if(i_EnergyType == EnergySource.eEnergySourceType.Electric)
            {
                m_Energy = new Electric(i_EnergyType);
            }
            else
            {
                m_Energy = new Fuel(i_EnergyType);
            }           
        }

        public string LisenceNumber
        {
            get { return r_LisenceNumber; }
        }

        public EnergySource Energy
        {
            get { return m_Energy; }
            set { m_Energy = value; }
        }

        public List<Wheel> Wheels
        {
            get { return m_Wheels; }
            set { m_Wheels = value; }
        }

        public void SetVehicleData(float i_CurrentEnergy)
        {
            Energy.CurrentEnergy = i_CurrentEnergy;
            Energy.SetEnergyPercent();
        }

        public abstract List<string> CreateListOfMessagesForUniqueMembers();

        public abstract void SetUniqueProperties(string i_UserInput, int i_PropertyNumber);

        public override string ToString()
        {
            string details = @"Lisence number: {0}
Model name : {1}
{2}
Wheels details:
{3}";
               
            return string.Format(details, r_LisenceNumber, r_ModelName, m_Energy.ToString(), getWheelsDetails());
        }

        private string getWheelsDetails()
        {
            StringBuilder wheelsDetails = new StringBuilder();

            for (int i = 0; i < m_Wheels.Count; i++)
            {
                wheelsDetails.Append($"WheelNumber: {i + 1}{Environment.NewLine}{m_Wheels[i].ToString()}");
            }

            return wheelsDetails.ToString();
        }
    }
}
