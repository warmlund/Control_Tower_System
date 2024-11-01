using Control_Tower_System_DTO;

namespace Control_Tower_System_BLL
{
    public class ControlTower : IControlTower
    {
        private FlightStorage _flightStorage;
        private FlightManager _flightManager;

        //events for notifying the PL layer
        public event EventHandler<FlightTakeOffEventArgs> FlightTakingOff;
        public event EventHandler<FlightLandedEventArgs> FlightLanding;
        public event EventHandler<FlightHeightEventArgs> FlightAltitudeChanged;

        public delegate void ChangeAltitudeDelegate(double altitudeValue);

        public ControlTower()
        {
            _flightStorage = new FlightStorage();
            _flightManager = new FlightManager();

            _flightManager.TakingOff += OnFlightTakingOff;
            _flightManager.Landing += OnFlightLanding;
            _flightManager.ChangingAltitude += OnChangingAltitude;
        }

        public void AddFlight()
        {
            _flightStorage.Add(_flightManager.CurrentFlight);
        }

        public void OrderTakeOff()
        {
            _flightManager.TakeOff();
        }

        public void OrderAltitudeChange(double altitude)
        {
            ChangeAltitudeDelegate changeAltitude = _flightManager.ChangeAltitude;
        }

        public void ChangeCurrentFlight(int index)
        {
            Flight selectedFlight = _flightStorage.Get(index);
            _flightManager.CurrentFlight = selectedFlight;
        }

        public List<Flight> GetAllFlights()
        {
            return _flightStorage.GetAllFlights();
        }
        public void CreateFlight(string id, string airline, string destination, double duration)
        {
            _flightManager.CreateFlight(id, airline, destination, duration);
        }

        public void OnFlightTakingOff(object sender, FlightTakeOffEventArgs e)
        {
            FlightTakingOff?.Invoke(this, e);
        }

        public void OnFlightLanding(object sender, FlightLandedEventArgs e)
        {
            FlightLanding?.Invoke(this, e);
        }

        public void OnChangingAltitude(object sender, FlightHeightEventArgs e)
        {
            FlightAltitudeChanged?.Invoke(this, e);
        }
    }
}
