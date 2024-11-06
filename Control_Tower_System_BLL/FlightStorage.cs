using Control_Tower_System_DAL;
using Control_Tower_System_DTO;

namespace Control_Tower_System_BLL
{
    public class FlightStorage : ListManager<FlightManager>
    {
        public List<FlightManager> GetAllFlightsManagers()
        {
            List<FlightManager> list = new List<FlightManager>();
            foreach (FlightManager flight in _list)
            {
                list.Add(flight);
            }
            return list;
        }

        public List<Flight> GetAllFlights()
        {
            List<Flight> list = new List<Flight>();
            foreach(FlightManager flight in _list)
            {
                list.Add(flight.CurrentFlight);
            }

            return list;
        }

        public Flight GetFlight(int id)
        {
            return _list[id].CurrentFlight;
        }

        bool IsFlightIdUsed(string id)
        {
            return _list.Any(f => f.CurrentFlight.AirlineId == id);
        }
    }
}
