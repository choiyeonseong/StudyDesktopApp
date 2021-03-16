using System;

namespace IoTSensorMonApp
{
    internal class SensorData
    {
        public DateTime Current { get; set; }   // 현재 시간
        public int Value { get; set; }  // 센서값
        public bool SimulFlag { get; set; } // 시뮬레이션 여부


        /// <summary>
        /// 생성자
        /// </summary>
        public SensorData(DateTime current, int value, bool simulFalg)
        {
            Current = current;
            Value = value;
            SimulFlag = simulFalg;
        }
    }
}
