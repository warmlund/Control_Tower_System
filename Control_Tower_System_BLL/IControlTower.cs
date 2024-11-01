using Control_Tower_System_DTO;

namespace Control_Tower_System_BLL
{
    internal interface IControlTower
    {
        void CreateFlight(string id, string airline, string destination, double duration, double flightHeight, bool inFlight, TimeOnly time);
        void AddFlight(Flight flight);
        void TakeOff(Flight flight);
        void Land(Flight flight);
        void ChangeAltitude(Flight flight);
    }
}
