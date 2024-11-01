using Control_Tower_System_DTO;

namespace Control_Tower_System_BLL
{
    /// <summary>
    /// Event args for flight takeoff
    /// </summary>
    public class FlightTakeOffEventArgs : FlightEventArgs
    {
        public string Message { get; }

        public FlightTakeOffEventArgs(Flight flight) : base(flight)
        {
            Message = $"Flight {flight.Airline} ({flight.AirlineId}) has departed to {flight.Destination} at {flight.LocalTime}";
        }
    }
}
