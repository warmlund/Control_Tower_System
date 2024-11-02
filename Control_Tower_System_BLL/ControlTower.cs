using Control_Tower_System_DTO;

namespace Control_Tower_System_BLL
{
    public class ControlTower
    {
        private FlightStorage _flightStorage;

        public delegate void ChangeAltitudeDelegate(int flightId, double altitudeValue);

        public event EventHandler<FlightTakeOffEventArgs> FlightTakingOff;
        public event EventHandler<FlightLandedEventArgs> FlightLanding;
        public event EventHandler<FlightHeightEventArgs> FlightAltitudeChanged;

        public ControlTower()
        {
            _flightStorage = new FlightStorage();
        }

        public void OrderTakeOff(int flightId)
        {
            var flightManager = _flightStorage.Get(flightId);
            flightManager?.OnTakeOff();
        }

        public void OnNewAltitude(int flightId, double altitude)
        {
            Flight currentFlight = _flightStorage.GetFlight(flightId);
            new ChangeAltitudeDelegate(ChangeAltitude)(flightId,altitude);
        }

        public List<Flight> GetAllFlights()
        {
            return _flightStorage.GetAllFlights();
        }

        public void CreateFlight(string id, string airline, string destination, double duration)
        {
            var flightManager = new FlightManager();
            flightManager.CreateFlight(id, airline, destination, duration);

            flightManager.TakingOff += OnFlightTakingOff;
            flightManager.Landing += OnFlightLanding;
            flightManager.AltitudeChanging += OnChangingAltitude;

            _flightStorage.Add(flightManager); 
        }

        public void ChangeAltitude(int flightId, double altitude)
        {
            var flightManager = _flightStorage.Get(flightId);
            flightManager?.OnAltitudeChange(altitude);
        }

        private void OnFlightTakingOff(object sender, FlightTakeOffEventArgs e)
        {
            FlightTakingOff?.Invoke(this, e);
        }

        private void OnFlightLanding(object sender, FlightLandedEventArgs e)
        {
            FlightLanding?.Invoke(this, e);
        }

        private void OnChangingAltitude(object sender, FlightHeightEventArgs e)
        {
            FlightAltitudeChanged?.Invoke(this, e);
        }
    }
}
