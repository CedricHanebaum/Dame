using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Draught
{
    class Control
    {
        private Map m;
        private List <Players> pList = new List<Players>();
        private short index = 0;
        private Players act;
        private RandomAI AI = null;
        public enum Players { AIBlack, AIWhite, HumanBlack, HumanWhite };
        public Control(Map m, Players p1, Players p2)
        {
            this.m = m;
            if (!isHuman(p1) || !isHuman(p2))
            {
                AI = new RandomAI();
            }
            if (p1 == p2)
            {
                throw new ArgumentException("Zwei gleiche Spieler uebergeben");
            }
            pList.Add(p1);
            pList.Add(p2);
            act = pList.ElementAt(0);
        }

        private bool isHuman(Players p)
        {
            return (p == Players.HumanBlack || p == Players.HumanWhite);
        }

        private bool isBlack(Players p)
        {
            return (p == Players.AIBlack || p == Players.HumanBlack);
        }
        private Players changeIndex()
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
            if (!isHuman(pList.ElementAt(index)))
            {
                Token.PlayerColor col = Token.PlayerColor.Black;
                if(!isBlack(pList.ElementAt(index)))
                    col = Token.PlayerColor.White;
                int[] pos = AI.ChooseToken(m,col);
                int[] posN = AI.SetStep(m, col, pos);
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
