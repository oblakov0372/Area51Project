using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace LastProjectVersion2.Models
{
    /*
     walk on the floor(30%)
     ride the elevator(30%)
     leave from area(20%)
     */
    public enum SecurityLevel
    {
        Confidential,
        Secret,
        TopSecret
    }
    public class Agent
    {
        public int Id { get; set; }
        public SecurityLevel SecurityLevel { get; set; }
        public Floor CurrentFloor { get; set; }
        public bool InArea { get; set; }
        private Elevator Elevator { get; set; }

        static Random rnd = new Random();

        public Agent(int id, Elevator elevator)
        {
            Id = id;
            Elevator = elevator;
            SecurityLevel = (SecurityLevel)rnd.Next(0, 3);
            CurrentFloor = new Floor(EFloor.G);
            InArea = true;
        }
        public void CallElevator(Button button)
        {
            button.CallElevator(this, Elevator);
        }
        public void SelectFloor(Button button)
        {
            button.ElevatorGo(this, Elevator);
        }
        private bool Throw(int chance)
        {
            int dice = rnd.Next(100);
            return dice < chance;
        }

        public void Work()
        {
            Thread.Sleep(200);
            Console.WriteLine($"Agent with Id {Id} came to work");
            while (true)
            {
                if (Throw(30))
                {
                    CallElevator(CurrentFloor.ButtonToCallElevator);
                    Console.WriteLine($"Agent{Id} entered the elevator");
                    while (true)
                    {
                        Button clickedButton;
                        do
                        {
                            clickedButton = Elevator.Buttons[rnd.Next(0, 4)];
                        } while (clickedButton.Floor == CurrentFloor.Name);
                        Console.WriteLine($"Agent{Id} click button {clickedButton.Floor}");
                        SelectFloor(clickedButton);

                        if (Throw(90) && Elevator.ElevatorDoor.OpenDoor(this))
                        {
                            Console.WriteLine($"Agent {Id} leave from elevator");
                            Elevator.Leave();
                            break;
                        }
                    }
                }
                if (Throw(30))
                {
                    Console.WriteLine($"Agent {Id} walking in floor {CurrentFloor.Name}");
                    Thread.Sleep(2000);
                }
                if (Throw(20))
                {
                    if (CurrentFloor.Name != EFloor.G)
                    {
                        CallElevator(CurrentFloor.ButtonToCallElevator);
                        SelectFloor(Elevator.Buttons.Where(b => b.Floor == EFloor.G).FirstOrDefault());
                        Elevator.Leave();
                    }
                    Console.WriteLine($"Agent {Id} go home");
                    break;
                }
            }
        }
    }
}
