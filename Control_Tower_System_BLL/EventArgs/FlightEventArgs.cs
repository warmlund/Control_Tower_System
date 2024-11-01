using Control_Tower_System_DTO;

namespace Control_Tower_System_BLL
{
    /// <summary>
    /// Base class for the eventargs of the flights
    /// </summary>
    public class FlightEventArgs : EventArgs
    {
        public Flight Flight { get; }

        public FlightEventArgs(Flight flight) 
        { 
            Flight = flight; 
        }
    }
}
