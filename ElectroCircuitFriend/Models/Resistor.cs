namespace ElectroCircuitFriend.Models
{
    public class Resistor
    {
        public int Id { get; set; }
        public int Band1 { get; set; }
        public int Band2 { get; set; }
        public int Band3 { get; set; }
        public int Band4 { get; set; }
        public int Band5 { get; set; }
        public bool Use5Bands { get; set; }
        public int Amount { get; set; }
    }
}