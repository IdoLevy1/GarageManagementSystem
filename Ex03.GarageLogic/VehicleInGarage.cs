namespace Ex03.GarageLogic
{
    public class VehicleInGarage
    {
        public enum eVehicleStatus
        {
            InRepair,
            Repaired,
            Paid
        }

        private readonly string r_OwnerName;
        private readonly string r_OwnerPhoneNumber;
        private Vehicle m_Vehicle;
        private eVehicleStatus m_VehicleStatus;

        public VehicleInGarage(Vehicle i_Vehicle, string i_OwnerName, string i_OwnerPhoneNumber, eVehicleStatus i_VehicleStatus = eVehicleStatus.InRepair)
        {
            m_Vehicle = i_Vehicle;
            r_OwnerName = i_OwnerName;
            r_OwnerPhoneNumber = i_OwnerPhoneNumber;
            m_VehicleStatus = i_VehicleStatus;
        }

        public Vehicle Vehicle
        {
            get { return m_Vehicle; }
        }

        public eVehicleStatus VehicleStatus
        {
            get { return m_VehicleStatus; }
            set { m_VehicleStatus = value; }
        }

        public override string ToString()
        {
            string details = @"Owner name: {0}
Vehicle status: {1}
{2}";

            return string.Format(details, r_OwnerName, m_VehicleStatus.ToString(), m_Vehicle.ToString());
        }
    }
}
