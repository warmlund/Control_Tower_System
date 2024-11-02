using Control_Tower_System_DAL;
using Control_Tower_System_DTO;

namespace Control_Tower_System_BLL
{
    public class FlightStorage : ListManager<FlightManager>
    {
        public List<FlightManager> GetAllFlightsManagers()
        {
            var list = new List<FlightManager>();
            foreach (var flight in _list)
            {
                list.Add(flight);
            }
            return list;
        }

        public List<Flight> GetAllFlights()
        {
            var list = new List<Flight>();
            foreach(var flight in _list)
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
