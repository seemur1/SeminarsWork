using System;

namespace Seminar2_1HW
{
    class Robot
    {
        public int x, y;

        public void Right() { x++; }
        public void Left() { x--; }
        public void Forward() { y++; }
        public void Backward() { y--; }

        public string Position()
        {
            return string.Format("The Robot position: x={0}, y={1}", x, y);
        }
    }

    delegate void Steps();

    class Program
    {
        static Steps antiStep(char stepType, Robot robot)
        {
            switch (stepType)
            {
                case 'R':
                    return new Steps(robot.Left);
                case 'L':
                    return new Steps(robot.Right);
                case 'F':
                    return new Steps(robot.Backward);
                case 'B':
                    return new Steps(robot.Forward);
                // Если ввели мусор - ругаемся.
                default:
                    throw new ArgumentException();
            }
        }

        static Steps chooseAStep(char stepType, Robot robot, ref (int, int) currpos)
        {
            switch(stepType)
            {
                case 'R':
                    currpos.Item1++;
                    return new Steps(robot.Right);
                case 'L':
                    currpos.Item1--;
                    return new Steps(robot.Left);
                case 'F':
                    currpos.Item2++;
                    return new Steps(robot.Forward);
                case 'B':
                    currpos.Item2--;
                    return new Steps(robot.Backward);
                // Если ввели мусор - ругаемся.
                default:
                    throw new ArgumentException();
            }
        }

        // Метод, отрисовывающий картинку.
        static void WriteMap((int, int) currpos, bool[,] coords)
        {
            Console.Clear();

            for (int i = 0; i < coords.GetLength(0); ++i)
            {
                for (int j = 0; j < coords.GetLength(1); ++j)
                {
                    if ((i == currpos.Item1) && (j == currpos.Item2)) { Console.Write("*"); }
                    else if (coords[i, j]) { Console.Write("+"); }
                    else { Console.Write("0"); }
                }
                Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {

            //-------------------------------------------Первая часть ДЗ------------------------------------------------
            Robot rob = new Robot();
            (int, int) useless = (0, 0);
            Console.WriteLine("Введите комманду! (Вводите только \"корректные\" комманды!)");
            string command = Console.ReadLine();
            Console.WriteLine($"Начальные координаты: {rob.Position()}");
            if (command.Length == 0)
            {
                Console.WriteLine($"Конечные координаты: {rob.Position()}");
            }
            // Если путь ненулевой - то выполлняем всю последовательность шагов.
            else
            {
                Steps pathOfSteps = null;
                foreach (char i in command)
                {
                    pathOfSteps += chooseAStep(i, rob, ref useless);
                }
                pathOfSteps();
                Console.WriteLine($"Конечные координаты: {rob.Position()}");
            }
            //-------------------------------------------Вторая часть ДЗ------------------------------------------------

            // Инициализируем все необходимые значения.
            int lengthOfPath = 0;
            bool checker = false, commandIsCorrect;
            rob = new Robot();
            (int, int) currpos = (0, 0), bufferpos;
            Steps step, antistep;

            // Узнаём размеры окна.
            Console.WriteLine("Введите ширину окна (в клетках)! (На вход принимаются только целые числа!)");
            int x = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите высоту окна (в клетках)! (На вход принимаются только целые числа!)");
            int y = int.Parse(Console.ReadLine());
            // Инициализируем карту.
            bool[,] coords = new bool[x, y], buffercoords;

            // Работаем, пока не попросят прекратить работу.
            while (!checker)
            {
                // Обнулям список действий, и буфферную инфу.
                bufferpos = currpos;
                step = null;
                antistep = null;
                commandIsCorrect = true;
                buffercoords = new bool[x, y];

                // Читаем команду.
                Console.WriteLine("Введите комманду! (Вводите только \"корректные\" комманды!)");
                command = Console.ReadLine();
                // Последовательно обрабатываем инфу по прочитанному шагу.
                for (int i = 0; i < (command.Length) && commandIsCorrect; ++i)
                {
                    try
                    {
                        step += chooseAStep(command[i], rob, ref bufferpos);
                        antistep += antiStep(command[i], rob);
                        buffercoords[bufferpos.Item1, bufferpos.Item2] = true;
                    }
                    catch
                    {
                        commandIsCorrect = false;
                    }
                }
                // Шагаем.
                step();
                // Если шагнули "плохо" (вышли за границу) - шагаем обратно.
                if ((rob.x < 0) || (rob.x >= x) || (rob.y < 0) || (rob.y >= y))
                {
                    antistep();
                    commandIsCorrect = false;
                }
                // Если комманда корректна - идём, куда надо. (достаём значения из всех "буферов", и сохраняем их)
                if (commandIsCorrect)
                {
                    currpos = bufferpos;
                    for (int i = 0; i < x; ++i)
                    {
                        for (int j = 0; j < y; ++j) { coords[i, j] = buffercoords[i, j] | coords[i, j]; }
                    }
                }
                // Отрисовываем картинку.
                WriteMap(currpos, coords);
            }
        }
    }
}
