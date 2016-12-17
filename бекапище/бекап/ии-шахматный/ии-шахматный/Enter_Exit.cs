using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;  

namespace ии_шахматный
{
    class Enter_Exit
    {
        const float Pesh = 1000;//ценности фигур, чтобы не забыть
        const float Hor = 3500;
        const float Elp = 3000;
        const float Fers = 9000;
        const float lad = 5000;
        const float king = 90000;
        public static int Enter(pole[,] StartPole, StreamReader Sr)
        {
            char Univers = ' ';
            float Figure_on_field = 0;
            int x = 0;
            int y = 0;
            int stroke = 0;
            int color = 0;
            int depth = 0;
            string readStr = Sr.ReadLine();
            try
            {
                depth = Convert.ToInt32(readStr);
            }
            catch
            {
                Console.WriteLine("Ошибка формата ввода глубины");
                Console.ReadKey();
                Environment.Exit(0);
            }
            if (depth == 0)
            {
                Console.WriteLine("Слишком маленькая глубина");
                Console.ReadKey();
                Environment.Exit(0);
            }
            if (depth > 6)
            {
                Console.WriteLine("Возможно это слишком большая глубина.");
            }
            readStr = Sr.ReadLine();
            while (readStr != null)
            {
                stroke++;
                if (readStr.Length > 4)
                {
                    Console.WriteLine("Ошибка Ввода");
                }
                try
                {
                    Univers = Convert.ToChar(readStr.Remove(1));
                    readStr = readStr.Remove(0,1);
                }
                catch
                {
                    Console.WriteLine("Строка {0} Ошибка ввода формата фигуры",stroke);
                    Console.ReadKey();
                    Environment.Exit(0);
                }
                bool kol = false;
                if (Univers == 'k')
                {
                    Figure_on_field = king;
                    kol = true;
                }
                if (Univers == 'q')
                {
                    Figure_on_field = Fers; kol = true;
                }
                if (Univers == 'r')
                {
                    Figure_on_field = lad; kol = true;
                }
                if (Univers == 'n')
                {
                    Figure_on_field = Hor; kol = true;
                }
                if (Univers == 'b')
                {
                    Figure_on_field = Elp; kol = true;
                }
                if (Univers == 'p')
                {
                    Figure_on_field = Pesh; kol = true;
                }
                if (kol == false)
                {
                    Console.WriteLine("Строка {0} Ошибка ввода фигуры", stroke);
                    Console.ReadKey();
                    Environment.Exit(0);
                }
                try
                {
                    color = Convert.ToInt32(readStr.Remove(1));
                    if (color == 0)
                    {
                        color = -1;
                    }
                    if (color > 1)
                    {
                        Console.WriteLine("Строка {0} Цвет ходящей фигуры 1, цвет оппонента 0",stroke);
                              Console.ReadKey();
                        Environment.Exit(0);
                    }
                    readStr = readStr.Remove(0,1);
                }
                catch
                {

                    Console.WriteLine("Строка {0} Ошибка ввода цвета", stroke);
                    Console.ReadKey();
                    Environment.Exit(0);
                }
                try
                {
                    Univers = Convert.ToChar(readStr.Remove(1));
                    readStr = readStr.Remove(0,1);
                }
                catch
                {
                    Console.WriteLine("Строка {0} Ошибка ввода X", stroke);
                    Console.ReadKey();
                    Environment.Exit(0);
                }
                x = Convert_ToInt(Univers);
                if (x == 9)
                {
                    Console.WriteLine("Строка {0}",stroke);
                    Console.ReadKey();
                    Environment.Exit(0);
                }
                try
                {
                    y = Convert.ToInt32(readStr);
                    readStr = readStr.Remove(0,1);
                    if (y < 1 || y > 8)
                    {
                        Console.WriteLine("Строка {0} На поле вертикаль от 1 до 8", stroke);
                        Console.ReadKey();
                        Environment.Exit(0);
                    }
                }
                catch
                {
                    Console.WriteLine("Строка {0} Ошибка ввода Y", stroke);
                    Console.ReadKey();
                    Environment.Exit(0);
                }
                if(Figure_on_field == king)
                    if (x == 4)
                    {
                        if (y - 1 == 0 && color == 1)
                        {
                            StartPole[x, y - 1].figure = 90005;
                            StartPole[x, y - 1].color = color;
                            readStr = Sr.ReadLine();
                            continue;
                        }
                        if (y - 1 ==  7 && color == -1)
                        {
                            StartPole[x, y - 1].figure = 90005;
                            StartPole[x, y - 1].color = color;
                            readStr = Sr.ReadLine();
                            continue;
                        }
                    }
                if (Figure_on_field==lad)
                {
                    if((y - 1==0&&color==1)|| y - 1 == 7 && color == -1)
                        if (x == 0 || x == 7)
                        {
                            StartPole[x, y - 1].figure = 5005;
                            StartPole[x, y - 1].color = color;
                            readStr = Sr.ReadLine();
                            continue;
                        }
                }

                StartPole[x, y-1].figure = Figure_on_field;
                StartPole[x, y-1].color = color;
                readStr = Sr.ReadLine();
            }
            Sr.Dispose();
            return depth;
        }
        public static void Exit(List<SaveValue> EndTurn, StreamWriter fd)
        {
            int depth = 0;
            for (int i = EndTurn.Count - 1; i > -1; i--)
            {
                if (depth == EndTurn[i].depth)
                {
                    fd.Write(" или {0} [{1}{2}]=>[{3}{4}]",ConvertFigure(EndTurn[i].Hod.Fig),Convert_toChar(EndTurn[i].Hod.f_x), EndTurn[i].Hod.f_y + 1,Convert_toChar(EndTurn[i].Hod.s_x), EndTurn[i].Hod.s_y + 1);
                }
                else
                {
                    if (i != EndTurn.Count - 1)
                        fd.WriteLine();
                    depth = EndTurn[i].depth;
                    fd.Write("{0}) {1} [{2}{3}]=>[{4}{5}]", EndTurn.Count - i,ConvertFigure(EndTurn[i].Hod.Fig),Convert_toChar(EndTurn[i].Hod.f_x), EndTurn[i].Hod.f_y + 1,Convert_toChar(EndTurn[i].Hod.s_x), EndTurn[i].Hod.s_y + 1);
                }
            }
            fd.Dispose();
        }
        public static int Convert_ToInt(char x)
        {
            if (x == 'a')
            {
                return 0;
            }
            if (x == 'b')
            {
                return 1;
            }
            if (x == 'c')
            {
                return 2;
            }
            if (x == 'd')
            {
                return 3;
            }
            if (x == 'e')
            {
                return 4;
            }
            if (x == 'f')
            {
                return 5;
            }
            if (x == 'g')
            {
                return 6;
            }
            if (x == 'h')
            {
                return 7;
            }
            Console.WriteLine("Ошибка конвертации");

            return 9;
        }
        public static char Convert_toChar(int x)
        {
            if (x == 0)
            {
                return 'a';
            }
            if (x == 1)
            {
                return 'b';
            }
            if (x == 2)
            {
                return 'c';
            }
            if (x == 3)
            {
                return 'd';
            }
            if (x == 4)
            {
                return 'e';
            }
            if (x == 5)
            {
                return 'f';
            }
            if (x == 6)
            {
                return 'g';
            }
            if (x == 7)
            {
                return 'h';
            }
            return 'S';
        }
        public static char ConvertFigure (int x)
        {
            if(x == 90000||x == 90005)
            {
                return 'K';
            }
            if (x == 5000 || x == 5005)
            {
                return 'R';
            }
            if (x == 3500)
            {
                return 'N';
            }
            if (x == 3000)
            {
                return 'B';
            }
            if (x == 9000)
            {
                return 'Q';
            }
            if (x == 1000)
            {
                return 'P';
            }
            Console.WriteLine("Ошибка конвертации фигуры");
            return 'Z';
        }
    }
}
