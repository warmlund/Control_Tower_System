using Control_Tower_System_BLL;

namespace Control_Tower_System_BLL_Tests
{
    [TestClass]
    public class ControlTowerTest
    {
        /// <summary>
        /// Sets private instance variables used in both test methods
        /// </summary>
        private const string testFlightId = "FlightId";
        private const string testAirline = "Airline";
        private const string testDestination = "Destination";
        private const double testDuration = 1;
        private const double testAltitude = 5000;
        private const int testFlightIndex = 0;
        private ControlTower controlTower;

        [TestMethod]
        public void ChangeAltitudeTest()
        {
            //Arrange 
            //creates instance of controlTower and creates a testflight
            controlTower = new ControlTower();
            controlTower.CreateFlight(testFlightId, testAirline, testDestination, testDuration);

            bool isEventTriggered = false; //Sets bool for tracking event trigger to false
            FlightHeightEventArgs heightEventArgs = null; //sets FlightHeightEventArgs variable to null

            controlTower.FlightAltitudeChanged += (s, e) => //Sets event handler to flightAltitudeChanged event
            {
                isEventTriggered = true; //if the event is invoked in the test method, this will set to true
                heightEventArgs = e; //if the event is invoked in the test method, the eventargs variable will be set to the eventarg in the eventhandler
            };

            //Act
            controlTower.ChangeAltitude(testFlightIndex, testAltitude); //Runs the method

            //Assert
            Assert.AreEqual(heightEventArgs.Flight.FlightAltitude, testAltitude, "Altitude was not changed to new altitude"); //Asserts if the altitude has been changed by retrieving the flight altitude in the eventargs
            Assert.IsTrue(isEventTriggered, "Event FlightAltitudeChanged was not triggered"); //Asserts that the event has been triggered by checking if the boolean has been set to true
        }

        [TestMethod]
        public void CreateFlightTest()
        {
            //Arrange
            //Creates instance of the Control tower class
            //Gets the current amount of flights in the flightstorage
            controlTower = new ControlTower();
            int numberOfFlightsInStorage = controlTower.FStorage.Count;

            //Act
            //Calls the createflight class
            controlTower.CreateFlight(testFlightId, testAirline, testDestination, testDuration);

            //Assert
            //Asserts if the original storage count is the same as the new count after the method has been called minus 1
            Assert.IsTrue(numberOfFlightsInStorage == controlTower.FStorage.Count - 1, "Flight is not added to storage"); 
        }
    }
}