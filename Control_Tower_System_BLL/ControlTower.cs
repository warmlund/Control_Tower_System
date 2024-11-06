using Control_Tower_System_DTO;

namespace Control_Tower_System_BLL
{
    /// <summary>
    /// Control tower class 
    /// Handles storage of flights, ordering takeoffs and altitude changes
    /// </summary>
    public class ControlTower
    {
        private FlightStorage _flightStorage; //Stores all the flights
        public FlightStorage FStorage { get { return _flightStorage; } }

        public delegate void ChangeAltitudeDelegate(double altitudeValue); // Delegate for changing the altitude of a specific flight

        /// <summary>
        /// Events that triggers when the flight takes off, lands or the altitude is adjusted
        /// </summary>
        public event EventHandler<FlightTakeOffEventArgs> FlightTakingOff;
        public event EventHandler<FlightLandedEventArgs> FlightLanding;
        public event EventHandler<FlightHeightEventArgs> FlightAltitudeChanged;

        public ControlTower()
        {
            _flightStorage = new FlightStorage();
        }

        /// <summary>
        /// Creates a flightmanager instance and creates a flight within it
        /// </summary>
        /// <param name="id">airline id</param>
        /// <param name="airline">airline name</param>
        /// <param name="destination">flight destination</param>
        /// <param name="duration">flight duration</param>
        public void CreateFlight(string id, string airline, string destination, double duration)
        {
            FlightManager flightManager = new FlightManager();
            flightManager.CreateFlight(id, airline, destination, duration);

            //Subscribes to the managers events
            flightManager.TakingOff += OnFlightTakingOff;
            flightManager.Landing += OnFlightLanding;
            flightManager.AltitudeChanging += OnChangingAltitude;

            //adds the instance to the storage
            _flightStorage.Add(flightManager);
        }

        /// <summary>
        /// Orders flight to change altitude
        /// </summary>
        /// <param name="flightId">the flight index</param>
        /// <param name="altitude">the user input altitude</param>
        public void ChangeAltitude(int flightId, double altitude)
        {
            FlightManager flightManager = _flightStorage.Get(flightId);

            // Define the delegate to encapsulate the OnAltitudeChange method
            ChangeAltitudeDelegate changeAltitudeDelegate = flightManager.OnAltitudeChange;

            // Call the delegate
            changeAltitudeDelegate(altitude);
        }

        /// <summary>
        /// Orders a flight to take off
        /// </summary>
        /// <param name="flightId">the flight index in the storage</param>
        public void OrderTakeOff(int flightId)
        {
            FlightManager flightManager = _flightStorage.Get(flightId);
            flightManager?.OnTakeOff();
        }

        /// <summary>
        /// Handles the event when a flight takes off, invoking the FlightTakingOff event.
        /// </summary>
        private void OnFlightTakingOff(object sender, FlightTakeOffEventArgs e)
        {
            FlightTakingOff?.Invoke(this, e);
        }

        /// <summary>
        /// Handles the event when a flight lands, invoking the FlightLanding event.
        /// </summary>
        private void OnFlightLanding(object sender, FlightLandedEventArgs e)
        {
            FlightLanding?.Invoke(this, e);
        }

        /// <summary>
        /// Handles the event when a flight changes altitude, invoking the FlightAltitudeChanging event.
        /// </summary>
        private void OnChangingAltitude(object sender, FlightHeightEventArgs e)
        {
            FlightAltitudeChanged?.Invoke(this, e);
        }

        /// <summary>
        /// gets all flights in the storage as a list
        /// </summary>
        /// <returns>a list of all flights</returns>
        public List<Flight> GetAllFlights()
        {
            return _flightStorage.GetAllFlights();
        }
    }
}
