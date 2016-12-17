using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ии_шахматный
{
    class AlpBet
    {
        const int Pesh = 1000;
        const int Hor = 3500;
        const int Elp = 3000;
        const int Fers = 9000;
        const int lad = 5000;
        const int king = 90000;

        public static bool AlphaBetaAmHod(int color, int depth, pole[,] dosk, List<SaveValue> All_Moves)
        {
            if (depth == 0)//глубина
            {
                return SahAndMat(color, dosk, 2);
            }
            List<hod> Move = new List<hod>();
            Moves.TrueMoves(Move, dosk, color);//список всех возможных ходов
            if (Move.Count() > 0)
            {
                for (int i = 0; i < Move.Count(); i++)
                    {
                        float[] p = MakeMove(dosk, Move[i]);
                        bool tmp = AlphaBetaAmHod(-color, depth - 1, dosk,All_Moves);
                        UnMakeMove(dosk, Move[i], p);
                        if (color == 1 && tmp)
                        {         
                            All_Moves.Add(new SaveValue(Move[i],depth));
                            return true;
                        }
                        if (color == -1 && tmp == false)
                        {
                        if (All_Moves.Count != 0)
                            if (All_Moves[All_Moves.Count - 1].depth == depth - 1)
                        {
                            All_Moves.RemoveAt(All_Moves.Count - 1);
                            int j = All_Moves.Count - 1;
                                if(j!=-1)
                            while ((All_Moves[j].depth <= depth))
                            {
                                j--;
                                    if (j == -1) break;
                                }
                        }
                           // All_Moves.Clear();
                            return false;
                        } 
                                                                
                    }
                if (color == 1)
                {  if(All_Moves.Count!=0)
                    if (All_Moves[All_Moves.Count - 1].depth == depth - 2)
                    {
                        All_Moves.RemoveAt(All_Moves.Count-1);
                        int i = All_Moves.Count - 1;
                          if (i != -1)
                          while (All_Moves[i].depth <= depth)
                        {
                            i--;
                                if (i == -1) break;
                        }
                    }
                    //All_Moves.Clear();
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        public static bool SahAndMat(int color,pole[,] dosk,int depth)
        {
            List<hod> Move = new List<hod>();
            Moves.TrueMoves(Move, dosk, color);
            int x = Move.Count();
            if (Move.Count == 0 && depth == 2)
            {
                return !Moves.MatOrPat(dosk);
            }
            for (int i = 0; i < Move.Count; i++)
            {
                if (color == 1)
                    if(Move[i].MVVK < 10)
                {
                    return false;

                }else
                {
                    return true;
                }
                float[] p = MakeMove(dosk, Move[i]);
                bool tmp = SahAndMat(-color,dosk,depth-1);
                UnMakeMove(dosk, Move[i], p);
                if (tmp&&depth==1)
                {
                    return true;
                }
                if (tmp == false && depth == 2)
                {
                    return false;
                }
            }
            if (depth == 1)
            {
                return false;
            }
            else
            return true;
        }
        public static void Pic(pole[,] dosk)
        {
            for (int j = 7; j > -1; j--)
            {
                Console.Write("{0} ", j+1);
                for (int i = 0; i < 8; i++)
                {

                    if (dosk[i, j].figure == 1000)
                    {
                        if (dosk[i, j].color == 1)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }
                        Console.Write("P "); Console.ResetColor();
                    }
                    if (dosk[i, j].figure == 3500)
                    {
                        if (dosk[i, j].color == 1)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }
                        Console.Write("H ");
                        Console.ResetColor();
                    }

                    if (dosk[i, j].figure == 3000)
                    {
                        if (dosk[i, j].color == 1)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }
                        Console.Write("E "); Console.ResetColor();
                    }

                    if (dosk[i, j].figure == 9000)
                    {
                        if (dosk[i, j].color == 1)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }
                        Console.Write("F "); Console.ResetColor();
                    }

                    if (dosk[i, j].figure == 5000|| dosk[i, j].figure == 5005)
                    {
                        if (dosk[i, j].color == 1)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }
                        Console.Write("L "); Console.ResetColor();
                    }

                    if (dosk[i, j].figure == 90000|| dosk[i, j].figure == 90005)
                    {
                        if (dosk[i, j].color == 1)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }
                        Console.Write("K "); Console.ResetColor();
                    }
                    if (dosk[i, j].figure < 1000)
                        Console.Write(". ");
                }
                Console.WriteLine();
            }
            Console.Write("  a b c d e f g h");

        }//рисовка на консоли
        public static float[] MakeMove(pole[,] shah, hod hd)//делаем ход
        {
            float[] p = new float[3];//решение проблемы с возвратом обратно, приходится сохранять что было на той позиции в которую сходили.
            if(hd.Fig==king&& Math.Abs(hd.f_x - hd.s_x) > 1)//обработка с королем
            {
                p[0] = 5005;
                shah[hd.s_x, hd.s_y] = shah[hd.f_x, hd.f_y];
                shah[hd.f_x, hd.f_y].figure = 0;
                shah[hd.f_x, hd.f_y].color = 0;
                if (hd.s_x == 6)
                {
                    shah[5, hd.s_y] = shah[7, hd.s_y];
                    shah[7, hd.f_y].figure = 0;
                    shah[7, hd.f_y].color = 0;
                    p[1] = 1;
                    return p;
                }
                if (hd.s_x == 2)
                {
                    shah[3, hd.s_y] = shah[0, hd.s_y];
                    shah[0, hd.s_y].color = 0;
                    shah[0, hd.s_y].figure = 0;
                    p[1] = 2;
                    return p;
                }

            }

            p[0] = shah[hd.s_x, hd.s_y].figure;
            p[1] = shah[hd.s_x, hd.s_y].color;
            if (shah[hd.f_x, hd.f_y].figure!= hd.Fig)//обработка пешки при превращении
            {
                p[2] = Pesh;
                shah[hd.s_x, hd.s_y] = shah[hd.f_x, hd.f_y];
                shah[hd.s_x, hd.s_y].figure = hd.Fig;
            } 
            else
            {
                shah[hd.s_x, hd.s_y] = shah[hd.f_x, hd.f_y];
            }       
            shah[hd.f_x, hd.f_y].color = 0;
            shah[hd.f_x, hd.f_y].figure = 0;
            return p;
        }
        public static void UnMakeMove(pole[,] shah, hod hd, float[] p)
        {
            if (p[0] == 5005)
            {
                shah[hd.f_x, hd.f_y].figure = 90005;
                shah[hd.f_x, hd.f_y].color = shah[hd.s_x, hd.s_y].color;
                shah[hd.s_x, hd.s_y].figure = 0;
                shah[hd.f_x, hd.f_y].figure = 0;
                if(p[1]==1)
                {
                    shah[7, hd.s_y].figure=5005;
                    shah[7, hd.s_y].color = shah[5, hd.f_y].color;
                    shah[5, hd.f_y].figure = 0;
                    shah[5, hd.f_y].color = 0;
                }
                if (p[1] == 2)
                {
                    shah[0, hd.s_y].figure = 5005;
                    shah[0, hd.f_y].color = shah[3, hd.s_y].color;
                    shah[3, hd.s_y].color = 0;
                    shah[3, hd.s_y].figure = 0;
                }

            }
            if (p[2] == Pesh)
            {
                shah[hd.f_x, hd.f_y] = shah[hd.s_x, hd.s_y];
                shah[hd.f_x, hd.f_y].figure = Pesh;
            }
            else
            {
                shah[hd.f_x, hd.f_y] = shah[hd.s_x, hd.s_y];
            }
            shah[hd.s_x, hd.s_y].color = (int)p[1];
            shah[hd.s_x, hd.s_y].figure = p[0];
        }
    }
}
