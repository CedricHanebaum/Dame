using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Draught
{
    class Human :Player
    {
        public int[] ChooseToken(Map m, Token.PlayerColor color)
        {
            return new int[]{-1,-1}; //TODO aus der GUI die gewählte Figur des Spielers holen
        }
        public int[] SetStep(Map m, Token.PlayerColor color)
        {
            int[] pos = ChooseToken(m, color);
            int[,] help;
            if (m.Field[pos[0], pos[1]].Tok == "stone"){
                Token.PlayerColor c = m.Field[pos[0], pos[1]].Color;
                help = new Stone(c).nextStep(m, pos);//field of information where you can move, or have to move a stone
            }
            else{
                Token.PlayerColor c = m.Field[pos[0], pos[1]].Color;
                help = new Draught(c).nextStep(m, pos);//field of information where you can move, or have to move a draught
            }
            return new int[]{-1,-1};
        }
    }
}
