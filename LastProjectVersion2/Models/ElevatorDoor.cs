using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastProjectVersion2.Models
{
    public enum DoorStatus { Open, Close }

    public class ElevatorDoor
    {
        private readonly Dictionary<SecurityLevel, List<EFloor>> permissions = new Dictionary<SecurityLevel, List<EFloor>>()
        {
            {SecurityLevel.Confidential,new List<EFloor>(){ EFloor.G} },
            {SecurityLevel.Secret,new List<EFloor>(){ EFloor.G, EFloor.S} },
            {SecurityLevel.TopSecret,new List<EFloor>(){ EFloor.G, EFloor.S, EFloor.T1, EFloor.T2 } }
        };

        public DoorStatus DoorStatus { get; set; }
        public Elevator Elevator { get; set; }
        public ElevatorDoor(Elevator elevator)
        {
            Elevator = elevator;
        }
        public bool isOpened(Agent agent)
        {
            var permission = permissions[agent.SecurityLevel].Where(f => f == Elevator.CurrentFloor).ToList();

            if(permission.Count == 0)
            {
                return false;
            }
            return true;
        }
    }
}
