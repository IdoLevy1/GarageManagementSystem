using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private Dictionary<string, VehicleInGarage> m_Vehicles;

        public Garage()
        {
            m_Vehicles = new Dictionary<string, VehicleInGarage>();
        }

        public void CheckIfVehicleInGarage(string i_LisenceNumber)
        {   
            if(!m_Vehicles.ContainsKey(i_LisenceNumber))
            {
                throw new ArgumentException("Vehicle isn't in garage !");
            }
        }

        public void ChangeVehicleStatus(string i_LisenceNumber, VehicleInGarage.eVehicleStatus i_NewStatus)
        {
            CheckIfVehicleInGarage(i_LisenceNumber);
            m_Vehicles[i_LisenceNumber].VehicleStatus = i_NewStatus;
        }

        public void AddVehicle(Vehicle i_Vehicle, string i_OwnerName, string i_OwnerPhoneNumber)
        {
            m_Vehicles.Add(i_Vehicle.LisenceNumber, new VehicleInGarage(i_Vehicle, i_OwnerName, i_OwnerPhoneNumber));
        }

        public StringBuilder GetVehiclesLisenceNumberByStatus(VehicleInGarage.eVehicleStatus? i_Status)
        {
            VehicleInGarage.eVehicleStatus status;
            StringBuilder lisenceList = new StringBuilder();

            if(i_Status != null)
            {
                status = (VehicleInGarage.eVehicleStatus)i_Status;

                foreach (VehicleInGarage vehicle in m_Vehicles.Values)
                {
                    if (vehicle.VehicleStatus == status)
                    {
                        lisenceList.Append(vehicle.Vehicle.LisenceNumber + " ");
                    }
                }
            }
            else
            {
                foreach (VehicleInGarage vehicle in m_Vehicles.Values)
                {
                    lisenceList.Append(vehicle.Vehicle.LisenceNumber + " ");
                }
            }

            return lisenceList;
        }

        public void InflateWheelsInVehicle(string i_LisenceNumber)
        {
            CheckIfVehicleInGarage(i_LisenceNumber);

            foreach (Wheel wheel in m_Vehicles[i_LisenceNumber].Vehicle.Wheels)
            {
                wheel.InflateWheel(wheel.MaximumAirPressure - wheel.CurrentAirPressure);
            }
        }

        public void CheckIfEnergyIsFuel(string i_LisenceNumber)
        {
            CheckIfVehicleInGarage(i_LisenceNumber);

             if(m_Vehicles[i_LisenceNumber].Vehicle.Energy is Electric)
             {
                throw new ArgumentException("This is an electric vehicle !");
             }
        }

        public void CheckIfEnergyIsElectric(string i_LisenceNumber)
        {
            CheckIfVehicleInGarage(i_LisenceNumber);

            if (m_Vehicles[i_LisenceNumber].Vehicle.Energy is Fuel)
            {
                throw new ArgumentException("This vehicle is using fuel !");
            }
        }

        public void FuelVehicleInGarage(string i_LisenceNumber, Fuel.eFuelType i_FuelType, float i_FuelAmountToFill)
        {
            ((Fuel)m_Vehicles[i_LisenceNumber].Vehicle.Energy).FuelVehicle(i_FuelAmountToFill, i_FuelType); 
        }

        public void ChargeVehicleInGarage(string i_LisenceNumber, float i_TimeToCharge)
        {
            ((Electric)m_Vehicles[i_LisenceNumber].Vehicle.Energy).ChargeVehicle(i_TimeToCharge);
        }

        public string GetVehicleDetails(string i_LisenceNumber)
        {
            CheckIfVehicleInGarage(i_LisenceNumber);

            return m_Vehicles[i_LisenceNumber].ToString();
        }
    }
}
