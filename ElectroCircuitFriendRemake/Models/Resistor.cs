namespace ElectroCircuitFriendRemake.Models
{
    public enum ResistorColors
    {
        Black,
        Brown,
        Red,
        Orange,
        Yellow,
        Green,
        Blue,
        Violet,
        Gray,
        White,
        Gold,
        Silver
    }

    public enum ResistorMultiplier
    {
        X0,
        X10,
        X100,
        X1000,
        X10000,
        X100000,
        X1000000,
        X10000000,
        X100000000,
        X1000000000,
        X01,
        X001
    }

    public enum ResistorTolerance
    {
        T1 = 1,
        T2 = 2,
        T05 = 5,
        T025 = 6,
        T01 = 7,
        T005 = 8,
        T5 = 10,
        T10 = 11,
        T20 = 12
    }

    public class Resistor
    {
        public int Id { get; set; }
        public int Band1 { get; set; }
        public int Band2 { get; set; }
        public int Band3 { get; set; }
        public int? Band4 { get; set; }
        public int? Band5 { get; set; }
        public bool Use4Bands { get; set; }
        public bool Use5Bands { get; set; }
        public int Amount { get; set; }
    }
}