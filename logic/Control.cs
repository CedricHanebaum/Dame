using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Draught
{
    class Control
    {
        private Map m;
        private List<WindowsFormsApplication1.Loop.Players> pList = new List<WindowsFormsApplication1.Loop.Players>();
        private short index = 0;
        private WindowsFormsApplication1.Loop.Players act;
        private RandomAI AI = null;
        public Control(Map m, WindowsFormsApplication1.Loop.Players p1, WindowsFormsApplication1.Loop.Players p2)
        {
            this.m = m;
            if (p1 == WindowsFormsApplication1.Loop.Players.AI || p2 == WindowsFormsApplication1.Loop.Players.AI)
            {
                AI = new RandomAI();
                pList.Add(WindowsFormsApplication1.Loop.Players.AI);
                if (p1 == WindowsFormsApplication1.Loop.Players.AI && p2 == WindowsFormsApplication1.Loop.Players.AI)
                    pList.Add(WindowsFormsApplication1.Loop.Players.AI);
                else
                    pList.Add(WindowsFormsApplication1.Loop.Players.Human);
            }
            else
            {
                pList.Add(WindowsFormsApplication1.Loop.Players.Human);
                pList.Add(WindowsFormsApplication1.Loop.Players.Human);
            }
            act = pList.ElementAt(0);
        }

        private WindowsFormsApplication1.Loop.Players changeIndex()
        {
            short value = (short)(index + 1);
            if (value >= pList.Count())
                index = 0;
            else
                index++;
            return pList.ElementAt(index);
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

        public void AINext()
        {
            if (pList.ElementAt(index) == WindowsFormsApplication1.Loop.Players.AI)
            {
                int[] pos = AI.ChooseToken(m,Token.PlayerColor.Black);
                int[] posN = AI.SetStep(m, Token.PlayerColor.Black, pos);
                checkTurn(pos, posN, m.getToken(pos));
            }
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
            int[,] possNext = t.nextStep(m, posN);
            if (posN[0] == 0 && t.Tok=="stone")
            {
                Draught d = new Draught(t.Color);
                m.RemoveToken(posN);
                m.AddToken(posN, d);
                possNext = d.nextStep(m, posN);
            }
            foreach (int a in possNext)
            {
                foreach (int b in possNext)
                {
                    //Wenn weitere Schlag moeglich, dann kein Spielerwechsel, warten s.U
                    if (possNext[a, b] == 1)
                    {
                        return;
                    }
                }
            }
            act = changeIndex();
            // BEI AI WARTE AUF AUFRUF VON AI_NEXT(), sonst warte auf Aufruf von checkTurn bei Klick von Benutzer
        }
    }
}
