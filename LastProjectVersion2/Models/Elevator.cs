using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastProjectVersion2.Models
{
    public class Elevator
    {
        public EFloor CurrentFloor { get; set; }
        public List<Button> Buttons { get; set; }
        public ElevatorDoor ElevatorDoor { get; set; }
        Semaphore semaphore;
        public Elevator()
        {
            semaphore = new Semaphore(1, 1);
            ElevatorDoor = new ElevatorDoor(this);
            Buttons = new List<Button>();
            foreach (var i in Enum.GetValues(typeof(EFloor)))
            {
                Buttons.Add(new Button((EFloor)i));
            }
            CurrentFloor = EFloor.G;
        }
        public bool TryCall()
        {
            if (semaphore.WaitOne())
                return true;
            
            return false;
        }
        public void Leave()
        {
            semaphore.Release();
        }
        public void Go(Button button, Agent agent)
        {
            Console.WriteLine();
            while (CurrentFloor != button.Floor)
            {
                Console.WriteLine($"Elevator current floor is {CurrentFloor}");
                if (CurrentFloor < button.Floor)
                    CurrentFloor++;
                else
                    CurrentFloor--;
                Thread.Sleep(1000);
            }
            agent.CurrentFloor.Name = CurrentFloor;
            Console.WriteLine($"Elevator in your floor {CurrentFloor}");
            if (ElevatorDoor.isOpened(agent))
            {
                Console.WriteLine("Door is opening");
                return;
            }
            Console.WriteLine("You have not permissions\n");

        }
    }
}
