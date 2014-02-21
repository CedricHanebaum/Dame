using System;
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
					field[i, 0] = new Stone(Token.PlayerColor.Black);
					field[i, 2] = new Stone(Token.PlayerColor.Black);
					field[i, field.GetLength(1) - 2] = new Stone(Token.PlayerColor.White);
					if (field.GetLength(1) == 10) field[i, field.GetLength(1) - 4] = new Stone(Token.PlayerColor.White);//4. line at a 10x10 field
				}
				if (i % 2 == 0)
				{
					field[i, 1] = new Stone(Token.PlayerColor.Black);
					field[i, field.GetLength(1) - 1] = new Stone(Token.PlayerColor.White);
					field[i, field.GetLength(1) - 3] = new Stone(Token.PlayerColor.White);
					if (field.GetLength(1) == 10) field[i, 3] = new Stone(Token.PlayerColor.Black);//4. line at a 10x10 field
				}
			}
		}

		public void addListener(IMap l)
		{
			listeners.Add(l);
		}

		public Token[,] Field{
		   get { return this.field; }
		}

		public void updateListeners()
		{
			for (int i = 0; i < listeners.Count; i++)
			{
				listeners.ElementAt(i).refresh();
			}
		}

		public bool isOnTheMap(int pos1, int pos2)
		{
			if (pos1 >= 0 && pos1 < this.Field.GetLength(1) && pos2 >= 0 && pos2 < this.Field.GetLength(1))
			{
				return true;
			}
			return false;
		}

		public Token getToken(int[] pos)
		{

			return field[pos[0], pos[1]];


			/*
			Token.PlayerColor c;
			try {
				c = Field[pos[0], pos[1]].Color;
			} catch (NullReferenceException e) {
				return null;
			}
			if (Field[pos[0], pos[1]].Tok == "stone") return new Stone(c);
			else return new Draught(c); */
		}
		
	
		public void RemoveToken(List<int[]> pos)
		{
			int[] temp;
			for (int i = 0; i < pos.Count; i++)
			{
				temp = pos.ElementAt(i);
				this.Field[temp[0],temp[1]] = null;
			}
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
	}
}
