using ElevatorSimulationAbstractions;
using Microsoft.Extensions.Options;
using System;

namespace ElevatorSimulation
{
    public class Elevator : IElevator
    {
        public int Floor { get; set; }
        public Direction Direction { get; set; }
        public int Capacity { get; set; }
        public int Passengers { get; set; }

        public Elevator(IOptions<BuildingElevatorsSettings> options)
        {
            Floor = 1;
            Direction = Direction.Up;
            Capacity = options.Value.ElevatorCapacity;
            Passengers = 0;
        }

        public void Move(int floor)
        {
            if (floor > Floor)
            {
                Direction = Direction.Up;
            }
            else if (floor < Floor)
            {
                Direction = Direction.Down;
            }
            else
            {
                Console.WriteLine($"Elevator is already at floor :{Floor} ...");
                return;
            }

            Console.WriteLine($"Elevator moving {Passengers} Passengers from floor :{Floor} to floor :{floor} Direction: {Direction.ToString()} ...");

            while (Floor != floor)
            {
                if (Direction == Direction.Up)
                {
                    Floor++;
                }
                else
                {
                    Floor--;
                }
            }
        }

        public bool PickUp(int floor, int passengers)
        {
            if (passengers <= Capacity && passengers > 0 && floor > 0)
            {
                Passengers = passengers;
                Floor = floor;
                Console.WriteLine($"Elevator Picking up {Passengers} Pasangers at floor :{Floor} ...");
                return true;
            }
            else
            {
                Passengers = 0;
                Console.WriteLine($"Elevator failed to pick up {passengers} Pasangers at floor :{floor} ...");
                return false;
            }
        }

        public void DropOff(int floor)
        {
            Console.WriteLine($"Elevator Dropping off {Passengers} Passengers at floor :{floor} ...");

            Passengers = 0;
            Floor = floor;
        }
    }
}
