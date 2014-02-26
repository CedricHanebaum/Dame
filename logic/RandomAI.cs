using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Draught
{
    class RandomAI : Player
    {

        //returns a more or less random token which is able to move to at least one field
        public int isInList(int[] test, List<int[]> list, List<int[]> list2)
        {
            int[] temp, temp2;
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < list2.Count; j++)
                {
                    temp = list.ElementAt(i);
                    temp2 = list2.ElementAt(j);
                    if (temp[0] == test[0] && temp[1] == test[1] && temp2[0] == test[0] && temp2[1] == test[1])
                    {
                        return j;
                    }
                }
            }
            return -1;
        }
        public int[] ChooseToken(Map m, Token.PlayerColor color, List<int[]> taken)
        {
            List<int[]> tokens = new List<int[]>();
            List<int[]> priorityTokens = new List<int[]>();
            List<int[]> prioOutOfway = new List<int[]>();
            Random r = new Random();
            int[,] compare = new int[m.Field.GetLength(1), m.Field.GetLength(1)];
            for (int i = 0; i < m.Field.GetLength(1); i++){//build a list off all tokens with a specific color
                for (int j = 0; j < m.Field.GetLength(1); j++)
                {
                    if (m.Field[i, j] != null && m.Field[i, j].Color == color){
                        int[,] tmp = m.getToken(new int[] { i, j }).nextStep(m, new int[] { i, j });
                        bool moveable = false;
                        for (int a = 0; a < tmp.GetLength(1); a++){
                            for (int b = 0; b < tmp.GetLength(1); b++){
                                if (tmp[a, b] != -1)
                                {//if any turns are possible
                                    moveable = true;
                                }
                            }
                        }
                        if (moveable == true)
                        {
                            tokens.Add(new int[] { i, j });
                        }
                    }
                }
            }
            int count = 0;
            List<int> deleteList = new List<int>();
            for (int n = 0; n < tokens.Count; ++n)
            { //checks if there are any priority turns
                int[,] help = m.Field[tokens[n][0], tokens[n][1]].nextStep(m, new int[] { tokens[n][0], tokens[n][1] });
                for (int a = 0; a < help.GetLength(1); ++a)
                {
                    for (int b = 0; b < help.GetLength(1); ++b)
                    {
                        int iil = isInList(new int[] { tokens[n][0], tokens[n][1] }, taken, tokens);
                        if (help[a, b] == 1)
                        {
                            count++;
                            priorityTokens.Add(new int[] { tokens[n][0], tokens[n][1] });
                        }
                        else if (iil > -1 && tokens.Count > 1)
                            deleteList.Add(iil);
                    }
                }
            }
            if (count == 0)
            {//there are no priority turns
                for (int i = 0; i < tokens.Count; ++i)//if AI can get a Draugt it should get it
                {
                    if (deleteList.Contains(i))
                    {
                        continue;
                    }
                    if (color == Token.PlayerColor.Black &&tokens[i][1] == m.Field.GetLength(1)-2&&m.Field[tokens[i][0],tokens[i][1]].Tok=="stone")
                        return new int[] { tokens[i][0], tokens[i][1] };
                    if (color == Token.PlayerColor.White && tokens[i][1] == 1 && m.Field[tokens[i][0], tokens[i][1]].Tok == "stone")
                        return new int[] { tokens[i][0], tokens[i][1] };
                }
                int r1 = r.Next(0, tokens.Count);
                return new int[] { tokens[r1][0], tokens[r1][1] };
            }
            else
            {//there are priority turns
                for (int i = 0; i < priorityTokens.Count; ++i)
                {
                    if (color == Token.PlayerColor.Black && priorityTokens[i][1] == m.Field.GetLength(1) - 3 && m.Field[tokens[i][0], tokens[i][1]].Tok == "stone")
                        return new int[] { priorityTokens[i][0], priorityTokens[i][1] };
                    else if (color == Token.PlayerColor.White && priorityTokens[i][1] == 2 && m.Field[tokens[i][0], tokens[i][1]].Tok == "stone")
                        return new int[] { priorityTokens[i][0], priorityTokens[i][1] };
                }
                int r1 = r.Next(0, priorityTokens.Count);
                return new int[] { priorityTokens[r1][0], priorityTokens[r1][1] };
            }
        }

        public bool removeTokens(Token[,] field, Token.PlayerColor color, int [] pos, Map m)
	    {
        	Token temp = null;
            int[] posN;
			for (int j = 0; j < field.GetLength(0); j++)
            {
	            for (int k = 0; k < field.GetLength(1); k++)
	            {
		            if (field[j, k] == null)
			            continue;
		            else if (field[j, k].Color != color)
		            {
			            temp = field[j, k];
			            posN = new int[]{ j, k };
			            int[,] possible = temp.nextStep(m, posN);
                        for (int l = 0; l < possible.GetLength(0); l++)
                        {
                            for (int n = 0; n < possible.GetLength(1); n++)
                            {
                                if (possible[l, n] > -1)
                                {
                                    if (isInDistance(posN, new int[] { l, n }, pos))
                                    {
                                        return true;
                                    }
                                }
                            }
                        }
		            }
	            }
            }
            return false;
        }

        public bool isInDistance(int[] posO, int[] posN, int[] posT)
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
            int diff = Math.Abs(posN[0] - posO[0]) + 1;
            // Gehe mit Hilfe der Richtung den diagonalen Weg zum Zielfeld und entferne ggf. dort stehende Steine
            List<int[]> removeList = new List<int[]>();
            int[] temp;
            for (int i = 1; i < diff; i++)
            {
                temp = new int[] { posO[0] + (i * direct[0]), posO[1] + (i * direct[1]) };
                if (temp[0]==posT[0]&&temp[1]==posT[1])
                {
                    return true;
                }
            }
            return false;
        }

        //returns the field the Player wants to visit
        public int[] SetStep(Map m, Token.PlayerColor color, int[] pos, int check, Control contrl, List<int[]> taken)
        {
            int[,] help;
            Random r = new Random();
            int r1;
            List<int[]> priority = new List<int[]>();
            List<int[]> visitable = new List<int[]>();

            if (m.Field[pos[0], pos[1]].Tok == "stone")
            {
                Token.PlayerColor c = m.Field[pos[0], pos[1]].Color;
                help = new Stone(c).nextStep(m, pos);//field of information where you can move, or have to move a stone
            }
            else
            {
                Token.PlayerColor c = m.Field[pos[0], pos[1]].Color;
                help = new Draught(c).nextStep(m, pos);//field of information where you can move, or have to move a draught
            }

            for (int a = 0; a < help.GetLength(1); ++a)
            {
                for (int b = 0; b < help.GetLength(1); ++b)
                {
                    if (help[a, b] == 1) priority.Add(new int[] { a, b }); //builds a list of field with priority
                    if (help[a, b] == 0) visitable.Add(new int[] { a, b });//builds a list of field which are visitable
                }
            }
            int[] temp;
            Token[,] field = m.Field;
            if (priority.Count != 0)
            {//if there are fields with higher priority
                for(int i=0; i<priority.Count; i++)
                {
                    temp = new int[] { priority[i][0], priority[i][1] };
                    contrl.temp = new int[] { pos[0], pos[1], temp[0], temp[1] };
                    return temp;
                }
            }
            else
            {
                for(int i=0; i<visitable.Count; i++)
                {
                    temp = new int[] { visitable[i][0], visitable[i][1] };
                    if (!removeTokens(field, color, temp, m))
                    {
                        contrl.temp = new int[] { pos[0], pos[1], temp[0], temp[1] };
                        return temp;
                    }
                }
            }
            if (check < 80)
            {
                taken.Add(pos);
                return SetStep(m, color, ChooseToken(m, color, taken), check + 1, contrl, taken);
            }
            else
            {
                if (priority.Count != 0)
                {//if there are fields with higher priority
                    r1 = r.Next(0, priority.Count);
                    temp = new int[] { priority[r1][0], priority[r1][1] };//choose a random field
                }
                else
                {
                    r1 = r.Next(0, visitable.Count);
                    temp = new int[] { visitable[r1][0], visitable[r1][1] };//choose a random field
                }
                contrl.temp = new int[] { pos[0], pos[1], temp[0], temp[1] };
                return temp;
            }
        }

    }
}

