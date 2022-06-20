using System;
using System.Collections.Generic;
using System.Text;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    internal class UserInterface
    {
        private const int k_MinimumLisenceNumberLength = 6;
        private const int k_MaximumLisenceNumberLength = 8;
        private const int k_PhoneNumberMinimumLength = 9;
        private const int k_PhoneNumberMaximumLength = 10;
        private const int k_MaximumNameLength = 20;

        public void PrintMenu()
        {
            string menu = string.Format(@"Please choose an action you want to perform:
1. Insert a new vehicle to the garage.
2. Display the list of vehicle lisence number in the garage.
3. Change a vehicle status in the garage.
4. Inflate air on wheels in a vehicle.
5. Refuel a regular vehicle.
6. Charging an electric vehicle.
7. Display a vehicle details.
8. Exit.");

            Console.WriteLine(menu);
        }

        public void PrintExceptionMessage(Exception i_Exception)
        {
            Console.Clear();

            if (i_Exception is FormatException)
            {
                Console.WriteLine("Invalid input format ! Please enter a valid input");
            }
            else
            {
                Console.WriteLine(i_Exception.Message);
            }    
        }

        public int GetUserSelectionFromUser()
        {
            string userSelection;
            userSelection = Console.ReadLine();

            return int.Parse(userSelection);
        }

        private void checkIfStringIsDigits(string i_UserInput)
        {
            foreach (char digit in i_UserInput)
            {
                if (!char.IsDigit(digit))
                {
                    throw new FormatException();
                }
            }
        }

        private void checkIfStringIsLetters(string i_UserInput)
        {
            foreach (char letter in i_UserInput)
            {
                if (!char.IsLetter(letter) && letter != ' ')
                {
                    throw new FormatException();
                }
            }
        }

        private string getNameFromUser(string i_MessageToUser)
        {
            string name;
            bool isValidName = false;

            do
            {
                Console.WriteLine(i_MessageToUser);
                name = Console.ReadLine();

                try
                {
                    if (name.Length > k_MaximumNameLength)
                    {
                        throw new FormatException();
                    }

                    checkIfStringIsLetters(name);
                    isValidName = true;
                }
                catch (FormatException formatException)
                {
                    PrintExceptionMessage(formatException);
                }
            }
            while (!isValidName);

            return name;
        }

        private string getNumberFromUser(string i_UserInput, int i_MinValue, int i_MaxValue)
        {
            string number;
            bool isValidNumber = false;

            do
            {
                Console.WriteLine(i_UserInput);
                number = Console.ReadLine();

                try
                {
                    if (number.Length > i_MaxValue || number.Length < i_MinValue)
                    {
                        throw new FormatException();
                    }

                    checkIfStringIsDigits(number);
                    isValidNumber = true;
                }
                catch (FormatException formatException)
                {
                    PrintExceptionMessage(formatException);
                }
            }
            while (!isValidNumber);

            return number;
        }

        public string GetLisenceNumberFromUser()
        {
            return getNumberFromUser("Please enter a lisence number (6 - 8 digits): ", k_MinimumLisenceNumberLength, k_MaximumLisenceNumberLength);
        }

        public void PrintVehicleInGarageMessage(string i_LisenceNumber)
        {
            Console.WriteLine($"Vehicle {i_LisenceNumber} is already in the garage.{Environment.NewLine}Vehicle status has changed to InRepair.");
        }

        public string GetOwnerNameFromUser()
        {
            return getNameFromUser("Please enter an owner name: ");
        }

        public string GetOwnerPhoneNumberFromUser()
        {
            return getNumberFromUser("Please enter an owner phone number (9 - 10 digits): ", k_PhoneNumberMinimumLength, k_PhoneNumberMaximumLength);
        }

        public VehicleCreator.eVehicleType GetVehicleTypeFromUser()
        {
            int userSelection = 0;
            bool isValidVehicleType = false;

            do
            {
                Console.WriteLine("Please choose a vehicle type: ");

                foreach (int type in Enum.GetValues(typeof(VehicleCreator.eVehicleType)))
                {
                    Console.WriteLine($"{type}. {(VehicleCreator.eVehicleType)type}");
                }

                try
                {
                    userSelection = GetUserSelectionFromUser();

                    if (userSelection < 1 || userSelection > Enum.GetNames(typeof(VehicleCreator.eVehicleType)).Length)
                    {
                        throw new FormatException();
                    }

                    isValidVehicleType = true;
                }
                catch (FormatException formatException)
                {
                    PrintExceptionMessage(formatException);
                }
            }
            while (!isValidVehicleType);

            return (VehicleCreator.eVehicleType)userSelection;
        }

        public string GetModelNameFromUser()
        {
            return getNameFromUser("Please Enter a Model Name: ");
        }

        public string GetWheelsManufacturerFromUser()
        {
            return getNameFromUser("Please Enter the wheels manufacturer: ");
        }

        public float GetCurrentEnergyAmountFromUser(EnergySource.eEnergySourceType i_EnergySourceType, float i_MaximumEnergyAmount)
        {
            string currentEnergyAmountString;
            bool isValidCurrentEnergyAmount = false;
            float currentEnergyAmount = 0;
            string messageToUser = (i_EnergySourceType == EnergySource.eEnergySourceType.Electric) ? "Please Enter current charging time amount:" :
                "Please Enter current Fuel amount in vehicle: ";
            do
            {
                Console.WriteLine(messageToUser);
                currentEnergyAmountString = Console.ReadLine();

                try
                {
                    currentEnergyAmount = float.Parse(currentEnergyAmountString);

                    if(currentEnergyAmount > i_MaximumEnergyAmount || currentEnergyAmount < 0)
                    {
                        throw new ValueOutOfRangeException(i_MaximumEnergyAmount, 0);
                    }

                    isValidCurrentEnergyAmount = true;
                }
                catch (FormatException formatException)
                {
                    PrintExceptionMessage(formatException);
                }
                catch(ValueOutOfRangeException exception)
                {
                    PrintExceptionMessage(exception);
                }
            }
            while (!isValidCurrentEnergyAmount);

            return currentEnergyAmount;
        }

        private float getAirPressureToWheelFromUser(int i_WheelNumber)
        {
            string wheelAirPressure;
            bool isValidWheelAirPressure = false;
            float currentAirPressure = 0;

            do
            {
                Console.WriteLine($"Please Enter a current wheel air pressure for wheel number {i_WheelNumber}: ");
                wheelAirPressure = Console.ReadLine();

                try
                {
                    currentAirPressure = float.Parse(wheelAirPressure);
                    isValidWheelAirPressure = true;
                }
                catch (FormatException formatException)
                {
                    PrintExceptionMessage(formatException);
                }
            }
            while (!isValidWheelAirPressure);

            return currentAirPressure;
        }

        public void SetWheelsCurrentAirPressureFromUser(List<Wheel> i_Wheels)
        {
            float CurrentAirPressureInWheel;

            for (int i = 0; i < i_Wheels.Count; i++)
            {
                try
                {
                    CurrentAirPressureInWheel = getAirPressureToWheelFromUser(i + 1);

                    if (CurrentAirPressureInWheel > i_Wheels[i].MaximumAirPressure || CurrentAirPressureInWheel < 0)
                    {
                        throw new ValueOutOfRangeException(i_Wheels[i].MaximumAirPressure, 0);
                    }

                    i_Wheels[i].CurrentAirPressure = CurrentAirPressureInWheel;
                }
                catch (ValueOutOfRangeException exception)
                {
                    PrintExceptionMessage(exception);
                    i--;
                }
            }
        }

        public string GetUniquePropertyString(string i_Message)
        {
            string input;

            Console.WriteLine(i_Message);
            input = Console.ReadLine();

            return input;
        }

        public VehicleInGarage.eVehicleStatus? GetFilterStatusFromUser()
        {
            VehicleInGarage.eVehicleStatus? status;
            int userSelection = 0;
            bool isValidinput = false;
            string statusVehiclesOption = string.Format(@"Please choose one of the following vehicles status to present:
1. InRepair vehicles
2. Repaired vehicles
3. Paid vehicles
4. All vehicles");

            do
            {
                Console.WriteLine(statusVehiclesOption);

                try
                {
                    userSelection = GetUserSelectionFromUser();

                    if (userSelection < 1 || userSelection > 4)
                    {
                        throw new FormatException();
                    }

                    isValidinput = true;
                }
                catch (FormatException formatException)
                {
                    PrintExceptionMessage(formatException);
                }
            }
            while (!isValidinput);

            switch (userSelection)
            {
                case 1:
                        status = VehicleInGarage.eVehicleStatus.InRepair;
                        break;
                case 2:
                        status = VehicleInGarage.eVehicleStatus.Repaired;
                        break;
                case 3:
                        status = VehicleInGarage.eVehicleStatus.Paid;
                        break;
                default:
                        status = null;
                        break;
            }

            return status;
        }

        public void PrintLisenceList(StringBuilder i_lisenceList, VehicleInGarage.eVehicleStatus? i_Status)
        {
            if (i_lisenceList.Length == 0)
            {
                if (i_Status == null)
                {
                    Console.WriteLine("There are no vehicles in the garage!");
                }
                else
                {
                    Console.WriteLine($"There are no {i_Status} vehicles in the garage!");
                }
            }
            else
            {
                Console.WriteLine($"Vehicles in garage with status {i_Status}: {i_lisenceList}");
            }
        }

        public VehicleInGarage.eVehicleStatus GetNewStatusFromUser()
        {
            VehicleInGarage.eVehicleStatus status;
            int userSelection = 0;
            bool isValidinput = false;
            string statusOptions = string.Format(@"
Please enter a new status:
1. InRepair
2. Repaired
3. Paid");

            do
            {
                Console.WriteLine(statusOptions);               

                try
                {
                    userSelection = GetUserSelectionFromUser();

                    if (userSelection < 1 || userSelection > 3)
                    {
                        throw new FormatException();
                    }

                    isValidinput = true;
                }
                catch (FormatException formatException)
                {
                    PrintExceptionMessage(formatException);
                }
            }
            while (!isValidinput);

            if (userSelection == 1)
            {
                status = VehicleInGarage.eVehicleStatus.InRepair;
            }
            else if (userSelection == 2)
            {
                status = VehicleInGarage.eVehicleStatus.Repaired;
            }
            else
            {
                status = VehicleInGarage.eVehicleStatus.Paid;
            }

            return status;
        }

        public Fuel.eFuelType GetFuelTypeFromUser()
        {
            Fuel.eFuelType fuelType;
            int userSelection = 0;
            bool isValidFuelType = false;
            string fuelOptions = string.Format(@"
Please Enter a fuel type:
1. Octan95
2. Octan96
3. Octan98
4. Soler");

            do
            {
                Console.WriteLine(fuelOptions);

                try
                {
                    userSelection = GetUserSelectionFromUser();

                    if (userSelection < 1 || userSelection > 4)
                    {
                        throw new FormatException();
                    }

                    isValidFuelType = true;
                }
                catch (FormatException formatException)
                {
                    PrintExceptionMessage(formatException);
                }
            }
            while (!isValidFuelType);

            switch(userSelection)
            {
                case 1:
                        fuelType = Fuel.eFuelType.Octan95;
                        break;
                case 2:
                        fuelType = Fuel.eFuelType.Octan96;
                        break;
                case 3:
                        fuelType = Fuel.eFuelType.Octan98;
                        break;
                default:
                        fuelType = Fuel.eFuelType.Soler;
                        break;
            }

            return fuelType;
        }

        public float GetEnergyAmountToFillFromUser()
        {
            string energyAmountToFillString;
            bool isValidEnergyAmountToFill = false;
            float energyAmountToFill = 0;

            do
            {
                Console.WriteLine("Please Enter energy amount to fill: ");
                energyAmountToFillString = Console.ReadLine();

                try
                {
                    energyAmountToFill = float.Parse(energyAmountToFillString);
                    isValidEnergyAmountToFill = true;
                }
                catch (FormatException formatException)
                {
                    PrintExceptionMessage(formatException);
                }
            }
            while (!isValidEnergyAmountToFill);

            return energyAmountToFill;
        }
       
        public void printVehicleDetails(string i_VehicleDetails)
        {
            Console.WriteLine(i_VehicleDetails);
        }  
    }
}
