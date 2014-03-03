﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ISO;
using tmp;

namespace Draught
{
	class Control : ITickable
	{
		private Map m;
        private List<Intelligence> pList = new List<Intelligence>();
		private short index = 0;
        private Intelligence act;
		private RandomAI AI = null;
		private Loop l;
		private long delta = 0;
		public int[] temp = null;
        private int[] posAI;
		private bool wait = true;
		private bool msg = false;
        public Control(Map m, Intelligence p1, Intelligence p2, Loop l)
		{
			this.m = m;
			this.l = l;
			l.addToUpdateList(this);
			if (!isHuman(p1) || !isHuman(p2))
			{
				AI = new RandomAI(); 
			}
            p1.Color = Intelligence.eColor.black; ;
			pList.Add(p1);
            p2.Color = Intelligence.eColor.white;
			pList.Add(p2);
			act = pList.ElementAt(0);
			AINext(null);
		}

		public void update(long delta)
		{
			this.delta += delta;
			if (wait)
			{
				this.delta = 0;
				wait = false;
			}
			if (temp != null)
			{
				if (this.delta >= 1000)
				{
                    Token.PlayerColor col = Token.PlayerColor.Black;
                    if (!isBlack(pList.ElementAt(index)))
                        col = Token.PlayerColor.White;
                    if (posAI == null) posAI = AI.ChooseToken(m, col, new List<int[]>());
                    int[] posN = AI.SetStep(m, col, posAI, 0, this, new List<int[]>());
					checkTurn(new int[] { temp[0], temp[1] }, new int[] { temp[2], temp[3] }, true);
				}
			}
			if (msg)
			{
				if (this.delta >= 1200)
				{
					msg = false;
					if(!hasTurns(pList.ElementAt(0),false)&&!hasTurns(pList.ElementAt(1),false))
					{
						errorMessage("Das Spiel endet unentschieden!", true);
						return;
					}
					String tmp = "Spiel beendet, Spieler ";
					if (act == pList.ElementAt(0))
						tmp += "2 gewinnt!";
					else
						tmp += "1 gewinnt!";
					errorMessage(tmp, true);
				}
			}
		}

		// Methode zum Pruefen, ob Spieler Ai oder Human ist
        private bool isHuman(Intelligence p)
		{
			return p.Type == Intelligence.eType.human;
		}

		// Methode zum Pruefen, ob Spieler schwarze oder weisse Steine hat
        private bool isBlack(Intelligence p)
		{
			return this.getColor(p) == Token.PlayerColor.Black;
		}

        private Token.PlayerColor getColor(Intelligence p)
		{
			if (p.Color == Intelligence.eColor.black)
				return Token.PlayerColor.Black;
			else
				return Token.PlayerColor.White;
		}

		//Methode zum Pruefen, ob ein Spieler die Moeglichkeit hat, weitere Zuege zu taetigen
        private bool hasTurns(Intelligence p, bool schlagzwang)
		{
			Token.PlayerColor col = Token.PlayerColor.Black;
			if (!isBlack(p))
				col = Token.PlayerColor.White;
			Token[,] field = m.Field;
			Token temp = null;
			int[] posN;
			for (int i = 0; i < field.GetLength(0); i++)
			{
				for (int j = 0; j < field.GetLength(1); j++)
				{
					if (field[i, j] == null)
						continue;
					else if (field[i, j].Color == col)
					{
						temp = field[i, j];
						posN = new int[]{ i, j };
						int[,] possible = temp.nextStep(m, posN);
						for (int k = 0; k < possible.GetLength(0); k++)
						{
							for (int z = 0; z < possible.GetLength(1); z++)
							{
								if (possible[k, z] > -1 && !schlagzwang)
									return true;
								else if (schlagzwang && possible[k, z] == 1)
									return true;
							}
						}
					}
				}
			}
			return false;
		}

		// Methode prueft, ob ein Spieler noch Steine auf dem Feld hat
        private bool hasStones(Intelligence p)
		{
			Token.PlayerColor col = Token.PlayerColor.Black;
			if(!isBlack(p))
				col = Token.PlayerColor.White;
			Token[,] field = m.Field;
			for (int i = 0; i < field.GetLength(0); i++)
			{
				for (int j = 0; j < field.GetLength(1); j++)
				{
					if (field[i, j] == null)
						continue;
					else if (field[i, j].Color == col)
						return true;
				}
			}
			return false;
		}

		// Dient zum Durchlaufen der Spielerliste im Rahmen der Spielverwaltung, gibt den Spieler zurueck, der als naechstes an der Reihe ist
        public Intelligence changeIndex()
		{
			short value = (short)(index + 1);
			if (value >= pList.Count())
				index = 0;
			else
				index++;
			return pList.ElementAt(index);
		}

