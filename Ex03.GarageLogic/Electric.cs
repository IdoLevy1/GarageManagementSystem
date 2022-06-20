namespace Ex03.GarageLogic
{
    internal class Electric : EnergySource
    {
        public Electric(eEnergySourceType i_SourceType)
            : base(i_SourceType) 
        {
        }

        public void ChargeVehicle(float i_ChargingTimeToAdd)
        {
            if (MaximumEnergy >= CurrentEnergy + i_ChargingTimeToAdd)
            {
                CurrentEnergy += i_ChargingTimeToAdd;
                SetEnergyPercent();
            }
            else
            {
                throw new ValueOutOfRangeException(MaximumEnergy - CurrentEnergy, 0);
            }
        }
    }
}
