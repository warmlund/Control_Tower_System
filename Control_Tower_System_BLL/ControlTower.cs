using Control_Tower_System_DTO;

namespace Control_Tower_System_BLL
{
    public class ControlTower : IControlTower
    {
        private FlightStorage _flightStorage;

        public ControlTower()
        {
            _flightStorage = new FlightStorage();
        }

        public void CreateFlight(string id, string airline, string destination, double duration, double flightHeight, bool inFlight, TimeOnly time)
        {
            Flight flight = new Flight
            {
                AirlineId = id,
                Airline = airline,
                Destination = destination,
                Duration = duration,
                FlightHeight = flightHeight,
                InFlight = inFlight,
                LocalTime = time
            };

        }

        public void AddFlight(Flight flight)
        {
            _flightStorage.Add(flight);
        }

        public void TakeOff(Flight flight)
        {

        }

        public void Land(Flight flight)
        {

        }

        public void ChangeAltitude(Flight flight)
        {

        }
    }
}
