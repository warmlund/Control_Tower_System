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
        private ObservableCollection<Flight> _flightList;
        private ObservableCollection<string> _statusList;
        private Flight _currentSelectedFlight;
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
        public Flight CurrentSelectedFlight { get { return _currentSelectedFlight; } set { if (_currentSelectedFlight != value) { _currentSelectedFlight = value; OnPropertyChanged(nameof(CurrentSelectedFlight)); } } }
        public string Airline { get { return _airline; } set { if (_airline != value) { _airline = value; OnPropertyChanged(nameof(Airline)); } } }
        public string AirlineId { get { return _airlineId; } set { if (_airlineId != value) { _airlineId = value; OnPropertyChanged(nameof(AirlineId)); } } }
        public string Destination { get { return _destination; } set { if (_destination != value) { _destination = value; OnPropertyChanged(nameof(Destination)); } } }
        public string ProposedDuration { get { return _proposedDuration; } set { if (_proposedDuration != value) { _proposedDuration = value; OnPropertyChanged(nameof(ProposedDuration)); IsDurationCorrectFormat(); } } }
        public string ProposedAltitude { get { return _proposedAltitude; } set { if (_proposedAltitude != value) { _proposedAltitude = value; OnPropertyChanged(nameof(ProposedAltitude)); IsAltitudeCorrectFormat(); } } }
        #endregion
        public ViewModel(ControlTower controlTower)
        {
            _controlTower = controlTower;
            _flightList = new ObservableCollection<Flight>();
            _currentSelectedFlight = null;
            _newAltitude = 0.0;
            _duration = 0.0;

            AddFlight = new Command(AddNewFlight, CanAddNewFlight);
            TakeOff = new Command(FlightTakeOff, CanFlightTakeOff);
            ChangeAltitude = new Command(ChangeSelectedFlightAltitude, CanChangeSelectedFlightAltitude);

            _controlTower.FlightTakingOff += OnFlightTakingOff;
            _controlTower.FlightLanding += OnFlightLanding;
            _controlTower.FlightAltitudeChanged += OnFlightAltitudeChanged;
        }

        public void UpdateCollection()
        {
            FlightList.Clear();
            foreach (var flight in _controlTower.GetAllFlights())
            {
                FlightList.Add(flight);
            }
        }

        private bool CanChangeSelectedFlightAltitude()
        {
            if (CurrentSelectedFlight != null && IsAltitudeCorrectFormat())
                return true;
            return false;
        }

        private void ChangeSelectedFlightAltitude()
        {
            _controlTower.OrderAltitudeChange(_newAltitude);
        }

        private bool CanFlightTakeOff()
        {
            if (_currentSelectedFlight != null && _flightList.Count > 0)
                return true;
            return false;
        }

        private void FlightTakeOff()
        {
            _controlTower.OrderTakeOff();
        }

        private bool CanAddNewFlight()
        {
            if (Airline != string.Empty && AirlineId != string.Empty &&
                Destination != string.Empty && IsDurationCorrectFormat())
                return true;
            return false;
        }

        private void AddNewFlight()
        {
            _controlTower.CreateFlight(AirlineId, Airline, Destination, _duration);
            UpdateCollection();
        }

        private void OnFlightTakingOff(object sender, FlightTakeOffEventArgs e)
        {
            StatusList.Add(e.Message);
        }

        private void OnFlightLanding(object sender, FlightLandedEventArgs e)
        {
            StatusList.Add(e.Message);
        }

        private void OnFlightAltitudeChanged(object sender, FlightHeightEventArgs e)
        {
            StatusList.Add(e.Message);
        }

        private bool IsAltitudeCorrectFormat()
        {
            if (ConverterUtils.StringToDouble(ProposedAltitude, out _newAltitude, 0, 100))
                return true;
            return false;
        }

        private bool IsDurationCorrectFormat()
        {
            if (ConverterUtils.StringToDouble(ProposedDuration, out _duration, 0, 100))
                return true;
            return false;
        }
    }
}
