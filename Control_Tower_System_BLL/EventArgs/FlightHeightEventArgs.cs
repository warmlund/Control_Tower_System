﻿using Control_Tower_System_DTO;

namespace Control_Tower_System_BLL
{
    /// <summary>
    /// Eventargs for flight altitude
    /// </summary>
    public class FlightHeightEventArgs : FlightEventArgs
    {
        public string Message { get; }

        public FlightHeightEventArgs(Flight flight, double oldAltitude) : base(flight)
        {
            Message = $"Flight {flight.Airline} ({flight.AirlineId}) has changed altitude from {oldAltitude} to {flight.FlightAltitude}";
        }
    }
}
