using System;

namespace CarManage
{
    class Car : Menu.ICar
    {
        private readonly string _model;
        private double _tank;
        private readonly int _seats;
        private readonly double _engine;
        private readonly double _consumption;
        private DateTime _engineStarted;
        private readonly double _fullTank;

        public EngineStatus engineStatus { get; private set; }

        public delegate void notify(string message);
        notify _notify;

        public enum EngineStatus
        {
            Stop,
            Start
        };

        public Car(double tank, int seats, double engine, double consumption,string model, notify not)
        {
            _model = model;
            _tank = tank;
            _seats = seats;
            _engine = engine;
            _consumption = consumption;
            _fullTank = tank;
            _notify = not;
            _notify("Car added");
        }

        public void Refill()
        {
            _tank = _fullTank;
            _notify("Tank refilled");
        }

        public void Stop()
        {
            engineStatus = EngineStatus.Stop;
            _notify("engine stopped");
        }

        public void Start()
        {
            if (engineStatus != EngineStatus.Start && _tank > 0)
            {
                engineStatus = EngineStatus.Start;
                _engineStarted = DateTime.Now;
                _notify("Engine started");
            }
        }

            public override string ToString()
        {
            if (engineStatus == EngineStatus.Start)
            {
                _tank -= (DateTime.Now - _engineStarted).TotalSeconds / 100 * _consumption;
                _engineStarted = DateTime.Now;

                if (_tank > 0)
                {
                    return
                        $"Car: {_model}; Engine: {_engine};Seats: {_seats}; TankFuel: {_tank}; Consumption: {_consumption} l/100; Engine status: Started";
                }
                else
                {
                    engineStatus = EngineStatus.Stop;
                    _notify("Low fuel, engine stopped");
                    return $"Car: {_model}; Engine: {_engine};Seats: {_seats}; TankFuel: {_tank}; Consumption: {_consumption} l/100; Engine Status: Stop";
                }
            }
            else
                return $"Car: {_model}; Engine: {_engine};Seats: {_seats}; TankFuel: {_tank}; Consumption: {_consumption} l/100; Engine Status: Stop";
        }

    }
}
