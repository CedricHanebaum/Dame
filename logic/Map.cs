using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Draught
{
    class Map
    {
        private Token[,] field=new Token[0,0];

        public Map(int n)
        {
            field = (n == 8) ? new Token[8, 8] : new Token[10, 10];//8x8 or 10x10 field is declaired

            for (int i = 0; i < field.Length; i++){//tokens are given their start positions
                if (i % 2 != 0){
                    field[0, i] = new Stone(Token.PlayerColor.Black);
                    field[2, i] = new Stone(Token.PlayerColor.Black);
                    field[field.Length - 2, i] = new Stone(Token.PlayerColor.White);
                    if (field.Length == 10) field[field.Length - 4, i] = new Stone(Token.PlayerColor.White);//4. line at a 10x10 field
                }
                if (i % 2 == 0){
                    field[1, i] = new Stone(Token.PlayerColor.Black);
                    field[field.Length - 1, i] = new Stone(Token.PlayerColor.White);
                    field[field.Length - 3, i] = new Stone(Token.PlayerColor.White);
                    if (field.Length == 10) field[3, i] = new Stone(Token.PlayerColor.Black);//4. line at a 10x10 field
                }
            }

        }

        public Token[,] Field{
           get { return this.field; }
        }
        public Token getToken(int[] pos)
        {
            Token.PlayerColor c = Field[pos[0], pos[1]].Color;
            if(Field[pos[0], pos[1]].Tok=="Stone") return new Stone(c);
            else return new Draught(c);
        }
    }
}
