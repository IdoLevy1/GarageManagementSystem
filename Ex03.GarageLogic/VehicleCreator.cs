namespace Ex03.GarageLogic
{
    public class VehicleCreator
    {
        public enum eVehicleType
        {
            Fuel_Car = 1,
            Electric_Car,
            Fuel_Motorbike,
            Electric_Motorbike,
            Fuel_Truck
        }

        public static Vehicle CreateVehicle(eVehicleType i_VehicleType, string i_ModelName, string i_LisenceNumber, string i_WheelManufacturer)
        {
            Vehicle newVehicle;

            switch(i_VehicleType)
            {
                case eVehicleType.Fuel_Car:
                        newVehicle = new Car(i_ModelName, i_LisenceNumber, EnergySource.eEnergySourceType.Fuel, i_WheelManufacturer);
                        break;
                case eVehicleType.Electric_Car:
                        newVehicle = new Car(i_ModelName, i_LisenceNumber, EnergySource.eEnergySourceType.Electric, i_WheelManufacturer);
                        break;
                case eVehicleType.Fuel_Motorbike:
                        newVehicle = new Motorbike(i_ModelName, i_LisenceNumber, EnergySource.eEnergySourceType.Fuel, i_WheelManufacturer);
                        break;
                case eVehicleType.Electric_Motorbike:
                        newVehicle = new Motorbike(i_ModelName, i_LisenceNumber, EnergySource.eEnergySourceType.Electric, i_WheelManufacturer);
                        break;
                default:
                        newVehicle = new Truck(i_ModelName, i_LisenceNumber, EnergySource.eEnergySourceType.Fuel, i_WheelManufacturer);
                        break;
            }

            return newVehicle;
        }
    }
}
