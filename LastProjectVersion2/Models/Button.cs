using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastProjectVersion2.Models
{
    public class Button
    {
        public EFloor Floor { get; set; }
        public bool Disabled { get; set; }
        public Button(EFloor floor)
        {
            Floor = floor;
            Disabled = true;
        }
        public void CallElevator(Agent agent,Elevator elevator)
        {
            Console.WriteLine($"Agent{agent.Id} call elevator");
            if (elevator.TryCall())
            {
                ElevatorGo(agent, elevator);
            }
        }
        public void ElevatorGo(Agent agent, Elevator elevator)
        {
            elevator.Go(this, agent);
        }
    }
}
