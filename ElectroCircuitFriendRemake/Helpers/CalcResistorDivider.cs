using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectroCircuitFriendRemake.Helpers
{
    public class CalcResistorDivider
    {
        public static double Vout(double Vs, int R1, int R2)
        {
            return Vs * R2 / (R1 + R2);
        }

        public static double R1(double Vs, double Vout, int R2)
        {
            return Vs * R2 / Vout - R2;
        }

        public static double R2(double Vs, double Vout, int R1)
        {
            return Vout * R1 / (Vs - Vout);
        }

        public static double Vs(double Vout, int R1, int R2)
        {
            return Vout * (R1 + R2) / R2;
        }

    }
}
