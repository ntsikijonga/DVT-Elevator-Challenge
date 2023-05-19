using Microsoft.Extensions.DependencyInjection;
using ElevatorSimulationAbstractions;
using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace ElevatorSimulation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=============Elevator Simulation===================");

            //Seting up Dependency Injection

            var configBuilder = new ConfigurationBuilder();

            configBuilder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, false);

            var configuration = configBuilder.Build();

            var services = new ServiceCollection()
            .AddSingleton<IBuilding, Building>()
            .AddSingleton<IElevator, Elevator>();
            services.Configure<BuildingElevatorsSettings>(configuration.GetSection(nameof(BuildingElevatorsSettings)));

            var serviceProvider = services.BuildServiceProvider();

            var building = serviceProvider.GetRequiredService<IBuilding>();
            var settings = serviceProvider.GetRequiredService<IOptions<BuildingElevatorsSettings>>();

            while(true)
            {
                Console.WriteLine($"Would you to call an Elevator? Y/N");
                var response = Console.ReadLine().ToLower();

                try
                {
                    switch (response)
                    {
                        case "y":
                            {
                                Console.WriteLine($"On which floor are you in the building? {1}-{settings.Value.NumberOfFloors}");
                                Int32.TryParse(Console.ReadLine(), out var floor);
                                if( !(floor > 0) || floor > settings.Value.NumberOfFloors)
                                {
                                    Console.WriteLine($"Floor should be between {1} and {settings.Value.NumberOfFloors}");
                                    continue;
                                }

                                Console.WriteLine($"What is you destination floor ? {1}-{settings.Value.NumberOfFloors}");
                                Int32.TryParse(Console.ReadLine(), out var destination);
                                if ( !(destination > 0) || destination > settings.Value.NumberOfFloors)
                                {
                                    Console.WriteLine($"Destination should be between {1} and {settings.Value.NumberOfFloors}");
                                    continue;
                                }

                                Console.WriteLine($"Number of passengers ? {1}-{settings.Value.ElevatorCapacity}");
                                Int32.TryParse(Console.ReadLine(), out var passengers);
                                if ( !(passengers > 0) || passengers > settings.Value.ElevatorCapacity)
                                {
                                    Console.WriteLine($"Number of passengers should be between {1} and {settings.Value.ElevatorCapacity}");
                                    continue;
                                }

                                building.CallElevator(floor, destination, passengers);
                                break;
                            }
                        default: continue;
                    }
                }
                catch( Exception ex )
                {
                    Console.WriteLine(ex.Message);
                    continue;
                }
                
            }
        }
    }
}
