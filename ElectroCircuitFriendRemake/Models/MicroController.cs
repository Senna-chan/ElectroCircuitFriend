using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectroCircuitFriendRemake.Models
{
    public enum MicroControllerFeatures
    {
        Bluetooth,
        BluetoothLE,
        Wifi,
        GPS,
        LoRa,
        IMU6DOF,
        Temperature,
        Pressure,
        NativeUSB,
        LipoManagment
    }
    public class MicroController
    {
        public int IOPins { get; set; }
        public int ADCPins { get; set; }
        public float Voltage { get; set; }
        public MicroControllerFeatures[] Features { get; set; }
    }
}
