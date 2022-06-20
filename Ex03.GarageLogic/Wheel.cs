using System;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private readonly string r_ManufacturerName;
        private readonly float r_MaximumAirPressure;
        private float m_CurrentAirPressure;

        public Wheel(string i_ManufacturerName, float i_MaximumAirPressure)
        {
            r_ManufacturerName = i_ManufacturerName;
            r_MaximumAirPressure = i_MaximumAirPressure;
        }

        public float CurrentAirPressure
        {
            get { return m_CurrentAirPressure; }
            set { m_CurrentAirPressure = value; }
        }

        public float MaximumAirPressure
        {
            get { return r_MaximumAirPressure; }
        }

        public void InflateWheel(float i_AirPressureToAdd)
        {
            if(r_MaximumAirPressure < m_CurrentAirPressure + i_AirPressureToAdd)
            {
                throw new ValueOutOfRangeException(r_MaximumAirPressure - m_CurrentAirPressure, 0);
            }
            else
            {
                m_CurrentAirPressure += i_AirPressureToAdd;
            } 
        }

        public override string ToString()
        {
            return $"Wheel manufacturer: {r_ManufacturerName}{Environment.NewLine}Wheel current air pressure: {m_CurrentAirPressure}{Environment.NewLine}";
        }
    }
}
