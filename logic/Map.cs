﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Draught
{
    class Map
    {
        private Token[,] field=new Token[0,0];
        private List<IMap> listeners = new List<IMap>();
        public Map(int n)
        {
            field = (n == 8) ? new Token[8, 8] : new Token[10, 10];//8x8 or 10x10 field is declaired
            for (int i = 0; i < field.GetLength(1); i++){//tokens are given their start positions
                if (i % 2 != 0)
                {
                    field[0, i] = new Stone(Token.PlayerColor.Black);
                    field[2, i] = new Stone(Token.PlayerColor.Black);
                    field[field.GetLength(1) - 2, i] = new Stone(Token.PlayerColor.White);
                    if (field.GetLength(1) == 10) field[field.GetLength(1) - 4, i] = new Stone(Token.PlayerColor.White);//4. line at a 10x10 field
                }
                if (i % 2 == 0)
                {
                    field[1, i] = new Stone(Token.PlayerColor.Black);
                    field[field.GetLength(1) - 1, i] = new Stone(Token.PlayerColor.White);
                    field[field.GetLength(1) - 3, i] = new Stone(Token.PlayerColor.White);
                    if (field.GetLength(1) == 10) field[3, i] = new Stone(Token.PlayerColor.Black);//4. line at a 10x10 field
                }
            }
        }

        public void addListener(IMap l)
        {
            listeners.Add(l);
        }

        public void updateListeners()
        {
            for (int i = 0; i < listeners.Count; i++)
            {
                listeners.ElementAt(i).refresh();
            }
        }

        public Token[,] Field
        {
           get { return this.field; }
        }
        public Token getToken(int[] pos)
        {
            Token.PlayerColor c = Field[pos[0], pos[1]].Color;
            if (Field[pos[0], pos[1]].Tok == "stone") return new Stone(c);
            else if (Field[pos[0], pos[1]] == null) return null;
            else return new Draught(c);
        }
        public void RemoveToken(int[] pos)
        {
           this.Field[pos[0], pos[1]] = null;
           updateListeners();
        }

        public void AddToken(int[] pos, Token t)
        {
            if(field[pos[0],pos[1]]==null)
                field[pos[0],pos[1]] = t;
            updateListeners();
        }

        public bool isOnTheMap(int[] pos)
        {
            if (pos[0] >= 0 && pos[0] < this.Field.GetLength(1) && pos[1] >= 0 && pos[1] < this.Field.GetLength(1))
            {
                return true;
            }
            return false;
        }

        public void MoveToken(int[]start,int[]end)
        {
            if (Field[start[0], start[1]] != null && Field[end[0], end[1]] == null)
            {
                Field[end[0], end[1]] = Field[start[0], start[1]];
                RemoveToken(new int[] { start[0], start[1] });
            }
            else
            {
                throw new NotSupportedException("Move is not possible");
            }
        }
    }
}
