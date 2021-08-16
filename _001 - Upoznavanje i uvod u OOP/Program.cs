using System;
using System.Collections.Generic;

namespace _001
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Primjer uporabe:\n" +
                              "* inheritance\n" +
                              "* polymorphism\n" +
                              "* encapsulation\n" +
                              "* abstract class\n" +
                              "* interface\n" +
                              "* data types(value and reference types)\n" +
                              "* access modifiers\n" +
                              "* naming conventions(PascalCase i camelCase)\n" +
                              "* generics\n" +
                              "* SRP and DRY principle\n" +
                              "* Composition over inheritance\n");
        }
    }

    /*
     * Use camel casing ("camelCasing") when naming private or internal fields, and prefix them with _.
     * Source: https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions
     */

    #region Abstraction - Polymorphism
    class AbstractionAndPolymorphism
    {
        #nullable enable
        abstract class Vehicle
        {
            protected Dictionary<string, string> CarDetails = new Dictionary<string, string>();
            public string? GetDetail(string key) => (CarDetails.ContainsKey(key)) ? CarDetails[key] : null;
            public abstract void AddDetails(string key, string value);

            public Vehicle(string make, string model, string year)
            {
                CarDetails.Add("Make", make);
                CarDetails.Add("Model", model);
                CarDetails.Add("Year", year);
            }
        }
        #nullable disable

        class Car : Vehicle
        {

            Car(string make, string model, string year, string numberOfDoors) : base(make, model, year)
            {
                CarDetails.Add("Number of doors", numberOfDoors);
            }

            public override void AddDetails(string key, string value)
            {
                if (CarDetails.ContainsKey(key))
                    CarDetails[key] = value;
                else
                    CarDetails.Add(key, value);
            }
        }
    }
    #endregion

    #region Inheritance - Composition - Encapsulation - Data Types - Access Modifiers - SRP
    public class Car : ICar
    {
        private Engine _engine;
        private FuelTank _fuelTank;
        private const double _idleFuel = 0.0003;

        public FuelTankDisplay FuelTankDisplay;

        public Car(double fuelLevel = 20)
        {
            _fuelTank = new FuelTank();
            _fuelTank.Refuel(fuelLevel);
            _engine = new Engine(_fuelTank);

            FuelTankDisplay = new FuelTankDisplay(_fuelTank);
        }

        public bool EngineIsRunning => _engine.IsRunning;

        public void EngineStart()
        {
            if (_fuelTank.FillLevel > 0.0)
                _engine.Start();
        }

        public void EngineStop() => _engine.Stop();

        public void Refuel(double liters) => _fuelTank.Refuel(liters);

        public void RunningIdle()
        {
            if (_fuelTank.FillLevel < _idleFuel || (_fuelTank.FillLevel - _idleFuel) < _idleFuel)
            {
                _engine.Stop();
                return;
            }

            if (_engine.IsRunning)
                _fuelTank.Consume(_idleFuel);
        }
    }

    public class Engine : IEngine
    {
        private FuelTank _fuelTank;
        private bool _isRunning = false;

        public Engine(FuelTank fuelTank) => _fuelTank = fuelTank;

        public bool IsRunning => _isRunning;

        public void Consume(double liters) => _fuelTank.Consume(liters);

        public void Start() => _isRunning = true;

        public void Stop() => _isRunning = false;
    }

    public class FuelTank : IFuelTank
    {
        private const float MaximumLevel = 60.0f;
        private const float MinimumLevel = 5.0f;
        private double _fuelLevel = 0.0f;
        private bool _isOnReserve = false;
        private bool _isComplete = false;

        public double FillLevel => _fuelLevel;

        public bool IsOnReserve => _isOnReserve;

        public bool IsComplete => _isComplete;

        public void Consume(double liters)
        {
            if (liters > 0)
                _fuelLevel -= liters;

            _isComplete = _fuelLevel < MinimumLevel ? true : false;

            if (_fuelLevel < 0.0)
                _fuelLevel = 0.0;
        }

        public void Refuel(double liters)
        {
            if (liters > 0)
                _fuelLevel += liters;

            if (_isComplete = _fuelLevel >= MaximumLevel)
                _fuelLevel = MaximumLevel;

            _isOnReserve = (_fuelLevel > MinimumLevel) ? false : true;
        }
    }

    public class FuelTankDisplay : IFuelTankDisplay
    {
        private FuelTank _fuelTank;

        public FuelTankDisplay(FuelTank fuelTank) => _fuelTank = fuelTank;

        public double FillLevel => ((int)Math.Round(_fuelTank.FillLevel * 100.0)) / 100.0;

        public bool IsOnReserve => _fuelTank.IsOnReserve;

        public bool IsComplete => _fuelTank.IsComplete;
    }
    #endregion

    #region Generics - DRY
    public interface IDieselCar : ICar { }

    public interface IGasolineCar : ICar { }

    public class CarCollection<CarType> where CarType : ICar
    {
        private List<CarType> _carList = new();
        public void Add(CarType car)
        {
            _carList.Add(car);
        }
    }
    #endregion

    #region Interfaces
    public interface ICar
    {
        bool EngineIsRunning { get; }

        void EngineStart();

        void EngineStop();

        void Refuel(double liters);

        void RunningIdle();
    }

    public interface IEngine
    {
        bool IsRunning { get; }

        void Consume(double liters);

        void Start();

        void Stop();
    }

    public interface IFuelTank
    {
        double FillLevel { get; }

        bool IsOnReserve { get; }

        bool IsComplete { get; }

        void Consume(double liters);

        void Refuel(double liters);
    }

    public interface IFuelTankDisplay
    {
        double FillLevel { get; }

        bool IsOnReserve { get; }

        bool IsComplete { get; }
    }
    #endregion
}