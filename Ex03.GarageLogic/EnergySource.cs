namespace Ex03.GarageLogic
{
    public abstract class EnergySource
    {
        public enum eEnergySourceType
        {
            Electric,
            Fuel
        }

        private readonly eEnergySourceType r_SourceType;
        private float m_CurrentEnergy;
        private float m_MaximumEnergy;
        private float m_EnergyPercent;

        public EnergySource(eEnergySourceType i_SourceType)
        {
            r_SourceType = i_SourceType;
        }

        public eEnergySourceType SourceType
        {
            get { return r_SourceType; }
        }

        public float CurrentEnergy
        {
            get { return m_CurrentEnergy; }
            set { m_CurrentEnergy = value; }
        }

        public float MaximumEnergy
        {
            get { return m_MaximumEnergy; }
            set { m_MaximumEnergy = value; }
        }

        public void SetEnergyPercent()
        {
            m_EnergyPercent = m_CurrentEnergy / m_MaximumEnergy * 100;
        }

        public override string ToString()
        {
            return $"Current energy: {m_EnergyPercent}%";
        }
    }
}
