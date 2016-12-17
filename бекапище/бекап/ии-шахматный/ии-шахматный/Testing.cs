using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.IO;

namespace ии_шахматный
{
    [TestFixture]
    class Testing
    {
        const float Pesh = 1000;//ценности фигур, чтобы не забыть
        const float Hor = 3500;
        const float Elp = 3000;
        const float Fers = 9000;
        const float lad = 5000;
        const float king = 90000;
        [Test]
        public void TestEnter()
        {
            pole[,] Pl = new  pole[8, 8];

            Enter_Exit.Enter(Pl,new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "Base.txt"));
            pole[,] PlTrue = new pole[8, 8];
            PlTrue[4, 0].color = 1;
            PlTrue[4, 0].figure = 3500;

            PlTrue[6, 0].color = 1;
            PlTrue[6, 0].figure = 9000;

            PlTrue[3, 1].color = 1;
            PlTrue[3, 1].figure = 1000;

            PlTrue[4, 1].color = -1;
            PlTrue[4, 1].figure = 90000;

            PlTrue[3, 2].color = -1;
            PlTrue[3, 2].figure = 1000;

            PlTrue[3, 5].color = 1;
            PlTrue[3, 5].figure = 90000;

            PlTrue[4, 5].color = 1;
            PlTrue[4, 5].figure = 1000;

            PlTrue[6, 5].color = 1;
            PlTrue[6, 5].figure = 3500;

            PlTrue[7, 6].color = 1;
            PlTrue[7, 6].figure = 3000;

            Assert.AreEqual(Pl, PlTrue);
        }
        [Test]
        public void TestExit()
        { 
            List<SaveValue> Position = new List<SaveValue>();
            Position.Add(new SaveValue(new hod(6,0,6,3,9000,0),1));
            Position.Add(new SaveValue(new hod(7,6,4,3,3000,0),3));
            Position.Add(new SaveValue(new hod(6,5,5,3,3500,0),5));;
            Position.Add(new SaveValue(new hod(4,0,5,2,3500,0),7));
            string OutPut = AppDomain.CurrentDomain.BaseDirectory + "Outp.txt";
            string OutPutVs = AppDomain.CurrentDomain.BaseDirectory + "OutpVS.txt";
            StreamWriter fd = new StreamWriter(OutPut);
            Enter_Exit.Exit(Position,fd);
            File.WriteAllText(OutPutVs, "1) N [e1]=>[f3]\r\n2) N [g6]=>[f4]\r\n3) B [h7]=>[e4]\r\n4) Q [g1]=>[g4]");
            Assert.AreEqual(File.ReadAllText(OutPut),File.ReadAllText(OutPutVs));
        }
      
