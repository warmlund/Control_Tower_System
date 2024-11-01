using Control_Tower_System_DTO;

namespace Control_Tower_System_BLL
{
    /// <summary>
    /// This class handles two delegate events for flight takeoff and landing
    /// "Blueprint" for Flight is in the flight class in the DTO layer
    /// </summary>
    internal class FlightManager
    {
        public event EventHandler<FlightTakeOffEventArgs> TakingOff;
        public event EventHandler<FlightLandedEventArgs> Landing;

        public void TakeOff(Flight flight, DateTime time) => OnTakeOff(flight, time);
        public void Land(Flight flight, DateTime time) => OnLanding(flight, time);

        private void OnTakeOff(Flight flight, DateTime takeOffTime)
        {
            TakingOff?.Invoke(flight, new FlightTakeOffEventArgs(flight, takeOffTime));
        }

        private void OnLanding(Flight flight, DateTime landingTime)
        {
            Landing?.Invoke(flight, new FlightLandedEventArgs(flight, landingTime));
        }
    }
}
