using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Control_Tower_System_DAL;
using Control_Tower_System_DTO;

namespace Control_Tower_System_BLL
{
    public class FlightStorage : ListManager<Flight>
    {
        public override string[] ToStringArray()
        {
            var flightArray = new string[_list.Count];

            for (int i = 0; i < _list.Count; i++)
            {
                flightArray[i] = _list[i].ToString();
            }
            return flightArray;
        }
    }
}
