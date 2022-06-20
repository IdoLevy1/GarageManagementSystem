using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    internal class GarageManager
    {
        private readonly Garage r_Garage = new Garage();
        private readonly UserInterface r_UserInterface = new UserInterface();
        
        public void ManageGarage()
        {
            int userSelection;
            bool isExitGarage = false;

            while(!isExitGarage)
            {
                r_UserInterface.PrintMenu();

                try
                {
                    userSelection = r_UserInterface.GetUserSelectionFromUser();

                    switch(userSelection)
                    {
                        case 1:
                                addVehicleToGarage();
                                break;
                        case 2:
                                getVehiclesLisenceNumber();
                                break;
                        case 3:
                                changeVehicleStatusInGarage();
                                break;
                        case 4:
                                inflateAirInWheels();
                                break;
                        case 5:
                                fuelVehicle();
                                break;
                        case 6:
                                chargeVehicle();
                                break;
                        case 7:
                                getVehicleDetails();
                                break;
                        case 8:
                                isExitGarage = true;
                                break;
                        default:
                                throw new FormatException();
                    }
                }
                catch(FormatException formatException)
                {
                    r_UserInterface.PrintExceptionMessage(formatException);
                }
            }
        }

        private void addVehicleToGarage()
        {
            string ownerName, ownerPhoneNumber;
            string lisenceNumber = r_UserInterface.GetLisenceNumberFromUser();

            try
            {
                r_Garage.CheckIfVehicleInGarage(lisenceNumber);
                r_Garage.ChangeVehicleStatus(lisenceNumber, VehicleInGarage.eVehicleStatus.InRepair);
                r_UserInterface.PrintVehicleInGarageMessage(lisenceNumber);
            }
            catch(ArgumentException)
            {
                ownerName = r_UserInterface.GetOwnerNameFromUser();
                ownerPhoneNumber = r_UserInterface.GetOwnerPhoneNumberFromUser();
                r_Garage.AddVehicle(getNewVehicle(lisenceNumber), ownerName, ownerPhoneNumber);
            } 
        }

        private Vehicle getNewVehicle(string i_LisenceNumber)
        {
            Vehicle vehicle;
            VehicleCreator.eVehicleType vehicleType = r_UserInterface.GetVehicleTypeFromUser();
            string modelName = r_UserInterface.GetModelNameFromUser();
            string wheelsManufacturer = r_UserInterface.GetWheelsManufacturerFromUser();

            vehicle = VehicleCreator.CreateVehicle(vehicleType, modelName, i_LisenceNumber, wheelsManufacturer);
            setDataToVehicle(vehicle);

            return vehicle;
        }

        private void setDataToVehicle(Vehicle i_Vehicle)
        {
            r_UserInterface.SetWheelsCurrentAirPressureFromUser(i_Vehicle.Wheels);
            i_Vehicle.SetVehicleData(r_UserInterface.GetCurrentEnergyAmountFromUser(i_Vehicle.Energy.SourceType, i_Vehicle.Energy.MaximumEnergy));           
            SetUniqueProperties(i_Vehicle);
        }

        private void SetUniqueProperties(Vehicle i_Vehicle)
        {
            string userInput;
            List<string> messages = i_Vehicle.CreateListOfMessagesForUniqueMembers();

            for (int i = 0; i < messages.Count; i++)
            {
                userInput = r_UserInterface.GetUniquePropertyString(messages[i]);

                try
                {
                    i_Vehicle.SetUniqueProperties(userInput, i);
                }
                catch (FormatException formatException)
                {
                    i--;
                    r_UserInterface.PrintExceptionMessage(formatException);
                }
                catch (ArgumentException argumentException)
                {
                    i--;
                    r_UserInterface.PrintExceptionMessage(argumentException);
                }
            }
        }

        private void getVehiclesLisenceNumber()
        {
           VehicleInGarage.eVehicleStatus? status = r_UserInterface.GetFilterStatusFromUser();
           StringBuilder lisenceList = r_Garage.GetVehiclesLisenceNumberByStatus(status);
           r_UserInterface.PrintLisenceList(lisenceList, status);
        }

        private void changeVehicleStatusInGarage()
        {
            string lisenceNumber = r_UserInterface.GetLisenceNumberFromUser();
            VehicleInGarage.eVehicleStatus newStatus = r_UserInterface.GetNewStatusFromUser();

            try
            {
                r_Garage.ChangeVehicleStatus(lisenceNumber, newStatus);
            }
            catch(ArgumentException argumentException)
            {
                r_UserInterface.PrintExceptionMessage(argumentException);
            }
        }
       
        private void inflateAirInWheels()
        {
            string lisenceNumber = r_UserInterface.GetLisenceNumberFromUser();

            try
            {
                r_Garage.InflateWheelsInVehicle(lisenceNumber);
            }
            catch (ArgumentException argumentException)
            {
                r_UserInterface.PrintExceptionMessage(argumentException);
            }
        }

        private void fuelVehicle()
        {
            string lisenceNumber = r_UserInterface.GetLisenceNumberFromUser();

            try
            {
                r_Garage.CheckIfEnergyIsFuel(lisenceNumber);
                r_Garage.FuelVehicleInGarage(lisenceNumber, r_UserInterface.GetFuelTypeFromUser(), r_UserInterface.GetEnergyAmountToFillFromUser());
            }
            catch(ArgumentException argumentException)
            {
                r_UserInterface.PrintExceptionMessage(argumentException);
            }
            catch (ValueOutOfRangeException valueOutOfRangeException)
            {
                r_UserInterface.PrintExceptionMessage(valueOutOfRangeException);
            }
        }

        private void chargeVehicle()
        {
            string lisenceNumber = r_UserInterface.GetLisenceNumberFromUser();

            try
            {
                r_Garage.CheckIfEnergyIsElectric(lisenceNumber);
                r_Garage.ChargeVehicleInGarage(lisenceNumber, r_UserInterface.GetEnergyAmountToFillFromUser()); 
            }
            catch (ArgumentException argumentException)
            {
                r_UserInterface.PrintExceptionMessage(argumentException);
            }
            catch (ValueOutOfRangeException valueOutOfRangeException)
            {
                r_UserInterface.PrintExceptionMessage(valueOutOfRangeException);
            }
        }

        private void getVehicleDetails()
        {
            string lisenceNumber = r_UserInterface.GetLisenceNumberFromUser();
            string vehicleDetails;

            try
            {
                vehicleDetails = r_Garage.GetVehicleDetails(lisenceNumber);
                r_UserInterface.printVehicleDetails(vehicleDetails);
            }
            catch (ArgumentException argumentException)
            {
                r_UserInterface.PrintExceptionMessage(argumentException);
            }
        }
    }
}
