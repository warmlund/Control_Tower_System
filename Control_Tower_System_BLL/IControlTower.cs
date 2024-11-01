﻿using Control_Tower_System_DTO;

namespace Control_Tower_System_BLL
{
    internal interface IControlTower
    {
        void AddFlight();
        void CreateFlight(string id, string airline, string destination, double duration, double flightHeight, bool inFlight, TimeOnly time);
        void OnFlightTakingOff(object sender, FlightTakeOffEventArgs e);
        void OnFlightLanding(object sender, FlightLandedEventArgs e);
        void OnChangingAltitude(object sender, FlightHeightEventArgs e);
    }
}
