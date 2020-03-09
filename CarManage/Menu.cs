using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text;

namespace CarManage
{

    class Menu
    {
        private int _chooseItem = 0;

        readonly List<Car> carsList = new List<Car>();
        private string _log;

        private enum Cars
        {
            Peugeot,
            Citroen,
            BMW,
            Hyundai,
            Honda
        }

        public interface ICar
        {
            void Start();

            void Stop();
        }

        public void ShowNotify(string text)
        {
            _log += text + "\n";
        }

        public void MakeSomeCar(int count)
        {
            Random random = new Random();

            for(int i = 0; i<count; i++)
                AddCar((Cars)random.Next(0,4));

            ShowMenu();
        }

       /* private readonly Dictionary<Cars, string> _carsDictionary = new Dictionary<Cars, string>()
        {
            {Cars.Peugeot, "Peugeot 3008, 1,6 Engine (56 liters tank)"},
            {Cars.Citroen, "Citroen C5, EP6 2.2 (60 liters tank)"},
            {Cars.BMW, "BMW E46, 3.0 Diesel, (60 liters tank)" },
            {Cars.Hyundai, "Hyundai Accent, 1,6, (55 liters tank)" },
            {Cars.Honda, "Honda Civic, 1.3, (45 liters tank)" }
        };*/

        private void AddCar(Cars car)
        {
            switch (car)
            {
                case Cars.Peugeot:
                    carsList.Add(new Car(56,7,1.6,6.7,"Peugeot",ShowNotify));
                    break;
                case Cars.Citroen:
                    carsList.Add(new Car(60, 5, 2.2, 9,"Citroen",ShowNotify));
                    break;
                case Cars.BMW:
                    carsList.Add(new Car(60, 4, 3.0, 13,"BMW",ShowNotify));
                    break;
                case Cars.Hyundai:
                    carsList.Add(new Car(55, 5, 1.6, 7,"Hyundai",ShowNotify));
                    break;
                case Cars.Honda:
                    carsList.Add(new Car(45, 4, 1.3, 4,"Honda",ShowNotify));
                    break;
            }
        }

        public void ShowMenu()
        {

            Console.Clear();

            for (var carIndex = 0; carIndex < carsList.Count; carIndex++)
            {
                var car = carsList[carIndex];

                if (car.engineStatus == Car.EngineStatus.Start && _chooseItem != carIndex)
                    ConsoleColorWrite(car.ToString(),ConsoleColor.Green);
                else if (car.engineStatus == Car.EngineStatus.Stop && _chooseItem != carIndex)
                    ConsoleColorWrite(car.ToString(),ConsoleColor.Red);
                else
                    ConsoleColorWrite($"----->{car}",ConsoleColor.Cyan);
            }

            ConsoleColorWrite(
                "Press:\n" +
                "1 - to start Engine\n" +
                "2-to Stop engine\n" +
                "3-to add Peugeot\n" +
                "4-to add BMW\n" +
                "5-to add Honda\n" +
                "6-to add Hyundai\n" +
                "7-to add Citroen\n" +
                "8-to refill FuelTank",
                ConsoleColor.Magenta);
            CheckKey();
        }

        private void ConsoleColorWrite(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = default;
        }

        private void CheckKey()
        {
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.DownArrow:
                    MakeChoise(ConsoleKey.DownArrow);
                    break;

                case ConsoleKey.UpArrow:
                    MakeChoise(ConsoleKey.UpArrow);
                    break;
                case ConsoleKey.D1:
                    carsList[_chooseItem].Start();
                    break;
                case ConsoleKey.D2:
                    carsList[_chooseItem].Stop();
                    break;
                case ConsoleKey.D3:
                    AddCar(Cars.Peugeot);
                    break;
                case ConsoleKey.D4:
                    AddCar(Cars.BMW);
                    break;
                case ConsoleKey.D5:
                    AddCar(Cars.Honda);
                    break;
                case ConsoleKey.D6:
                    AddCar(Cars.Hyundai);
                    break;
                case ConsoleKey.D7:
                    AddCar(Cars.Citroen);
                    break;
                case ConsoleKey.D8:
                    carsList[_chooseItem].Refill();
                    break;
                case ConsoleKey.D9:
                    Console.Clear();
                    ConsoleColorWrite(_log,ConsoleColor.Cyan);
                    Console.ReadKey();
                    break;
            }
            ShowMenu();
        }

        private void MakeChoise(ConsoleKey key)
        {
            if (_chooseItem < carsList.Count - 1 && key == ConsoleKey.DownArrow)
                _chooseItem++;
            else if (_chooseItem > 0 && key == ConsoleKey.UpArrow)
                _chooseItem--;
        }
    }
}
