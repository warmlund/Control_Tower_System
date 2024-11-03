using Control_Tower_System_BLL;

namespace Control_Tower_System_BLL_Tests
{
    [TestClass]
    public class FlightManagerTest
    {
        private FlightManager flightManager;

        /// <summary>
        /// Creates a test initialize class
        /// creates a instance variable of the flightmanager class
        /// and creates a test flight
        /// These are used for all test methods
        /// </summary>
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
            bool isEventTriggered = false; //Sets bool for tracking event trigger to false
            FlightTakeOffEventArgs takeOffEventArgs = null; //sets FlightTakeOffEventArgs variable to null

            flightManager.TakingOff += (s, e) =>  // Attach an event handler
            {
                isEventTriggered = true; //if the event is invoked in the test method, this will set to true
                takeOffEventArgs = e; //if the event is invoked in the test method, the eventargs variable will be set to the eventarg in the eventhandler
            };

            //Act
            flightManager.OnTakeOff(); //Calls the method

            //Assert
            Assert.IsTrue(isEventTriggered, "Event TakingOff not triggered"); //Asserts that the event has been triggered by checking if the boolean has been set to true
        }

        [TestMethod]
        public void OnLandingTest()
        {
            //Arrange
            bool isEventTriggered = false;  // Attach an event handler to the FlightAltitudeChanged event
            FlightLandedEventArgs landingEventArgs = null; //Sets FlightLandingEventArgs to null

            flightManager.Landing += (s, e) => //Attach event handler
            {
                isEventTriggered = true; //if the event is invoked in the test method, this will set to true
                landingEventArgs = e; //if the event is invoked in the test method, the eventargs variable will be set to the eventarg in the eventhandler
            };

            flightManager.OnTakeOff(); //calling taking off so we can call the landing mehtod

            //Act
            flightManager.OnLanding(); //Calls the method

            //Assert
            Assert.IsTrue(isEventTriggered, "Event Landing not triggered"); //Asserts that the event has been triggered by checking if the boolean has been set to true
        }
    }
}
