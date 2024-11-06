using Control_Tower_System_BLL;
using Control_Tower_System_DTO;
using System.Collections.ObjectModel;
using UtilitiesLib.Commands;
using UtilitiesLib.Converters;
using UtilitiesLib.Notifiers;

namespace Control_Tower_System_PL
{
    public class ViewModel : NotifyPropertyChanged
    {
        #region variables
        private ControlTower _controlTower;
        private FlightManager _flightManager;
        private ObservableCollection<Flight> _flightList;
        private ObservableCollection<string> _statusList;
        private int _currentSelectedFlightIndex;
        private string _airline;
        private string _destination;
        private string _airlineId;
        private string _proposedDuration;
        private string _proposedAltitude;
        private double _newAltitude;
        private double _duration;
        #endregion variables

        #region commands
        public Command AddFlight { get; private set; }
        public Command TakeOff { get; private set; }
        public Command ChangeAltitude { get; private set; }
        #endregion

        #region properties
        public ObservableCollection<Flight> FlightList { get { return _flightList; } set { if (_flightList != value) { _flightList = value; OnPropertyChanged(nameof(FlightList)); } } }
        public ObservableCollection<string> StatusList { get { return _statusList; } set { if (_statusList != value) { _statusList = value; OnPropertyChanged(nameof(StatusList)); } } }
        public int CurrentSelectedFlightIndex { get { return _currentSelectedFlightIndex; } set { if (_currentSelectedFlightIndex != value) { _currentSelectedFlightIndex = value; OnPropertyChanged(nameof(CurrentSelectedFlightIndex));} } }
        public string Airline { get { return _airline; } set { if (_airline != value) { _airline = value; OnPropertyChanged(nameof(Airline)); AddFlight.RaiseCanExecuteChanged(); } } }
        public string AirlineId { get { return _airlineId; } set { if (_airlineId != value) { _airlineId = value; OnPropertyChanged(nameof(AirlineId)); AddFlight.RaiseCanExecuteChanged(); } } }
        public string Destination { get { return _destination; } set { if (_destination != value) { _destination = value; OnPropertyChanged(nameof(Destination)); AddFlight.RaiseCanExecuteChanged(); } } }
        public string ProposedDuration { get { return _proposedDuration; } set { if (_proposedDuration != value) { _proposedDuration = value; OnPropertyChanged(nameof(ProposedDuration)); IsDurationCorrectFormat(); AddFlight.RaiseCanExecuteChanged(); } } }
        public string ProposedAltitude { get { return _proposedAltitude; } set { if (_proposedAltitude != value) { _proposedAltitude = value; OnPropertyChanged(nameof(ProposedAltitude)); IsAltitudeCorrectFormat(); ChangeAltitude.RaiseCanExecuteChanged(); } } }
        #endregion
        public ViewModel(ControlTower controlTower, FlightManager flightManager)
        {
            _controlTower = controlTower;
            _flightManager = flightManager;
            _flightList = new ObservableCollection<Flight>();
            _statusList = new ObservableCollection<string>();
            _currentSelectedFlightIndex = 0;
            _airline=string.Empty;
            _airlineId=string.Empty;
            _destination = string.Empty;
            _proposedAltitude=string.Empty;
            _newAltitude = 0.0;
            _duration = 0.0;

            AddFlight = new Command(AddNewFlight, CanAddNewFlight);
            TakeOff = new Command(FlightTakeOff, CanFlightTakeOff);
            ChangeAltitude = new Command(ChangeSelectedFlightAltitude, CanChangeSelectedFlightAltitude);

            //Subscribes to the events in the bll layer
            _controlTower.FlightTakingOff += OnFlightTakingOff;
            _controlTower.FlightLanding += OnFlightLanding;
            _controlTower.FlightAltitudeChanged += OnFlightAltitudeChanged;
        }

