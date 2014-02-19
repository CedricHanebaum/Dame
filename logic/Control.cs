using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Draught
{
    class Control
    {
        Map m;
        public Control(Map m)
        {
            this.m = m;
        }

        public void checkTurn(int[] posO, int[] posN, Token t)
        {
            int[,] possible = t.nextStep(m,posO);
            if (possible[posN[0], posN[1]] == 1)
            {

                doTurn(posN, posO, t);
            }
            else if (possible[posN[0], posN[1]] == 0)
            {
                foreach (int a in possible)
                {
                    foreach (int b in possible)
                    {
                        if (possible[a, b] == 1)
                        {
                            throw new ArgumentException("Zug nicht moeglich, Schlagzwang du Arschloch!!!!");
                        }
                    }
                }
                doTurn(posN, posO, t);
            }
            else if (possible[posN[0], posN[1]] == -1)
                throw new ArgumentException("Zug nicht moeglich, besser gucken, Sie Arschloch!!!!");
        }

       
        public void doTurn(int[] posN, int[] posO, Token t)
        {
            int[] direct = new int[2];
            if (posN[0] < posO[0])
            {
                direct[0] = -1;
                if (posN[1] < posO[1]) //TopLeft
                    direct[1] = -1;
                else //TopRight
                    direct[1] = 1;
            }
            else
            {
                direct[0] = 1;
                if (posN[1] < posO[1]) //BottomLeft
                    direct[1] = -1;
                else //BottomRight
                    direct[1] = 1;
            }
            int diff = Math.Abs(posN[0]-posO[0])+1;
            for (int i = 1; i < diff; i++)
            {
                m.RemoveToken(new int[]{posO[0]+(i*direct[0]), posO[1]+(i*direct[1])});
            }
            m.MoveToken(posO, posN);
            if (posN[0] == 0 && t.Tok=="stone")
            {
                Draught d = new Draught(t.Color);
                m.RemoveToken(posN);
                m.AddToken(posN, d);
            }

        }
    }
}
