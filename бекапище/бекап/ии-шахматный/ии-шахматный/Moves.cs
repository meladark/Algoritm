using System.Collections.Generic;

namespace ии_шахматный
{
    class Moves
    {
        const int Pesh = 1000;//ценности фигур, чтобы не забыть
        const int Hor = 3500;
        const int Elp = 3000;
        const int Fers = 9000;
        const int lad = 5000;//5005 готовая для ракировки
        const int king = 90000;//90005 готовый для рокировки
       public struct Coord
        {
            public int x;
            public int y;
            public Coord(int x,int y)
            {
                this.x = x;
                this.y = y;
            }
        }
        public static void TrueMoves(List<hod> Move, pole[,] dosk,int color)
        {
            List<Coord> Coord_Pesh = new List<Coord>();
            List<Coord> Coord_lad = new List<Coord>();
            List<Coord> Coord_hor= new List<Coord>();
            List<Coord> Coord_elp = new List<Coord>();
            List<Coord> Coord_fers = new List<Coord>();
            bool Chek_king = false;
            Coord kinguin = new Coord();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (dosk[i, j].figure != 0&&dosk[i,j].color==color)
                    {
                        if(dosk[i, j].figure == Pesh)
                        {
                            Coord_Pesh.Add(new Coord(i,j));
                            continue;
                        }
                        if (dosk[i, j].figure == lad||dosk[i,j].figure==5005)
                        {
                            Coord_lad.Add(new Coord(i, j));
                            continue;
                        }
                        if (dosk[i, j].figure == Hor)
                        {
                            Coord_hor.Add(new Coord(i, j));
                            continue;
                        }
                        if (dosk[i, j].figure == Elp)
                        {
                            Coord_elp.Add(new Coord(i, j));
                            continue;
                        }
                        if (dosk[i, j].figure == Fers)
                        {
                            Coord_fers.Add(new Coord(i, j));
                            continue;
                        }
                        if (dosk[i, j].figure == king||dosk[i,j].figure==90005)
                        {
                            kinguin = new Coord(i, j);
                            Chek_king = true;
                            continue;
                        }
                    }
                }
            }
            if (Chek_king)
            {
                King(kinguin.x, kinguin.y, dosk, color, Move);
                foreach(var Cord in Coord_fers)
                {
                    EFL(Cord.x,Cord.y,dosk, color, Move, Fers);
                }
                foreach (var Cord in Coord_lad)
                {
                    EFL(Cord.x, Cord.y, dosk, color, Move, lad);
                }
                foreach (var Cord in Coord_elp)
                {
                    EFL(Cord.x, Cord.y, dosk, color, Move, Elp);
                }
                foreach (var Cord in Coord_hor)
                {
                    horse(Cord.x,Cord.y,dosk, color, Move);
                }
                foreach (var Cord in Coord_Pesh)
                {
                    pesh(Cord.x,Cord.y, dosk, color, Move);
                }
            }        
            if (Move.Count == 0)
            {
                return;//пат
            }
            else
            {
               // foreach(var go in Move)
                {
                 //   if (go.MVVK > 0)
                    {                       
                         Move.Sort((a, b) => a.MVVK.CompareTo(b.MVVK) * -1);
                        //break;
                    }
                    
                }
                
            }
            
        }//главный
        public static bool MatOrPat(pole[,] dosk)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if((dosk[i,j].figure==king|| dosk[i, j].figure == 90005) && dosk[i, j].color == -1)
                    {
                        return CanMove(i, j, dosk, -1);
                    }
                }
            }
                    return false;
        }
        public static void pesh(int i,int j,pole[,] dosk, int color, List<hod> move)//генерация всех возможных ходов для всех выбранных пешек
        {
             
                        if (color == 1)//если белые
                        {
                            if (j < 7)//ход вперед на один, не реализовывал случай с удвоенным ходом в начале игры
                                if (dosk[i, j + 1].figure == 0)
                                {
                                    if (j == 1)
                                    {
                                        move.Add(new hod(i, j, i, j + 2, Pesh, 0));
                                    }
                                    if (j+1 == 7)
                                    {
                                        move.Add(new hod(i, j, i, j + 1, Fers, 0));//добавляем в список ходов.
                                        move.Add(new hod(i, j, i, j + 1, Hor, 0));//добавляем в список ходов.
                                        move.Add(new hod(i, j, i, j + 1, lad, 0));//добавляем в список ходов.
                                        move.Add(new hod(i, j, i, j + 1, Elp, 0));
                                    }
                                    else
                                    move.Add(new hod(i,j,i,j+1,Pesh,0));//добавляем в список ходов.
                                }
                            
                            if (j < 7 && i < 7)
                                if (dosk[i + 1, j + 1].color == -color)
                                {
                                    if (j + 1 == 7)
                                    {
                                        move.Add(new hod(i, j, i+1, j + 1, Fers, dosk[i + 1, j + 1].figure / Fers));//добавляем в список ходов.
                                        move.Add(new hod(i, j, i+1, j + 1, Hor, dosk[i + 1, j + 1].figure / Hor));//добавляем в список ходов.
                                        move.Add(new hod(i, j, i + 1, j + 1, Elp, dosk[i + 1, j + 1].figure / Elp));//добавляем в список ходов.
                                        move.Add(new hod(i, j, i + 1, j + 1, lad, dosk[i + 1, j + 1].figure / lad));
                                        
                                    }
                                    else
                                        move.Add(new hod(i, j, i + 1, j + 1, Pesh,dosk[i+1,j+1].figure/Pesh));
                                }
                            if (j < 7 && i > 0)
                                if (dosk[i - 1, j + 1].color == -color)
                                {
                                    if (j + 1== 7)
                                    {
                                        move.Add(new hod(i, j, i - 1, j + 1, lad,  dosk[i - 1, j + 1].figure / lad));
                                        move.Add(new hod(i, j, i - 1, j + 1, Hor, dosk[i - 1, j + 1].figure / Hor));
                                        move.Add(new hod(i, j, i - 1, j + 1, Elp,  dosk[i - 1, j + 1].figure / Elp));
                                        move.Add(new hod(i, j, i-1, j + 1, Fers, dosk[i - 1, j + 1].figure / Fers));//добавляем в список ходов.
                                        
                                    }
                                    else
                                        move.Add(new hod(i, j, i - 1, j + 1, Pesh, dosk[i - 1, j + 1].figure / Pesh));
                                }
                        }

                        else//черные
                        {
                            if (j > 0)
                                if (dosk[i, j - 1].figure == 0)
                                {
                                if (j == 6)
                                {
                                    move.Add(new hod(i, j, i, j - 1, Pesh, 0));
                                }
                                    if (j -1 == 0)
                                    {
                                        move.Add(new hod(i, j, i, j - 1, Fers, 0));
                                        move.Add(new hod(i, j, i, j - 1, Hor,  0));
                                        move.Add(new hod(i, j, i, j - 1, lad,  0));
                                        move.Add(new hod(i, j, i, j - 1, Elp,  0));
                                    }
                                    else
                                    move.Add(new hod(i, j, i, j - 1, Pesh, 0));
                                }
                            if (i > 0 && j > 0)
                                if (dosk[i - 1, j - 1].color == -color)
                                {
                                    if (j - 1== 0)
                                    {
                                        move.Add(new hod(i, j, i-1, j - 1, Fers,  dosk[i - 1, j - 1].figure / Fers));
                                        move.Add(new hod(i, j, i-1, j - 1, Hor,  dosk[i - 1, j - 1].figure / Hor));
                                        move.Add(new hod(i, j, i - 1, j - 1, lad,  dosk[i - 1, j - 1].figure / lad));
                                        move.Add(new hod(i, j, i - 1, j - 1, Elp,  dosk[i - 1, j - 1].figure / Elp));
                                    }
                                    else
                                        move.Add(new hod(i, j, i - 1, j - 1, Pesh, dosk[i - 1, j - 1].figure / Pesh));
                                }
                            if (j > 0 && i < 7)
                                if (dosk[i + 1, j - 1].color == -color)
                                {
                                    if (j - 1 == 0)
                                    {
                                        move.Add(new hod(i, j, i + 1, j - 1, Fers, dosk[i + 1, j - 1].figure / Fers));
                                        move.Add(new hod(i, j, i + 1, j - 1, Hor, dosk[i + 1, j - 1].figure / Hor));
                                        move.Add(new hod(i, j, i + 1, j - 1, Elp, dosk[i + 1, j - 1].figure / Elp));
                                        move.Add(new hod(i, j, i + 1, j - 1, lad, dosk[i + 1, j - 1].figure / lad));
                                    }
                                    else
                                        move.Add(new hod(i, j, i + 1, j - 1, Pesh, dosk[i + 1, j - 1].figure / Pesh));
                                }
                        }
            return;
        }
        public static void horse(int i, int j,pole[,] dosk, int color, List<hod> move)
        {
                        if (i > 0 && j < 6)
                            if(dosk[i-1,j+2].color!=color)
                        {
                            move.Add(new hod(i,j,i-1,j+2,Hor,dosk[i-1,j+2].figure/Hor));
                        }
                        if (i > 1 && j < 7)
                            if (dosk[i-2, j+1].color != color)
                            {
                            move.Add(new hod(i, j, i - 2, j + 1, Hor, dosk[i - 2, j + 1].figure / Hor));
                        }
                        if (i < 7 && j < 6)
                            if (dosk[i+1, j+2].color != color)
                            {
                            move.Add(new hod(i, j, i + 1, j + 2, Hor, dosk[i + 1, j + 2].figure / Hor));
                        }
                        if (i < 6 && j < 7)
                            if (dosk[i+2, j+1].color != color)
                            {
                            move.Add(new hod(i, j, i + 2, j + 1, Hor, dosk[i + 2, j + 1].figure / Hor));
                        }
                        if (i < 6 && j > 0)
                            if (dosk[i+2, j-1].color != color)
                            {
                            move.Add(new hod(i, j, i + 2, j - 1, Hor, dosk[i + 2, j - 1].figure / Hor));
                        }
                        if (i < 7 && j > 1)
                            if (dosk[i+1, j-2].color != color)
                            {
                            move.Add(new hod(i, j, i + 1, j - 2, Hor, dosk[i + 1, j - 2].figure / Hor));
                        }
                        if (i > 0 && j > 1)
                            if (dosk[i-1, j-2].color != color)
                            {
                            move.Add(new hod(i, j, i - 1, j - 2, Hor, dosk[i - 1, j - 2].figure / Hor));
                        }
                        if (i > 1 && j > 0)
                            if (dosk[i-2, j-1].color != color)
                            {
                            move.Add(new hod(i, j, i - 2, j - 1, Hor, dosk[i - 2, j - 1].figure / Hor));
                        }
            return;
        }//ходы коней
        public static void EFL(int i,int j,pole[,] dosk, int color, List<hod> move, int Figur)
        {
                        int i_old = i;
                        int j_old = j;
                        if (Figur == Fers || Figur == Elp)//дамка или слон, диагональ
                        {

                            while ((i < 7 && j < 7))
                            {
                                if (dosk[i + 1, j + 1].figure != 0)
                                    if (dosk[i + 1, j + 1].color == color)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        move.Add(new hod(i_old,j_old,i+1,j+1,Figur,dosk[i+1,j+1].figure/Figur));
                                        break;
                                    }
                                move.Add(new hod(i_old, j_old, i + 1, j + 1, Figur,0));
                                i = i + 1;
                                j = j + 1;
                            }
                            i = i_old;
                            j = j_old;
                            while ((i > 0 && j < 7))
                            {
                                if (dosk[i - 1, j + 1].figure != 0)
                                    if (dosk[i - 1, j + 1].color == color)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        move.Add(new hod(i_old, j_old, i - 1, j + 1, Figur, dosk[i - 1, j + 1].figure / Figur));
                                        break;
                                    }
                                
                                move.Add(new hod(i_old, j_old, i - 1, j + 1, Figur, 0));
                                i = i - 1;
                                j = j + 1;
                            }
                            i = i_old;
                            j = j_old;
                            while ((i > 0 && j > 0))
                            {
                                if (dosk[i - 1, j - 1].figure != 0)
                                    if (dosk[i - 1, j - 1].color == color)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        move.Add(new hod(i_old, j_old, i - 1, j - 1, Figur, dosk[i - 1, j - 1].figure / Figur));
                                        break;
                                    }
                                move.Add(new hod(i_old, j_old, i - 1, j - 1, Figur, 0));
                                i = i - 1;
                                j = j - 1;
                            }
                            i = i_old;
                            j = j_old;
                            while (i < 7 && j > 0)
                            {
                                if (dosk[i + 1, j - 1].figure != 0)
                                    if (dosk[i + 1, j - 1].color == color)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        move.Add(new hod(i_old, j_old, i + 1, j - 1, Figur,dosk[i + 1, j - 1].figure / Figur));
                                        break;
                                    }
                                
                                move.Add(new hod(i_old, j_old, i + 1, j - 1, Figur, 0));
                                i = i + 1;
                                j = j - 1;
                            }
                            i = i_old;
                            j = j_old;
                        }
                        if (Figur == Fers || Figur == lad)//ферзь и ладья
                        {
                            while (i < 7)
                            {
                                if (dosk[i + 1, j].figure != 0)
                                    if (dosk[i + 1, j].color == color)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        move.Add(new hod(i_old, j_old, i + 1, j, Figur, dosk[i + 1, j].figure / Figur));
                                        break;
                                    }
                                move.Add(new hod(i_old, j_old, i + 1, j, Figur,0));
                                i = i + 1;         
                            }
                            i = i_old;
                            j = j_old;
                            while (i > 0)
                            {
                                if (dosk[i - 1, j].figure != 0)
                                    if (dosk[i - 1, j].color == color)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        move.Add(new hod(i_old, j_old, i - 1, j, Figur, dosk[i - 1, j].figure / Figur));
                                        break;
                                    }
                                move.Add(new hod(i_old, j_old, i - 1, j, Figur, 0));
                                i = i - 1;
                               
                            }
                            i = i_old;
                            j = j_old;
                            while (j < 7)
                            {
                                if (dosk[i, j + 1].figure != 0)
                                    if (dosk[i, j + 1].color == color)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        move.Add(new hod(i_old, j_old, i, j + 1, Figur, dosk[i, j + 1].figure / Figur));
                                        break;
                                    }
                                move.Add(new hod(i_old, j_old, i, j + 1, Figur, 0));
                                j = j + 1;                               
                            }
                            i = i_old;
                            j = j_old;
                            while (j > 0)
                            {
                                if (dosk[i, j - 1].figure != 0)
                                    if (dosk[i, j - 1].color == color)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        move.Add(new hod(i_old, j_old, i, j - 1, Figur, dosk[i, j - 1].figure / Figur));
                                        break;
                                    }
                                move.Add(new hod(i_old, j_old, i, j - 1, Figur, 0));
                                j = j - 1;
                            }
                            i = i_old;
                            j = j_old;
                        }
            return;
        }//Слон, ладья, ферзь
        public static bool CanMove(int i, int j, pole[,] dosk, int color)
        {
            int old_j = j;
            int old_i = i;
            while (j < 7)
            {
                if (dosk[i, j + 1].color == -color&&dosk[i, j + 1].figure!=0)
                {
                    if (dosk[i, j + 1].figure == Fers || dosk[i, j + 1].figure == lad|| dosk[i, j + 1].figure == 5005)
                    {
                        return false;
                    }
                }
                else
                {
                    break;
                }
                j++;
            }
            j = old_j;
            while (j > 0)
            {
                if (dosk[i, j - 1].figure!=0)
                {
                    if (dosk[i, j - 1].color == -color && (dosk[i, j - 1].figure == Fers || dosk[i, j - 1].figure == lad|| dosk[i, j - 1].figure ==5005))
                    {
                        return false;
                    }
                    else
                    {
                        break;
                    }
                }
                j--;
            }
            j = old_j;
            while (i > 0)
            {
                if (dosk[i - 1, j].figure!=0)
                {
                    if (dosk[i - 1, j].color == -color && (dosk[i - 1, j].figure == Fers || dosk[i - 1, j].figure == lad|| dosk[i - 1, j].figure ==5005))
                    {
                        return false;
                    }
                    else
                    {
                        break;
                    }
                }
                i--;
            }
            i = old_i;
            while (i < 7)
            {
                if (dosk[i + 1, j].figure!=0)
                {
                    if (dosk[i + 1, j].color == -color && (dosk[i + 1, j].figure == Fers || dosk[i + 1, j].figure == lad|| dosk[i + 1, j].figure ==5005))
                    {
                        return false;
                    }
                    else
                    {
                        break;
                    }
                }
                
                i++;
            }
            i = old_i;
            //слон+
            while (i < 7 && j < 7)
            {
                if (dosk[i + 1, j + 1].figure!=0)
                {
                    if (dosk[i + 1, j + 1].color == -color && (dosk[i + 1, j + 1].figure == Fers || dosk[i + 1, j + 1].figure == Elp))
                    {
                        return false;
                    }
                    else
                    {
                        break;
                    }
                } 
                i++;
                j++;
            }
            i = old_i;
            j = old_j;
            while (i > 0 && j < 7)
            {
                if ( dosk[i - 1, j + 1].figure!=0)
                {
                    if (dosk[i - 1, j + 1].color == -color && (dosk[i - 1, j + 1].figure == Fers || dosk[i - 1, j + 1].figure == Elp))
                    {
                        return false;
                    }
                    else
                    {
                        break;
                    }
                }
                i--;
                j++;
            }
            i = old_i;
            j = old_j;
            while (i < 7 && j > 0)
            {
                if ( dosk[i + 1, j - 1].figure!=0)
                {
                    if (dosk[i + 1, j - 1].color == -color && (dosk[i + 1, j - 1].figure == Fers || dosk[i + 1, j - 1].figure == Elp))
                    {
                        return false;
                    }
                    else
                    {
                        break;
                    }
                }
                i++;
                j--;
            }
            i = old_i;
            j = old_j;
            while (i > 0 && j > 0)
            {
                if ( dosk[i - 1, j - 1].figure!=0)
                {
                    if (dosk[i - 1, j - 1].color == -color && (dosk[i - 1, j - 1].figure == Fers || dosk[i - 1, j - 1].figure == Elp))
                    {
                        return false;
                    }
                    else break;
                }
                i--;
                j--;
            }
            i = old_i;
            j = old_j;
            //пешки
            if (color == -1)
            {
                if (i + 1 < 8 && j - 1 > -1)
                    if (dosk[i + 1, j - 1].figure == Pesh && dosk[i + 1, j - 1].color == -color)
                    {
                        return false;
                    }
                if (i - 1 > -1 && j - 1 > -1)
                {
                    if (dosk[i - 1, j - 1].figure == Pesh && dosk[i - 1, j - 1].color == -color)
                    {
                        return false;
                    }
                }
            }
            else
            {
                if (i + 1 < 8 && j + 1 < 8)
                    if (dosk[i + 1, j + 1].figure == Pesh && dosk[i + 1, j + 1].color == -color)
                    {
                        return false;
                    }
                if (i - 1 > -1 && j + 1 < 8)
                {
                    if (dosk[i - 1, j + 1].figure == Pesh && dosk[i - 1, j + 1].color == -color)
                    {
                        return false;
                    }
                }
            }
            i = old_i;
            j = old_j;
            if (i + 2 < 8 && j + 1 < 8)
            {
                if (dosk[i + 2, j + 1].color == -color && dosk[i + 2, j + 1].figure == Hor)
                {
                    return false;
                }
            }
            if (i + 1 < 8 && j + 2 < 8)
            {
                if (dosk[i + 1, j + 2].color == -color && dosk[i + 1, j + 2].figure == Hor)
                {
                    return false;
                }
            }
            if (i - 1 > -1 && j + 2 < 8)
            {
                if (dosk[i - 1, j + 2].color == -color && dosk[i - 1, j + 2].figure == Hor)
                {
                    return false;
                }
            }
            if (i - 2 > -1 && j + 1 < 8)
            {
                if (dosk[i - 2, j + 1].color == -color && dosk[i - 2, j + 1].figure == Hor)
                {
                    return false;
                }
            }
            if (i - 1 > -1 && j - 2 > -1)
            {
                if (dosk[i - 1, j - 2].color == -color && dosk[i - 1, j - 2].figure == Hor)
                {
                    return false;
                }
            }
            if (i - 2 > -1 && j - 1 > -1)
            {
                if (dosk[i - 2, j - 1].color == -color && dosk[i - 2, j - 1].figure == Hor)
                {
                    return false;
                }
            }
            if (i + 1 < 8 && j - 2 > -1)
            {
                if (dosk[i + 1, j - 2].color == -color && dosk[i + 1, j - 2].figure == Hor)
                {
                    return false;
                }
            }
            if (i + 2 < 8 && j - 1 > -1)
            {
                if (dosk[i + 2, j - 1].color == -color && dosk[i + 2, j - 1].figure == Hor)
                {
                    return false;
                }
            }
            if (i < 7)
            {
                if (dosk[i + 1, j].color == -color && (dosk[i + 1, j].figure == king|| dosk[i + 1, j].figure ==90005))
                {
                    return false;
                }
                if (j < 7)
                {
                    if (dosk[i + 1, j + 1].color == -color && (dosk[i + 1, j + 1].figure == king|| dosk[i + 1, j + 1].figure ==90005))
                    {
                        return false;
                    }
                }
                if (j > 0)
                {
                    if (dosk[i + 1, j - 1].color == -color && (dosk[i + 1, j - 1].figure == king|| dosk[i + 1, j - 1].figure ==90005))
                    {
                        return false;
                    }
                }
            }
            if (i > 0)
            {
                if (dosk[i - 1, j].color == -color && (dosk[i - 1, j].figure == king|| dosk[i - 1, j].figure ==90005))
                {
                    return false;
                }
                if (j < 7)
                {
                    if (dosk[i - 1, j + 1].color == -color && (dosk[i - 1, j + 1].figure == king|| dosk[i - 1, j + 1].figure ==90005))
                    {
                        return false;
                    }
                }
                if (j > 0)
                {
                    if (dosk[i - 1, j - 1].color == -color && (dosk[i - 1, j - 1].figure == king|| dosk[i - 1, j - 1].figure ==90005))
                    {
                        return false;
                    }
                }
            }
            if (j < 7)
            {
                if (dosk[i, j + 1].color == -color && (dosk[i, j + 1].figure == king|| dosk[i, j + 1].figure ==90005))
                {
                    return false;
                }
            }
            if (j > 0)
            {
                if (dosk[i, j - 1].color == -color && (dosk[i, j - 1].figure == king|| dosk[i, j - 1].figure == 90005))
                {
                    return false;
                }
            }
            return true;
        }//возможность хода короля в данную позицию, проверка на бой
        public static void King(int i, int j,pole[,] dosk, int color, List<hod> move)
        {
            //рокировка 
            if (dosk[i, j].figure == 90005)
            {
                if (dosk[7, j].figure == 5005)
                {
                    if(dosk[i+1,j].figure==0&&dosk[i+2,j].figure==0)
                        if (CanMove(i, j, dosk, color) && CanMove(i + 1, j, dosk, color) && CanMove(i + 2, j, dosk, color))
                        {
                            move.Add(new hod(i, j, i + 2, j, king, 0));
                        }
                }
                if (dosk[0, j].figure == 5005)
                {
                    if (dosk[i - 1, j].figure == 0 && dosk[i + 2, j].figure == 0)
                        if (CanMove(i, j, dosk, color) && CanMove(i - 1, j, dosk, color) && CanMove(i - 2, j, dosk, color))
                        {
                            move.Add(new hod(i, j, i - 2, j, king, 0));
                        }
                }
            }


                        if (j < 7)//проверка стоит ли он не на самом краю
                        {
                            if (dosk[i, j + 1].color != color && CanMove(i, j + 1, dosk, color))//проверка, может ли он пойти вверх
                            {
                                move.Add(new hod(i, j, i, j + 1, king, dosk[i, j + 1].figure / king));
                            }
                            {
                                if (i < 7 && CanMove(i + 1, j + 1, dosk, color))//вложенная проверка пойти вверх и вправо
                                        if (dosk[i+1, j + 1].color != color)
                                {
                                    move.Add(new hod(i, j, i + 1, j + 1, king, dosk[i + 1, j + 1].figure / king));
                                }
                                if (i > 0 && CanMove(i - 1, j + 1, dosk, color))//вверх и влево
                                        if (dosk[i-1, j + 1].color != color)
                                 {
                                    move.Add(new hod(i, j, i - 1, j + 1, king, dosk[i - 1, j + 1].figure / king));
                                }
                            }
                        }
                            


                        if (j > 0)//на нижнем краю
                        {
                            if (dosk[i, j - 1].color != color && CanMove(i, j - 1, dosk, color))//пойти вниз
                            {
                                move.Add(new hod(i, j, i, j - 1, king, dosk[i, j - 1].figure / king));
                            }
                                
                                if (i < 7 && CanMove(i + 1, j - 1, dosk, color))//пойти вниз и вправо
                                    if (dosk[i + 1, j - 1].color != color)
                                {
                                    move.Add(new hod(i, j, i + 1, j - 1, king,dosk[i + 1, j - 1].figure / king));
                                }

                                if (i > 0 && CanMove(i - 1, j - 1, dosk, color))//пойти вниз и влево
                                      if (dosk[i - 1, j - 1].color != color)
                                {
                                    move.Add(new hod(i, j, i - 1, j - 1, king, dosk[i - 1, j - 1].figure / king));
                                }
                        }
                           
                        if (i > 0 && CanMove(i - 1, j, dosk, color))//пойти влево
                            if (dosk[i-1, j].color != color)
                            {
                            move.Add(new hod(i, j, i - 1, j, king, dosk[i - 1, j].figure / king));
                        }
                        if (i < 7 && CanMove(i + 1, j, dosk, color))//пойти вправо
                            if (dosk[i+1, j].color != color)
                            {
                            move.Add(new hod(i,j,i+1,j,king, dosk[i + 1, j].figure / king));
                        }
        }//все ходы короля
    }
}
