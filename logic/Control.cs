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
            if (p1 == p2 || (isBlack(p1) && isBlack(p2)) || (!isBlack(p1) && !isBlack(p2)))
            {
                throw new ArgumentException("Zwei gleiche Spieler uebergeben, oder keine unterschiedliche Farbe!");
            }
            pList.Add(p1);
            pList.Add(p2);
            act = pList.ElementAt(0);
        }

        // Methode zum Pruefen, ob Spieler Ai oder Human ist
        private bool isHuman(Players p)
        {
            return (p == Players.HumanBlack || p == Players.HumanWhite);
        }

        // Methode zum Pruefen, ob Spieler schwarze oder weisse Steine hat
        private bool isBlack(Players p)
        {
            return (p == Players.AIBlack || p == Players.HumanBlack);
        }

        //Methode zum Pruefen, ob ein Spieler die Moeglichkeit hat, weitere Zuege zu taetigen
        private bool hasTurns(Players p)
        {
            Token.PlayerColor col = Token.PlayerColor.Black;
            if (!isBlack(p))
                col = Token.PlayerColor.White;
            Token[,] field = m.Field;
            Token temp = null;
            int[] posN;
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); i++)
                {
                    if (field[i, j].Color == col)
                    {
                        temp = field[i, j];
                        posN = new int[]{ i, j };
                        int[,] possible = temp.nextStep(m, posN);
                        if (possible[posN[0], posN[1]] > -1)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        // Methode prueft, ob ein Spieler noch Steine auf dem Feld hat
        private bool hasStones(Players p)
        {
            Token.PlayerColor col = Token.PlayerColor.Black;
            if(!isBlack(p))
                col = Token.PlayerColor.White;
            Token[,] field = m.Field;
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); i++)
                {
                    if (field[i, j].Color == col)
                        return true;
                }
            }
            return false;
        }

        // Dient zum Durchlaufen der Spielerliste im Rahmen der Spielverwaltung, gibt den Spieler zurueck, der als naechstes an der Reihe ist
        public Players changeIndex()
        {
            short value = (short)(index + 1);
            if (value >= pList.Count())
                index = 0;
            else
                index++;
            return pList.ElementAt(index);
        }

        // Prueft, ob ein uebergebener Zug moeglich ist, und laesst diesen ggf. ausfuehren
        public void checkTurn(int[] posO, int[] posN)
        {
            // Hole alle Moeglichkeiten
            Token t = m.getToken(posO);
            int[,] possible = t.nextStep(m,posO);
            //Pruefe ob ein Schritt ausgefuehrt werden soll, bei dem Schlagzwang besteht
            if (possible[posN[0], posN[1]] == 1)
            {
                doTurn(posN, posO, t);
            }
            // Falls ein anderer zug ausgefuehrt wird pruefe, ob nicht doch Schlagzwang besteht
            else if (possible[posN[0], posN[1]] == 0)
            {
                for (int i=0; i<possible.GetLength(0); i++)
                {
                    for (int j = 0; j < possible.GetLength(1); j++)
                    {
                        if (possible[i,j] == 1)
                        {
                            throw new ArgumentException("Zug nicht moeglich, Schlagzwang du Arschloch!!!!");
                        }
                    }
                }
                // Wenn Alles okay, dann lasse den Zug ausfuehren
                doTurn(posN, posO, t);
            }
            // Zug nicht moeglich, Exception wird geschmissen
            else if (possible[posN[0], posN[1]] == -1)
                throw new ArgumentException("Zug nicht moeglich, besser gucken, Sie Arschloch!!!!");
        }

        // Methode wird von aussen aufgerufen um naechsten Zug auszufuehren, wenn AI an der Reihe ist.
        public void AINext()
        {
            if (!isHuman(pList.ElementAt(index)))
            {
                Token.PlayerColor col = Token.PlayerColor.Black;
                if(!isBlack(pList.ElementAt(index)))
                    col = Token.PlayerColor.White;
                int[] pos = AI.ChooseToken(m,col);
                int[] posN = AI.SetStep(m, col, pos);
                checkTurn(pos, posN);
            }
        }
        // Methode zum ausfuehren genehmigter Spielzuege
        public void doTurn(int[] posN, int[] posO, Token t)
        {
            // Pruefe zunaechst, in welche Richtung die Figur bewegt werden soll
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
            // Gehe mit Hilfe der Richtung den diagonalen Weg zum Zielfeld und entferne ggf. dort stehende Steine
            for (int i = 1; i < diff; i++)
            {
                m.RemoveToken(new int[]{posO[0]+(i*direct[0]), posO[1]+(i*direct[1])});
            }
            // Fuehre die eigentliche Bewegung in der Map aus
            m.MoveToken(posO, posN);
            // Importiere die moeglichen naechsten Schritte zur Ueberpruefung, ob das Spiel fortgesetzt werden kann
            int[,] possNext = t.nextStep(m, posN);
            // Wenn letzte Reihe, dann wird Stein zur Dame
            if (((!isBlack(act) && posN[0] == 0) || (isBlack(act) && posN[0] == m.Field.GetLength(0))) && t.Tok=="stone")
            {
                Draught d = new Draught(t.Color);
                m.RemoveToken(posN);
                m.AddToken(posN, d);
                //Naechste Schritte der Dame sind andere als eines Steins
                possNext = d.nextStep(m, posN);
            }
            for (int i = 0; i < possNext.GetLength(0); i++)
            {
                for (int j = 0; j < possNext.GetLength(1); j++)
                {
                    if (possNext[i, j] == 1)
                    {
                        return;
                    }
                }
            }
            // Weiter zum naechsten Spieler
            act = changeIndex();
            // Wenn naechster Spieler keine Figuren oder moegliche zuege mehr hat, dann ist das Spiel beendet.
            if (!hasStones(act) || !hasTurns(act))
            {
                String tmp = "Spiel beendet, Spieler ";
                if(act == pList.ElementAt(0))
                    tmp += "2 gewinnt!";
                else
                    tmp += "1 gewinnt!";
                throw new ExecutionEngineException(tmp);
            }
            // BEI AI WARTE AUF AUFRUF VON AI_NEXT(), sonst warte auf Aufruf von checkTurn bei Klick von Benutzer
        }
    }
}
