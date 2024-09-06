using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeApi.Data.Queries
{
    public class UpdateRoomQuery
    {
        public string Name { get; set; }
        public int? Area { get; set; }
        public bool? GasConnected { get; set; }
        public int? Voltage { get; set; }

        public UpdateRoomQuery(string name = null, int? area = null, bool? gasConnected = null, int? voltage = null) 
        { 
            Name= name;
            Area = area;
            Voltage = voltage;
            GasConnected = gasConnected;
        }
    }
}
