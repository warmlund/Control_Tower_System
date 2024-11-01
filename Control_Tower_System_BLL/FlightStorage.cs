using Control_Tower_System_DAL;
using Control_Tower_System_DTO;

namespace Control_Tower_System_BLL
{
    public class FlightStorage : ListManager<Flight>
    {
        public List<Flight> GetAllFlights()
        {
            var list = new List<Flight>();
            foreach (var flight in _list)
            {
                list.Add(flight);
            }
            return list;
        }
    }
}
