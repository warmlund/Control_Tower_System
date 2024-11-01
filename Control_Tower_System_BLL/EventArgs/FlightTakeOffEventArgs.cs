using Control_Tower_System_DTO;

namespace Control_Tower_System_BLL
{
    /// <summary>
    /// Event args for flight takeoff
    /// </summary>
    public class FlightTakeOffEventArgs : FlightEventArgs
    {
        public DateTime TakeOffTime { get; }

        public FlightTakeOffEventArgs(Flight flight, DateTime time) : base(flight)
        {
            TakeOffTime = time;
        }
    }
}
