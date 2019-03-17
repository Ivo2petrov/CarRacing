using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;


namespace Racers
{
    public class Program
    {
        private static readonly Random _random = new Random();
        public static void Main()
        {
            var car1 = new RaceCar(topSpeedMph: _random.Next(1, 200));
            car1.Driver = new Driver()
            {
                Name = "Jabir",
                SkillLevel = 50,
                WeightPounds = 150
            }

            ;
            car1.Year = 2015;
            car1.Make = "Mercedes";
            car1.Model = "AMG GT3";
            car1.HorsePower = 8300;
            car1.WeightPounds = 2400;
            
            var car2 = new RaceCar(topSpeedMph: _random.Next(1, 200));
            car2.Driver = new Driver()
            {
                Name = "Dany",
                SkillLevel = 75,
                WeightPounds = 178
            }

            ;
            car2.Year = 2018;
            car2.Make = "BMW";
            car2.Model = "M8 GTE";
            car2.HorsePower = 2600;
            car2.WeightPounds = 1220;

            var car3 = new RaceCar(topSpeedMph: _random.Next(1, 200));
            car3.Driver = new Driver()
            {
                Name = "Dominic",
                SkillLevel = 75,
                WeightPounds = 178
            }

            ;
            car3.Year = 2018;
            car3.Make = "SUBARU";
            car3.Model = "impreza STI";
            car3.HorsePower = 380;
            car3.WeightPounds = 1235;
            var raceTrack = new RaceTrack();
            var raceClass = GetRaceClasses()[_random.Next(0, 3)];
            Console.WriteLine("Welcome to the race!");
            Console.WriteLine();
            Console.WriteLine(String.Format("The 'Race Class' for this race will be '{0}'", raceClass.Name));
            Console.WriteLine();

            Console.WriteLine("Competing this day are thre cars.");
            Console.WriteLine(String.Format("{0} in a {1} {2} {3} with a top speed of {4} MPH", car1.Driver.Name, car1.Year, car1.Make, car1.Model, car1.TopSpeedMph));
            Console.WriteLine(String.Format("{0} in a {1} {2} {3} with a top speed of {4} MPH", car2.Driver.Name, car2.Year, car2.Make, car2.Model, car2.TopSpeedMph));
            Console.WriteLine(String.Format("{0} in a {1} {2} {3} with a top speed of {4} MPH", car3.Driver.Name, car3.Year, car3.Make, car3.Model, car3.TopSpeedMph));
            Console.WriteLine();

            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var raceResults = raceTrack.RaceTheCars(raceCar1: car1, raceCar2: car2, raceCar3: car3, raceClass: raceClass);
            if (raceResults.IsSuccess)
            {
                if (raceResults.IsTie)
                {
                    Console.Write(String.Format("There was a tie between the Following drivers. "));
                    raceResults.TiedCars.ForEach(c => Console.Write(String.Format(" {0} ", c.Driver.Name)));
                    Console.WriteLine();
                }
                else
                {
                    Console.Write(String.Format("Congratulations to {0}, the winner of this race.", raceResults.WinningRaceCar.Driver.Name));
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("The race was not successful.");
                Console.WriteLine(raceResults.UnsuccessfulRaceInformation);
                Console.WriteLine();
            }

            Thread.Sleep(1000); //to annoy the user.
            stopWatch.Stop();
            Console.WriteLine();
            Console.WriteLine(String.Format("This race took {0} milliseconds.", stopWatch.ElapsedMilliseconds));
            Console.WriteLine();
        }

        public static List<RaceClass> GetRaceClasses()
        {
            var result = new List<RaceClass>();
            var topFuel = new RaceClass(name: "Top Fuel", weightPoundsMin: 2300, weightPoundsMax: 2600, horsePowerMin: 8500, horsePowerMax: 10000, raceTrackLengthFeet: 1000);
            result.Add(topFuel);
            var funnyCar = new RaceClass(name: "Funny Car", weightPoundsMin: 2100, weightPoundsMax: 2600, horsePowerMin: 7000, horsePowerMax: 9000, raceTrackLengthFeet: Convert.ToInt32(Math.Round(((decimal).25 * 5280), 0))); //we should parse and store track length as feet at the source of origin like user input.
            result.Add(funnyCar);
            var proStock = new RaceClass(name: "Pro Stock", weightPoundsMin: 2350, weightPoundsMax: 2700, horsePowerMin: 900, horsePowerMax: 1300, raceTrackLengthFeet: Convert.ToInt32(Math.Round(((decimal).25 * 5280), 0))); //we should parse and store track length as feet at the source of origin like user input.
            result.Add(proStock);
            return result;
        }
    }

    public class Driver
    {
        public string Name
        {
            get;
            set;
        }

