﻿using Control_Tower_System_DTO;
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

        public event EventHandler<FlightTakeOffEventArgs> TakingOff;
        public event EventHandler<FlightLandedEventArgs> Landing;
        public event EventHandler<FlightHeightEventArgs> AltitudeChanging;

        public Flight CurrentFlight {  get; private set; }

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

        public void OnTakeOff()
        {
            SetupTimer();
            CurrentFlight.InFlight = true;
            CurrentFlight.LocalTime=TimeOnly.FromDateTime(DateTime.Now);

            TakingOff?.Invoke(CurrentFlight, new FlightTakeOffEventArgs(CurrentFlight));
        }

        public void OnLanding()
        {
            _dispatcher.Stop();
            CurrentFlight.LocalTime = CurrentFlight.LocalTime.AddHours(CurrentFlight.Duration);
            CurrentFlight.InFlight= false;
            Landing?.Invoke(CurrentFlight, new FlightLandedEventArgs(CurrentFlight));
        }

        public void OnAltitudeChange(double altitude)
        {
            double oldAltitude = CurrentFlight.FlightAltitude;
            CurrentFlight.FlightAltitude = altitude;
            AltitudeChanging?.Invoke(CurrentFlight, new FlightHeightEventArgs(CurrentFlight, oldAltitude));
        }

        private void SetupTimer()
        {
            _dispatcher= new DispatcherTimer();
            _dispatcher.Tick += new EventHandler(OnTimerTicking);
            _dispatcher.Interval = new TimeSpan(0, 0, 1);
           _dispatcher.Start();
        }

        public void OnTimerTicking(object? sender, EventArgs e)
        {
            TimeOnly currentTime= TimeOnly.FromDateTime(DateTime.Now);
            double timeLeft=(currentTime-CurrentFlight.LocalTime).TotalSeconds;

            if(timeLeft >= CurrentFlight.Duration)
            {
                OnLanding();
            }      
        }

        private double GenerateRandomHeight()
        {
            var random = new Random();
            return random.Next(0, 10000);
        }
    }
}
