using Control_Tower_System_DTO;

namespace Control_Tower_System_BLL
{
    /// <summary>
    /// Event args for flight landing
    /// </summary>
    public class FlightLandedEventArgs : FlightEventArgs
    {
        public DateTime LandTime { get; }

        public FlightLandedEventArgs(Flight flight, DateTime time) : base(flight)
        {
            LandTime = time;
        }

    }
}