        public decimal WeightPounds
        {
            get;
            set;
        }

        public int SkillLevel
        {
            get;
            set;
        } 
    }

    public class RaceCar
    {
        public string Make
        {
            get;
            set;
        }

        public string Model
        {
            get;
            set;
        }

        public int Year
        {
            get;
            set;
        }

        public int WeightPounds
        {
            get;
            set;
        }

        public int HorsePower
        {
            get;
            set;
        }

        public decimal TopSpeedMph
        {
            get;
            private set;
        } 

        public Driver Driver
        {
            get;
            set;
        }

        public RaceCar(decimal topSpeedMph)
        {
            this.TopSpeedMph = topSpeedMph;
        }
    }

    public class RaceResults
    {
        public bool IsSuccess
        {
            get;
            set;
        }

        public string UnsuccessfulRaceInformation
        {
            get;
            set;
        }

        public RaceCar WinningRaceCar
        {
            get;
            set;
        }

        public bool IsTie
        {
            get;
            set;
        }

        public List<RaceCar> TiedCars
        {
            get;
            set;
        }

        public RaceResults()
        {
            this.TiedCars = new List<RaceCar>();
        }
    }

    public class RaceClass
    {
        public string Name
        {
            get;
            private set;
        }

        public int WeightPoundsMin
        {
            get;
            private set;
        }

        public int WeightPoundsMax
        {
            get;
            private set;
        }

        public int HorsePowerMin
        {
            get;
            private set;
        }

        public int HorsePowerMax
        {
            get;
            private set;
        }

        public int RaceTrackLengthFeet
        {
            get;
            private set;
        }

        public RaceClass(string name, int weightPoundsMin, int weightPoundsMax, int horsePowerMin, int horsePowerMax, int raceTrackLengthFeet)
        {
            this.Name = name;
            this.WeightPoundsMin = weightPoundsMin;
            this.WeightPoundsMax = weightPoundsMax;
            this.RaceTrackLengthFeet = raceTrackLengthFeet;
            this.HorsePowerMin = horsePowerMin;
            this.HorsePowerMax = horsePowerMax;
        }
    }

    public class RaceTrack
    {
        private object raceCar3;

        public RaceResults RaceTheCars(RaceCar raceCar1, RaceCar raceCar2, RaceCar raceCar3, RaceClass raceClass)
        {
            var result = new RaceResults();
            if (this.ValidateCarsForRaceClass(raceClass: raceClass, raceCars: new List<RaceCar>
        {
        raceCar1, raceCar2, raceCar3,
        }

            ))
            {
                result = this.DetermineWinner(raceCar1: raceCar1, raceCar2: raceCar2, raceCar3: raceCar3);
            }
            else
            {
                result.IsSuccess = false;
                result.UnsuccessfulRaceInformation = "Not all race cars met the criteria for the chosen race class for this race. No race occurred.";
            }

            return result;
        }

        private RaceResults DetermineWinner(RaceCar raceCar1, RaceCar raceCar2, RaceCar raceCar3)
        {
            var result = new RaceResults();
            if (raceCar1 != null && raceCar2 != null && raceCar3 != null)
            {
                var racingCars = new List<RaceCar>
            {
            raceCar1, raceCar2, raceCar3
            }

                ;
               
                var winningCar = racingCars.OrderByDescending(c => c.TopSpeedMph).ToList()[0]; 
                                                                                               
                var sameSpeedCars = racingCars.Where(c => c.TopSpeedMph == winningCar.TopSpeedMph && c.Driver.Name != winningCar.Driver.Name).ToList();
                if (sameSpeedCars.Any())
                {
                    result.IsSuccess = true;
                    result.IsTie = true;
                    var tiedCars = new List<RaceCar>();
                    tiedCars.Add(winningCar);
                    tiedCars.AddRange(sameSpeedCars);
                    result.TiedCars = tiedCars;
                }
                else
                {
                    result.IsSuccess = true;
                    result.IsTie = false;
                    result.WinningRaceCar = winningCar;
                }
            }
            else
            {
                result.IsSuccess = false;
                result.UnsuccessfulRaceInformation = "There needs to be 3 cars to race. No race occurred.";
            }

            return result;
        }

        private bool ValidateCarsForRaceClass(RaceClass raceClass, List<RaceCar> raceCars)
        {
            var result = true;
            var inValidCars = raceCars.Where(c => c.WeightPounds < raceClass.WeightPoundsMin || c.WeightPounds > raceClass.WeightPoundsMax || c.HorsePower < raceClass.HorsePowerMin || c.HorsePower > raceClass.HorsePowerMax).ToList();
            result = inValidCars.Any() == false;
            return result;
        }
    }
}
