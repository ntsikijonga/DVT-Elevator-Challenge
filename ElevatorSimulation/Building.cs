using ElevatorSimulationAbstractions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ElevatorSimulation
{
    public class Building : IBuilding
    {
        public int Floors { get; set; }
        public List<IElevator> Elevators { get; set; }

        public Building(IOptions<BuildingElevatorsSettings> options, IElevator elevator)
        {
            Floors = options.Value.NumberOfFloors;

            Elevators = new List<IElevator>();

            for (var i = 0; i < options.Value.NumberOfElevator; i++)
            {
                Elevators.Add(elevator);
            }
        }

        public void CallElevator(int floor, int destination, int passengers)
        {
            var elevator = GetNearestElevator(floor);

            if(elevator.PickUp(floor, passengers))
            {
                elevator.Move(destination);
                elevator.DropOff(destination);
            }
            
        }

        public IElevator GetNearestElevator(int floor)
        {
            return Elevators.OrderBy(e => Math.Abs(e.Floor - floor)).First();
        }
    }
}
