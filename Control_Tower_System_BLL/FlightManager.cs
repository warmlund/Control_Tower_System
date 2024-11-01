using Control_Tower_System_DTO;
using System.Windows.Threading;

namespace Control_Tower_System_BLL
{
    /// <summary>
    /// This class handles two delegate events for flight takeoff and landing
    /// "Blueprint" for Flight is in the flight class in the DTO layer
    /// </summary>
    internal class FlightManager
    {
        private DispatcherTimer _dispatcher;

        public event EventHandler<FlightTakeOffEventArgs> TakingOff;
        public event EventHandler<FlightLandedEventArgs> Landing;
        public event EventHandler<FlightHeightEventArgs> ChangingAltitude;

        public Flight CurrentFlight {  get; set; }

        public void CreateFlight(string id, string airline, string destination, double duration, double flightHeight, bool inFlight, TimeOnly time)
        {
           CurrentFlight= new Flight
            {
                AirlineId = id,
                Airline = airline,
                Destination = destination,
                Duration = duration,
                FlightAltitude = flightHeight,
                InFlight = inFlight,
                LocalTime = time
            };
        }

        public void TakeOff() => OnTakeOff();
        public void Land() => OnLanding();
        public void ChangeAltitude() => OnAltitudeChange();

        private void OnTakeOff()
        {
            SetupTimer();
            CurrentFlight.InFlight = true;
            CurrentFlight.LocalTime=TimeOnly.FromDateTime(DateTime.Now);

            TakingOff?.Invoke(CurrentFlight, new FlightTakeOffEventArgs(CurrentFlight));
        }

        private void OnLanding()
        {
            SetupTimer();
            CurrentFlight.InFlight= false;
            Landing?.Invoke(CurrentFlight, new FlightLandedEventArgs(CurrentFlight));
        }

        private void OnAltitudeChange()
        {
            ChangingAltitude?.Invoke(CurrentFlight, new FlightHeightEventArgs(CurrentFlight));
        }

        public void SetupTimer()
        {
            _dispatcher= new DispatcherTimer();
            _dispatcher.Tick += new EventHandler(OnTimerTicking);
            _dispatcher.Interval = new TimeSpan(0, 0, 1);
           _dispatcher.Start();
        }

        public void StopTimer()
        {
            CurrentFlight.LocalTime= TimeOnly.FromDateTime(DateTime.Now);
            _dispatcher.Stop();
        }

        public void OnTimerTicking(object? sender, EventArgs e)
        {
            TimeOnly currentTime= TimeOnly.FromDateTime(DateTime.Now);
            double timeLeft=(currentTime-CurrentFlight.LocalTime).TotalSeconds;

            if(timeLeft >= CurrentFlight.Duration)
                OnLanding();
        }
    }
}
