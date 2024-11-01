using Control_Tower_System_DTO;

namespace Control_Tower_System_BLL
{
    /// <summary>
    /// Eventargs for flight altitude
    /// </summary>
    public class FlightHeightEventArgs : FlightEventArgs
    {
        public double Altitude { get; }

        public FlightHeightEventArgs(Flight flight, double altitude) : base(flight)
        {
            Altitude = altitude;
        }
    }
}
