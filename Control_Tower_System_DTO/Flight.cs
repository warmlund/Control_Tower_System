namespace Control_Tower_System_DTO
{
    public class Flight
    {
        public string AirlineId { get; set; }
        public string Airline { get; set; }
        public string Destination { get; set; }
        public double Duration { get; set; }
        public double FlightAltitude { get; set; }
        public bool InFlight { get; set; }
        public TimeOnly LocalTime { get; set; }
    }
}
