using LastProjectVersion2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastProjectVersion2
{
    public class Area51
    {
        public List<Floor> Floors { get; set; }
        public List<Thread> AgentsThread { get; set; }
        public Elevator Elevator { get; set; }
        public Area51()
        {
            Floors = new List<Floor>();
            foreach (var i in Enum.GetValues(typeof(EFloor)))
            {
                Floors.Add(new Floor((EFloor)i));
            }
            Elevator = new Elevator();
            AgentsThread = new List<Thread>();
        }
        public void Start()
        {
            for (int i = 1; i <= 20; i++)
            {
                Agent agent = new Agent(i, Elevator);
                var thread = new Thread(() =>
                {
                    agent.Work();
                });
                thread.Start();
                AgentsThread.Add(thread);
            }
        }

    }
}
