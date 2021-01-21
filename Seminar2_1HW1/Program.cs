using System;

namespace Seminar2_1HW1
{
    public delegate double delegateConvertTemperature(double a);

    class TemperatureConverterImp
    {
        private static delegateConvertTemperature[] listOfMethods;

        public TemperatureConverterImp()
        {
            listOfMethods = new delegateConvertTemperature[5];
            listOfMethods[0] = (value) => value * 9 / 5 + 32;
            listOfMethods[1] = (value) => (value + 459.67) * 5 / 9;
            listOfMethods[2] = (value) => value * 9 / 5;
            listOfMethods[3] = (value) => (value - 491.67) * 4 / 9;
            listOfMethods[4] = (value) => value * 5 / 4;
        }

        static double ChangeTemp(double value, int from, int to)
        {
            for (int i = from; i < to; ++i)
            {
                value = listOfMethods[i % listOfMethods.Length](value);
            }
            return value;
        }

        // СToF - от 0 до 1
        public double CToF(double value) { return ChangeTemp(value, 0, 1); }
        // FToC - от 1 до 5
        public double FToC(double value) { return ChangeTemp(value, 1, 5); }
        // CToK - от 0 до 2
        public static double CToK(double value) { return ChangeTemp(value, 0, 2); }
        // KToC - от 2 до 5
        public static double KToC(double value) { return ChangeTemp(value, 2, 5); }
        // СToRan - от 0 до 3
        public static double CToRan(double value) { return ChangeTemp(value, 0, 3); }
        // RanToC - от 3 до 5
        public static double RanToC(double value) { return ChangeTemp(value, 3, 5); }
        // CToReo - от 0 до 4
        public static double CToReo(double value) { return ChangeTemp(value, 0, 4); }
        // ReoToC - от 4 до 5
        public static double ReoToC(double value) { return ChangeTemp(value, 4, 5); }
    }

    class Program
    {
        static void Main(string[] args)
        {

            // ------------------------------------------------------------------------------------------------------------------------
            // Инициализируем необходимые делегаты.
            TemperatureConverterImp temperatureConverter = new TemperatureConverterImp();
            delegateConvertTemperature first = new delegateConvertTemperature(temperatureConverter.CToF);
            delegateConvertTemperature second = new delegateConvertTemperature(temperatureConverter.FToC);

            // Узнаём необходимую температуру (Цельсий).
            Console.WriteLine("Введите вашу температуру в шкале Цельсия! (обязательно вещественную!!!)");
            double tempCel = double.Parse(Console.ReadLine());
            // Узнаём необходимую температуру (Фаренгейт).
            Console.WriteLine("Введите вашу температуру в шкале Фаренгейта! (обязательно вещественную!!!)");
            double tempFar = double.Parse(Console.ReadLine());
            // Выводим обе конвертированные температуры:
            Console.WriteLine($"Первая температура по Фарегейту == {first(tempCel)}");
            Console.WriteLine($"Вторая температура по Цельсию == {second(tempFar)}");

            //------------------------------------------------Конец первой части--------------------------------------------------------

            // Пояснение - Всё было решено запихнуть не в отдельный класс StaticTempConverters, а в тот же класс добавилось несколько
            // методов. Логика выполнения та же, новые методы - статические, так, просто, показалось красивее.

            // Считываем данные о новой температуре на обработку.
            Console.WriteLine("Начало Выполнения Второй части задания!!!");
            Console.WriteLine("Введите вашу температуру в шкале Цельсия! (обязательно вещественную!!!)");

            tempCel = double.Parse(Console.ReadLine());

            // Создаём массив делегатов, содержащих требуемые методы.
            delegateConvertTemperature[] listOfMethods = new delegateConvertTemperature[4];
            listOfMethods[0] = temperatureConverter.CToF;
            listOfMethods[1] = TemperatureConverterImp.CToK;
            listOfMethods[2] = TemperatureConverterImp.CToRan;
            listOfMethods[3] = TemperatureConverterImp.CToReo;
            // Для удобного вызова всех методов создаём список описаний действий:
            string[] listOfDescriptions = { "Фаренгейта", "Кельвина", "Ранкина", "Реомюра" };

            // Выводим все ответы.
            for (int i = 0; i < listOfMethods.Length; ++i)
            {
                Console.WriteLine($"Ваша температура, переведённая в шкалу {listOfDescriptions[i]} == {listOfMethods[i](tempCel)}");
            }
        }
    }
}
