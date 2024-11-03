using Control_Tower_System_BLL;

namespace Control_Towe_System_BLL_Tests
{
    [TestClass]
    public class FlightManagerTest
    {
        private FlightManager flightManager;

        [TestInitialize]
        public void Setup()
        {
            flightManager = new FlightManager();
            flightManager.CreateFlight("TesFlightId", "TestAirline", "TestDestination", 1.0); 
        }

        [TestMethod]
        public void OnTakeOffTest()
        {
            //Arrange
            bool isEventTriggered = false;
            FlightTakeOffEventArgs takeOffEventArgs = null;

            flightManager.TakingOff += (s, e) =>
            {
                isEventTriggered = true;
                takeOffEventArgs = e;
            };

            //Act
            flightManager.OnTakeOff();

            //Assert
            Assert.IsTrue(isEventTriggered, "Event TakingOff not triggered");
        }

        [TestMethod]
        public void OnLandingTest()
        {
            //Arrange
            bool isEventTriggered = false;
            FlightLandedEventArgs landingEventArgs = null;

            flightManager.Landing += (s, e) =>
            {
                isEventTriggered = true;
                landingEventArgs = e;
            };

            flightManager.OnTakeOff(); //calling taking off so we can call the landing mehtod

            //Act
            flightManager.OnLanding();

            //Assert
            Assert.IsTrue(isEventTriggered, "Event Landing not triggered");
        }
    }
}
