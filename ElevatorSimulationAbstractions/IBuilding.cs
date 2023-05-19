using System;
using System.Collections.Generic;
using System.Text;

namespace ElevatorSimulationAbstractions
{
    public interface IBuilding
    {
        int Floors { get; set; }
        List<IElevator> Elevators { get; set; }

        void CallElevator(int floor, int destination, int passengers);

        IElevator GetNearestElevator(int floor);
    }
}
