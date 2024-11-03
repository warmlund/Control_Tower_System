using Control_Tower_System_BLL;
using Control_Tower_System_DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control_Tower_System_BLL_Tests
{
    [TestClass]
    public class FlightStorageTest
    {
        private ControlTower controlTower;

        [TestInitialize]
        public void Setup()
        {
            controlTower = new ControlTower();
            controlTower.CreateFlight("TestAirlineId", "TestAirline", "TestDestination", 1.0);
        }

        [TestMethod]
        public void GetAllFlightsTest()
        {
            //Arrangements made in setup
            
            //Act
            List<Flight> flights= controlTower.FStorage.GetAllFlights();

            //Assert
            Assert.IsTrue(flights.Count > 0, "Flight list is empty");
        }

        [TestMethod]
        public void RemoveAtTest()
        {
            //Arrange
            int flightIndex = 0;
            int flightCount=controlTower.FStorage.Count;

            //Act
            controlTower.FStorage.RemoveAt(0);

            //Assert
            Assert.IsTrue(flightCount == controlTower.FStorage.Count + 1, "Flight is not removed");
        }
    }
}
