using System;
using System.Collections.Generic;
using System.Text;

namespace ElevatorSimulation
{
    public class BuildingElevatorsSettings
    {
        public int NumberOfFloors { get; set; } = 10;

        public int NumberOfElevator { get; set; } = 4;

        public int ElevatorCapacity { get; set; } = 10;
    }
}
