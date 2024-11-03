using Control_Tower_System_DTO;
using System.Windows.Threading;

namespace Control_Tower_System_BLL
{
    /// <summary>
    /// This class handles two delegate events for flight takeoff and landing
    /// "Blueprint" for Flight is in the flight class in the DTO layer
    /// </summary>
    public class FlightManager
    {
        private DispatcherTimer _dispatcher;

        public event EventHandler<FlightTakeOffEventArgs> TakingOff; //Event triggers when the flight takes off
        public event EventHandler<FlightLandedEventArgs> Landing; //Event triggers when the flight lands
        public event EventHandler<FlightHeightEventArgs> AltitudeChanging; //Event triggers when the altitude changes

        public Flight CurrentFlight {  get; private set; } //Holds the flight information

        public void CreateFlight(string id, string airline, string destination, double duration)
        {
           CurrentFlight= new Flight
            {
                AirlineId = id,
                Airline = airline,
                Destination = destination,
                Duration = duration,
                FlightAltitude = GenerateRandomHeight(),
                InFlight = false,
                LocalTime = TimeOnly.FromDateTime(DateTime.Now)
            };
        }

        /// <summary>
        /// Initiates the take off of the flight
        /// starts a timer simulating the flight duration
        /// Sets the inflight to true
        /// Sets local time to current time
        /// Raises the takeoff event
        /// </summary>
        public void OnTakeOff()
        {
            SetupTimer();
            CurrentFlight.InFlight = true;
            CurrentFlight.LocalTime=TimeOnly.FromDateTime(DateTime.Now);
            TakingOff?.Invoke(CurrentFlight, new FlightTakeOffEventArgs(CurrentFlight));
        }

        /// <summary>
        /// Initiates landing of flight
        /// stops the timer
        /// Adds the duration of the flight to the flight's local time property
        /// sets inflight to false
        /// Raises the landing event
        /// </summary>
        public void OnLanding()
        {
            _dispatcher.Stop();
            CurrentFlight.LocalTime = CurrentFlight.LocalTime.AddHours(CurrentFlight.Duration);
            CurrentFlight.InFlight= false;
            Landing?.Invoke(CurrentFlight, new FlightLandedEventArgs(CurrentFlight));
        }

        /// <summary>
        /// Initiate the altitude change of the flight
        /// </summary>
        /// <param name="altitude">the altitude variable from user input</param>
        public void OnAltitudeChange(double altitude)
        {
            double oldAltitude = CurrentFlight.FlightAltitude; //Stores the old altitude
            CurrentFlight.FlightAltitude = altitude; //Sets new altitude
            AltitudeChanging?.Invoke(CurrentFlight, new FlightHeightEventArgs(CurrentFlight, oldAltitude)); //Invokes the event
        }

        /// <summary>
        /// Setups a timer for tracking the flight duration
        /// </summary>
        private void SetupTimer()
        {
            _dispatcher= new DispatcherTimer();
            _dispatcher.Tick += (s, e) =>
            {
                TimeOnly currentTime = TimeOnly.FromDateTime(DateTime.Now);
                double timeLeft = (currentTime - CurrentFlight.LocalTime).TotalSeconds;

                if (timeLeft >= CurrentFlight.Duration)
                {
                    OnLanding();
                }
            };
           /* _dispatcher.Tick += new EventHandler(OnTimerTicking); *///assicoiate the tick event with the tick method
            _dispatcher.Interval = new TimeSpan(0, 0, 1);
           _dispatcher.Start();
        }

        /// <summary>
        /// Generates a random altitude height between 0 and 10 000
        /// </summary>
        /// <returns>a random double</returns>
        private double GenerateRandomHeight()
        {
            var random = new Random();
            return random.Next(0, 10000);
        }
    }
}