        /// <summary>
        /// Updates the observablecollection 
        /// </summary>
        public void UpdateCollection()
        {
            FlightList.Clear();
            foreach (Flight flight in _controlTower.GetAllFlights())
            {
                FlightList.Add(flight);
            }
        }

        /// <summary>
        /// Checks if the user can change flight altitude
        /// </summary>
        /// <returns></returns>
        private bool CanChangeSelectedFlightAltitude()
        {
            if (CurrentSelectedFlightIndex >=0 && IsAltitudeCorrectFormat() && _controlTower.FStorage.GetFlight(_currentSelectedFlightIndex).InFlight == true)
                return true;
            return false;
        }

        /// <summary>
        /// Changes the current selected flights altitude
        /// </summary>
        private void ChangeSelectedFlightAltitude()
        {
            _controlTower.ChangeAltitude(CurrentSelectedFlightIndex, _newAltitude);
            ResetInput();
        }

        /// <summary>
        /// Checks if a flight can take off
        /// If the list of flights is not empty and a flight is selected the flight can take off
        /// </summary>
        /// <returns></returns>
        private bool CanFlightTakeOff()
        {
            if (_currentSelectedFlightIndex>=0 && _flightList.Count > 0 && _controlTower.FStorage.GetFlight(_currentSelectedFlightIndex).InFlight!=true)
                return true;
            return false;
        }

        /// <summary>
        /// Orders the flight to take off
        /// </summary>
        private void FlightTakeOff()
        {
            _controlTower.OrderTakeOff(CurrentSelectedFlightIndex);
            TakeOff.RaiseCanExecuteChanged();
            ChangeAltitude.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Checks if the user can add a new flight
        /// all input must be filled and the duration must be in correct format
        /// </summary>
        /// <returns></returns>
        private bool CanAddNewFlight()
        {
            if (_airline!=string.Empty && _airlineId != string.Empty &&
                _destination != string.Empty && IsDurationCorrectFormat())
                return true;
            return false;
        }

        /// <summary>
        /// Adds a new flight
        /// Resets the user input
        /// Updates the observablecollection
        /// </summary>
        private void AddNewFlight()
        {
            _controlTower.CreateFlight(AirlineId, Airline, Destination, _duration);
            ResetInput();
            UpdateCollection();
        }

        /// <summary>
        /// Resets the user input after a flight as been added or altitude changed
        /// </summary>
        private void ResetInput()
        {
            Destination = string.Empty;
            ProposedDuration = string.Empty;
            Airline = string.Empty;
            AirlineId = string.Empty;
            ProposedAltitude = string.Empty;
        }

        /// <summary>
        /// Handles the event when the flight takes off
        /// </summary>
        /// <param name="e">The message added to the status list</param>
        private void OnFlightTakingOff(object sender, FlightTakeOffEventArgs e)
        {
            StatusList.Add(e.Message);
        }

        /// <summary>
        /// Handles the event when the flight lands
        /// </summary>
        /// <param name="e">The message added to the status list</param>
        private void OnFlightLanding(object sender, FlightLandedEventArgs e)
        {
            StatusList.Add(e.Message);
        }

        /// <summary>
        /// Handles the event when the flight changes altitude
        /// </summary>
        /// <param name="e">The message added to the status list</param>
        private void OnFlightAltitudeChanged(object sender, FlightHeightEventArgs e)
        {
            StatusList.Add(e.Message);
        }

        /// <summary>
        /// Checks if the altitude has a correct format
        /// Uses the converterutils library
        /// </summary>
        /// <returns></returns>
        private bool IsAltitudeCorrectFormat()
        {
            if (ConverterUtils.StringToDouble(ProposedAltitude, out _newAltitude, 0, 10000))
                return true;
            return false;
        }

        /// <summary>
        /// Checks if the duration has a correct format
        /// Uses the converterutils library
        /// </summary>
        /// <returns></returns>
        private bool IsDurationCorrectFormat()
        {
            if (ConverterUtils.StringToDouble(ProposedDuration, out _duration, 0, 100))
                return true;
            return false;
        }
    }
}
