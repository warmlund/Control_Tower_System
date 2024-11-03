using Control_Tower_System_BLL;

namespace Control_Towe_System_BLL_Tests
{
    [TestClass]
    public class ControlTowerTest
    {
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
            controlTower = new ControlTower();
            controlTower.CreateFlight(testFlightId, testAirline, testDestination, testDuration);

            bool isEventTriggered = false;
            FlightHeightEventArgs heightEventArgs = null;

            controlTower.FlightAltitudeChanged += (s, e) =>
            {
                isEventTriggered = true;
                heightEventArgs = e;
            };

            //Act
            controlTower.ChangeAltitude(testFlightIndex, testAltitude);

            //Assert
            Assert.AreEqual(heightEventArgs.Flight.FlightAltitude, testAltitude, "Altitude was not changed to new altitude");
            Assert.IsTrue(isEventTriggered, "Event FlightAltitudeChanged was not triggered");
        }

        [TestMethod]
        public void CreateFlightTest()
        {
            //Arrange
            controlTower = new ControlTower();
            int numberOfFlightsInStorage = controlTower.FStorage.Count;

            //Act
            controlTower.CreateFlight(testFlightId, testAirline, testDestination, testDuration);

            //Assert
            Assert.IsTrue(numberOfFlightsInStorage==controlTower.FStorage.Count-1, "Flight is not added to storage");
        }
    }
}