        [Test]
        public void TestMoves()
        {
            pole[,] dosk = new pole[8, 8];
            StreamReader Sr = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "Base.txt");
            int f = Enter_Exit.Enter(dosk,Sr);
            string Adres = AppDomain.CurrentDomain.BaseDirectory + "TestingMove.txt";
            StreamWriter fd = new StreamWriter(Adres);
            List<Moves.Coord> Coord_Pesh = new List<Moves.Coord>();
            List<Moves.Coord> Coord_lad = new List<Moves.Coord>();
            List<Moves.Coord> Coord_hor = new List<Moves.Coord>();
            List<Moves.Coord> Coord_elp = new List<Moves.Coord>();
            List<Moves.Coord> Coord_fers = new List<Moves.Coord>();
            List<Moves.Coord> kinguin = new List<Moves.Coord>();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (dosk[i, j].figure != 0)
                    {
                        if (dosk[i, j].figure == Pesh)
                        {
                            Coord_Pesh.Add(new Moves.Coord(i, j));
                        }
                        if (dosk[i, j].figure == lad||dosk[i,j].figure==5005)
                        {
                            Coord_lad.Add(new Moves.Coord(i, j));
                        }
                        if (dosk[i, j].figure == Hor)
                        {
                            Coord_hor.Add(new Moves.Coord(i, j));
                        }
                        if (dosk[i, j].figure == Elp)
                        {
                            Coord_elp.Add(new Moves.Coord(i, j));
                        }
                        if (dosk[i, j].figure == Fers)
                        {
                            Coord_fers.Add(new Moves.Coord(i, j));
                        }
                        if (dosk[i, j].figure == king||dosk[i,j].figure==90005)
                        {
                            kinguin.Add(new Moves.Coord(i, j));
                        }
                    }
                }
            }

            foreach (var mov in kinguin)
            {
                List<hod> Move = new List<hod>();
                Moves.King(mov.x, mov.y, dosk, dosk[mov.x, mov.y].color, Move);
                fd.Write("Фигура {0}, [{1},{2}]","Король",Enter_Exit.Convert_toChar(mov.x), mov.y + 1);
                fd.WriteLine();
                
                for (int j = 7; j > -1; j--)
                {
                    fd.Write("{0} ", j + 1);
                    for (int i = 0; i < 8; i++)
                    {
                        bool trust = true;
                        foreach (var OneMove in Move)
                        {
                            if (OneMove.s_x == i && OneMove.s_y == j)
                            {
                                fd.Write("+ ");
                                trust = false;
                            }
                        }
                        if (trust)
                        {
                            if (dosk[i, j].figure == 1000)
                            {
                                if (dosk[i, j].color == 1)
                                {
                                    fd.Write("\u2659 ");
                                }
                                else
                                {
                                    fd.Write("\u265F ");
                                }
                            }
                            if (dosk[i, j].figure == 3500)
                            {
                                if (dosk[i, j].color == 1)
                                {
                                    fd.Write("\u2658 ");
                                }
                                else
                                {
                                    fd.Write("\u265E ");
                                }
                            }

                            if (dosk[i, j].figure == 3000)
                            {
                                if (dosk[i, j].color == 1)
                                {
                                    fd.Write("\u2657 ");
                                }
                                else
                                {
                                    fd.Write("\u265D ");
                                }
                            }

                            if (dosk[i, j].figure == 9000)
                            {
                                if (dosk[i, j].color == 1)
                                {
                                    fd.Write("\u2655 ");
                                }
                                else
                                {
                                    fd.Write("\u265B ");
                                }
                            }

                            if (dosk[i, j].figure == 5000)
                            {
                                if (dosk[i, j].color == 1)
                                {
                                    fd.Write("\u2656 ");
                                }
                                else
                                {
                                    fd.Write("\u265C ");
                                }
                            }

                            if (dosk[i, j].figure == 90000)
                            {
                                if (dosk[i, j].color == 1)
                                {
                                    fd.Write("\u2654 ");
                                }
                                else
                                {
                                    fd.Write("\u265A ");
                                }
                            }

                            if (dosk[i, j].figure < 1000)
                                fd.Write(". ");
                        }
                       
                    }   fd.WriteLine();    
                }
                fd.Write("  a b c d e f g h");
                fd.WriteLine();
            }

            foreach (var mov in Coord_fers)
            {
                List<hod> Move = new List<hod>();
                Moves.EFL(mov.x, mov.y, dosk, dosk[mov.x, mov.y].color, Move,9000);
                fd.Write("Фигура {0}, [{1},{2}]", "Ферзь", Enter_Exit.Convert_toChar(mov.x), mov.y+1);
                fd.WriteLine();
                for (int j = 7; j > -1; j--)
                {
                    fd.Write("{0} ", j + 1);
                    for (int i = 0; i < 8; i++)
                    {
                        bool trust = true;
                        foreach (var OneMove in Move)
                        {
                            if (OneMove.s_x == i && OneMove.s_y == j)
                            {
                                fd.Write("+ ");
                                trust = false;
                            }
                        }
                        if (trust)
                        {
                            if (dosk[i, j].figure == 1000)
                            {
                                if (dosk[i, j].color == 1)
                                {
                                    fd.Write("\u2659 ");
                                }
                                else
                                {
                                    fd.Write("\u265F ");
                                }
                            }
                            if (dosk[i, j].figure == 3500)
                            {
                                if (dosk[i, j].color == 1)
                                {
                                    fd.Write("\u2658 ");
                                }
                                else
                                {
                                    fd.Write("\u265E ");
                                }
                            }

                            if (dosk[i, j].figure == 3000)
                            {
                                if (dosk[i, j].color == 1)
                                {
                                    fd.Write("\u2657 ");
                                }
                                else
                                {
                                    fd.Write("\u265D ");
                                }
                            }

                            if (dosk[i, j].figure == 9000)
                            {
                                if (dosk[i, j].color == 1)
                                {
                                    fd.Write("\u2655 ");
                                }
                                else
                                {
                                    fd.Write("\u265B ");
                                }
                            }

                            if (dosk[i, j].figure == 5000)
                            {
                                if (dosk[i, j].color == 1)
                                {
                                    fd.Write("\u2656 ");
                                }
                                else
                                {
                                    fd.Write("\u265C ");
                                }
                            }

                            if (dosk[i, j].figure == 90000)
                            {
                                if (dosk[i, j].color == 1)
                                {
                                    fd.Write("\u2654 ");
                                }
                                else
                                {
                                    fd.Write("\u265A ");
                                }
                            }

                            if (dosk[i, j].figure < 1000)
                                fd.Write(". ");
                        }
                    }
                    fd.WriteLine();
                }
                fd.Write("  a b c d e f g h");
                fd.WriteLine();
            }

            foreach (var mov in Coord_elp)
            {
                List<hod> Move = new List<hod>();
                Moves.EFL(mov.x, mov.y, dosk, dosk[mov.x, mov.y].color, Move,3000);
                fd.Write("Фигура {0}, [{1},{2}]", "Слон", Enter_Exit.Convert_toChar(mov.x), mov.y + 1);
                fd.WriteLine();
                for (int j = 7; j > -1; j--)
                {
                    fd.Write("{0} ", j + 1);
                    for (int i = 0; i < 8; i++)
                    {
                        bool trust = true;
                        foreach (var OneMove in Move)
                        {
                            if (OneMove.s_x == i && OneMove.s_y == j)
                            {
                                fd.Write("+ ");
                                trust = false;
                            }
                        }
                        if (trust)
                        {
                            if (dosk[i, j].figure == 1000)
                            {
                                if (dosk[i, j].color == 1)
                                {
                                    fd.Write("\u2659 ");
                                }
                                else
                                {
                                    fd.Write("\u265F ");
                                }
                            }
                            if (dosk[i, j].figure == 3500)
                            {
                                if (dosk[i, j].color == 1)
                                {
                                    fd.Write("\u2658 ");
                                }
                                else
                                {
                                    fd.Write("\u265E ");
                                }
                            }

                            if (dosk[i, j].figure == 3000)
                            {
                                if (dosk[i, j].color == 1)
                                {
                                    fd.Write("\u2657 ");
                                }
                                else
                                {
                                    fd.Write("\u265D ");
                                }
                            }

                            if (dosk[i, j].figure == 9000)
                            {
                                if (dosk[i, j].color == 1)
                                {
                                    fd.Write("\u2655 ");
                                }
                                else
                                {
                                    fd.Write("\u265B ");
                                }
                            }

                            if (dosk[i, j].figure == 5000)
                            {
                                if (dosk[i, j].color == 1)
                                {
                                    fd.Write("\u2656 ");
                                }
                                else
                                {
                                    fd.Write("\u265C ");
                                }
                            }

                            if (dosk[i, j].figure == 90000)
                            {
                                if (dosk[i, j].color == 1)
                                {
                                    fd.Write("\u2654 ");
                                }
                                else
                                {
                                    fd.Write("\u265A ");
                                }
                            }

                            if (dosk[i, j].figure < 1000)
                                fd.Write(". ");
                        }
                    }
                    fd.WriteLine();
                }
                fd.Write("  a b c d e f g h");
                fd.WriteLine();
            }

            foreach (var mov in Coord_hor)
            {
                List<hod> Move = new List<hod>();
                Moves.horse(mov.x, mov.y, dosk, dosk[mov.x, mov.y].color, Move);
                fd.Write("Фигура {0}, [{1},{2}]", "Конь", Enter_Exit.Convert_toChar(mov.x), mov.y + 1);
                fd.WriteLine();
                for (int j = 7; j > -1; j--)
                {
                    fd.Write("{0} ", j + 1);
                    for (int i = 0; i < 8; i++)
                    {
                        bool trust = true;
                        foreach (var OneMove in Move)
                        {
                            if (OneMove.s_x == i && OneMove.s_y == j)
                            {
                                fd.Write("+ ");
                                trust = false;
                            }
                        }
                        if (trust)
                        {
                            if (dosk[i, j].figure == 1000)
                            {
                                if (dosk[i, j].color == 1)
                                {
                                    fd.Write("\u2659 ");
                                }
                                else
                                {
                                    fd.Write("\u265F ");
                                }
                            }
                            if (dosk[i, j].figure == 3500)
                            {
                                if (dosk[i, j].color == 1)
                                {
                                    fd.Write("\u2658 ");
                                }
                                else
                                {
                                    fd.Write("\u265E ");
                                }
                            }

                            if (dosk[i, j].figure == 3000)
                            {
                                if (dosk[i, j].color == 1)
                                {
                                    fd.Write("\u2657 ");
                                }
                                else
                                {
                                    fd.Write("\u265D ");
                                }
                            }

                            if (dosk[i, j].figure == 9000)
                            {
                                if (dosk[i, j].color == 1)
                                {
                                    fd.Write("\u2655 ");
                                }
                                else
                                {
                                    fd.Write("\u265B ");
                                }
                            }

                            if (dosk[i, j].figure == 5000)
                            {
                                if (dosk[i, j].color == 1)
                                {
                                    fd.Write("\u2656 ");
                                }
                                else
                                {
                                    fd.Write("\u265C ");
                                }
                            }

                            if (dosk[i, j].figure == 90000)
                            {
                                if (dosk[i, j].color == 1)
                                {
                                    fd.Write("\u2654 ");
                                }
                                else
                                {
                                    fd.Write("\u265A ");
                                }
                            }

                            if (dosk[i, j].figure < 1000)
                                fd.Write(". ");
                        }
                    }
                    fd.WriteLine();
                }
                fd.Write("  a b c d e f g h");
                fd.WriteLine();
            }

            foreach (var mov in Coord_lad)
            {
                List<hod> Move = new List<hod>();
                Moves.EFL(mov.x, mov.y, dosk, dosk[mov.x, mov.y].color, Move,(int)lad);
                fd.Write("Фигура {0}, [{1},{2}]", "Ладья", Enter_Exit.Convert_toChar(mov.x), mov.y + 1);
                fd.WriteLine();
                for (int j = 7; j > -1; j--)
                {
                    fd.Write("{0} ", j + 1);
                    for (int i = 0; i < 8; i++)
                    {
                        bool trust = true;
                        foreach (var OneMove in Move)
                        {
                            if (OneMove.s_x == i && OneMove.s_y == j)
                            {
                                fd.Write("+ ");
                                trust = false;
                            }
                        }
                        if (trust)
                        {
                            if (dosk[i, j].figure == 1000)
                            {
                                if (dosk[i, j].color == 1)
                                {
                                    fd.Write("\u2659 ");
                                }
                                else
                                {
                                    fd.Write("\u265F ");
                                }
                            }
                            if (dosk[i, j].figure == 3500)
                            {
                                if (dosk[i, j].color == 1)
                                {
                                    fd.Write("\u2658 ");
                                }
                                else
                                {
                                    fd.Write("\u265E ");
                                }
                            }

                            if (dosk[i, j].figure == 3000)
                            {
                                if (dosk[i, j].color == 1)
                                {
                                    fd.Write("\u2657 ");
                                }
                                else
                                {
                                    fd.Write("\u265D ");
                                }
                            }

                            if (dosk[i, j].figure == 9000)
                            {
                                if (dosk[i, j].color == 1)
                                {
                                    fd.Write("\u2655 ");
                                }
                                else
                                {
                                    fd.Write("\u265B ");
                                }
                            }

                            if (dosk[i, j].figure == 5000)
                            {
                                if (dosk[i, j].color == 1)
                                {
                                    fd.Write("\u2656 ");
                                }
                                else
                                {
                                    fd.Write("\u265C ");
                                }
                            }

                            if (dosk[i, j].figure == 90000)
                            {
                                if (dosk[i, j].color == 1)
                                {
                                    fd.Write("\u2654 ");
                                }
                                else
                                {
                                    fd.Write("\u265A ");
                                }
                            }

                            if (dosk[i, j].figure < 1000)
                                fd.Write(". ");
                        }
                    }
                    fd.WriteLine();
                }
                fd.Write("  a b c d e f g h");
                fd.WriteLine();
            }

            foreach (var mov in Coord_Pesh)
            {
                List<hod> Move = new List<hod>();
                Moves.pesh(mov.x, mov.y, dosk, dosk[mov.x, mov.y].color, Move);
                fd.Write("Фигура {0}, [{1},{2}]","Пешка", Enter_Exit.Convert_toChar(mov.x), mov.y + 1);
                fd.WriteLine();
                for (int j = 7; j > -1; j--)
                {
                    fd.Write("{0} ", j+1);
                    for (int i = 0; i < 8; i++)
                    {
                        bool trust = true;
                        foreach (var OneMove in Move)
                        {
                            if (OneMove.s_x == i && OneMove.s_y == j)
                            {
                                fd.Write("+ ");
                                trust = false;
                            }
                        }
                        if (trust)
                        {
                            if (dosk[i, j].figure == 1000)
                            {
                                if (dosk[i, j].color == 1)
                                {
                                    fd.Write("\u2659 ");
                                }else
                                {
                                    fd.Write("\u265F ");
                                }
                            }
                            if (dosk[i, j].figure == 3500)
                            {
                                if (dosk[i, j].color == 1)
                                {
                                    fd.Write("\u2658 ");
                                }
                                else
                                {
                                    fd.Write("\u265E ");
                                }
                            }

                            if (dosk[i, j].figure == 3000)
                            {
                                if (dosk[i, j].color == 1)
                                {
                                    fd.Write("\u2657 ");
                                }
                                else
                                {
                                    fd.Write("\u265D ");
                                }
                            }

                            if (dosk[i, j].figure == 9000)
                            {
                                if (dosk[i, j].color == 1)
                                {
                                    fd.Write("\u2655 ");
                                }
                                else
                                {
                                    fd.Write("\u265B ");
                                } 
                            }

                            if (dosk[i, j].figure == 5000)
                            {
                                if (dosk[i, j].color == 1)
                                {
                                    fd.Write("\u2656 ");
                                }
                                else
                                {
                                    fd.Write("\u265C ");
                                }
                            }

                            if (dosk[i, j].figure == 90000)
                            {
                                if (dosk[i, j].color == 1)
                                {
                                    fd.Write("\u2654 ");
                                }
                                else
                                {
                                    fd.Write("\u265A ");
                                }
                            }

                            if (dosk[i, j].figure < 1000)
                                fd.Write(". ");
                        }
                    }
                    fd.WriteLine();
                }
                fd.Write("  a b c d e f g h");
                fd.WriteLine();
            }
            fd.Dispose();
        }
    }
}