		// Prueft, ob ein uebergebener Zug moeglich ist, und laesst diesen ggf. ausfuehren
		public void checkTurn(int[] posO, int[] posN, bool automatic)
		{
			if (!isHuman(act) && !automatic)
			{
				errorMessage("Die KI ist am Zug!", false);
				return;
			}
			else if (automatic)
				temp = null;
			// Hole alle Moeglichkeiten
			Token t = m.getToken(posO);
			if (t == null || t.Color != getColor(act))
			{
				errorMessage("Ungueltigen Stein ausgewaehlt!", false);
				return;
			}
			int[,] possible = t.nextStep(m,posO);
			//Pruefe ob ein Schritt ausgefuehrt werden soll, bei dem Schlagzwang besteht
			if (possible[posN[0], posN[1]] == 1)
			{
				doTurn(posN, posO, t, true);
			}
			// Falls ein anderer Zug ausgefuehrt wird pruefe, ob nicht doch Schlagzwang besteht
			else if (possible[posN[0], posN[1]] == 0)
			{
					if (hasTurns(act,true))
					{
						errorMessage("Zug nicht moeglich, Schlagzwang beachten!", false);
						return;
					}
					// Wenn Alles okay, dann lasse den Zug ausfuehren
					doTurn(posN, posO, t, false);
			}
			// Zug nicht moeglich, Exception wird geschmissen
			else if (possible[posN[0], posN[1]] == -1)
				errorMessage("Ungueltiges Zielfeld!", false);
		}

		// Methode wird von aussen aufgerufen um naechsten Zug auszufuehren, wenn AI an der Reihe ist.
		public void AINext(int[] pos)
		{
			if (!isHuman(pList.ElementAt(index)))
			{
                errorMessage("Die KI berechnet ihren nächsten Zug..", false);
                posAI = pos;
				wait = true;
                if(temp==null) temp = new int[]{1};
			}
		}

		public void errorMessage(String message, bool exit)
		{
            if (exit)
            {
               string caption = "GEWONNEN!";
               System.Windows.Forms.MessageBoxButtons buttons = System.Windows.Forms.MessageBoxButtons.OK;
               System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show(message, caption, buttons);
                // HIER: SPIELENDE, ZURUECK ZUM HAUPTMENUE
               l.showMainMenu();
            } else
            l.getGuiManager().setInGameLableText(message);
		}

		// Methode zum ausfuehren genehmigter Spielzuege
		public void doTurn(int[] posN, int[] posO, Token t, bool beaten)
		{
            l.getGuiManager().setInGameLableText("");
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
			List<int[]> removeList = new List<int[]>();
			for (int i = 1; i < diff; i++)
			{
				removeList.Add(new int[]{posO[0]+(i*direct[0]), posO[1]+(i*direct[1])});
			}
			// Fuehre die eigentliche Bewegung in der Map aus
			removeList.Add(posO);
			// Importiere die moeglichen naechsten Schritte zur Ueberpruefung, ob das Spiel fortgesetzt werden kann
			// Wenn letzte Reihe, dann wird Stein zur Dame
			bool draught = false;
			if (((!isBlack(act) && posN[1] == 0) || (isBlack(act) && posN[1] == m.Field.GetLength(0)-1)) && t.Tok=="stone")
			{
				draught = true;
				Draught d = new Draught(t.Color);
				removeList.Add(posN);
				m.AddToken(posN, d);
				t = d;
				//Naechste Schritte der Dame sind andere als eines Steins
			}
			m.RemoveToken(removeList);
			m.AddToken(posN, t);
			int[,] possNext = t.nextStep(m, posN);
			if (beaten) temp = null;
			if (beaten&&!draught)
			{
				for (int i = 0; i < possNext.GetLength(0); i++)
				{
					for (int j = 0; j < possNext.GetLength(1); j++)
					{
						if (possNext[i, j] == 1)
						{
							if (!isHuman(act))
							{
								AINext(posN);
							}
							return;
						}
					}
				}
			}
			// Weiter zum naechsten Spieler
			act = changeIndex();
			// Wenn naechster Spieler keine Figuren oder moegliche zuege mehr hat, dann ist das Spiel beendet.
			if (!hasStones(act)||!hasTurns(act,false))
			{
				msg = true;
				wait = true;
				return;
			}
            if (!isHuman(act))
                AINext(null);
            else
                errorMessage("Sie sind am Zug!", false);
			// BEI AI WARTE AUF AUFRUF VON AI_NEXT(), sonst warte auf Aufruf von checkTurn bei Klick von Benutzer
		}
	}
}
