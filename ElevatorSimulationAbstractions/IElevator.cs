using System;

namespace ElevatorSimulationAbstractions
{
    public interface IElevator
    {
        int Floor { get; set; }

        Direction Direction { get; set; }

        int Capacity { get; set; }

        int Passengers { get; set; }

        void Move(int floor);

        bool PickUp(int floor, int passengers);

        void DropOff(int floor);
    }
}
