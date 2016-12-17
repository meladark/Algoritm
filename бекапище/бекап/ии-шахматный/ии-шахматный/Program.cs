using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace ии_шахматный
{
 public  struct pole
    { 
        public int color;//0- нету фигуры 1 - белые -1 - черные
        public float figure;
    }
 public struct SaveValue
    {
        public hod Hod;
        public int depth;
        public SaveValue(hod Hod,int depth)
        {
           
            this.Hod = Hod;
            this.depth = depth;
        }
    }
 public struct hod
    {
    public int f_x;
    public int f_y;
    public int s_x;
    public int s_y;
    public int Fig;
    public float MVVK;
        public hod(int f_x,int f_y,int s_x,int s_y,int Fig, float MVVK)
        {
            this.MVVK = MVVK;
            this.f_x = f_x;
            this.f_y = f_y;
            this.s_x = s_x;
            this.s_y = s_y;
            this.Fig = Fig;           
        }
    }
    public class Program
    {
        static void Main(string[] args)
        {
            pole[,] shah = new pole[8, 8];
            Console.WriteLine();
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            StreamReader Sr = new StreamReader(args[0]);
            int depth = Enter_Exit.Enter(shah, Sr);
            AlpBet.Pic(shah);//рисовка
            List<SaveValue> Posible_Moves = new List<SaveValue>();
            bool score = AlpBet.AlphaBetaAmHod(1, depth * 2 - 1, shah, Posible_Moves);
           stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
            Console.WriteLine();
            Console.WriteLine("RunTime " + elapsedTime);
            Console.WriteLine();
            Console.WriteLine("Конец перебора");
            StreamWriter fd = new StreamWriter(args[1]);
            Enter_Exit.Exit(Posible_Moves, fd);
            Console.WriteLine();
            string outpu;
            if (score)
            {
                outpu = "Удачно";
            }
            else { outpu = "Неудачно"; }
            Console.WriteLine("Завершение расчета {0}", outpu);
            Console.ReadKey();
        }
    }
}
