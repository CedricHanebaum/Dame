using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Draught
{
    abstract class Token
    {
        protected string tok;
        private PlayerColor color;
        public Token() { }
        public Token(PlayerColor pc){
            color = pc;
            
        }
        public PlayerColor Color{
            get { return this.color;}
        }
        public PlayerColor GetOpponentColor()
        {
            if (this.Color == PlayerColor.White) return PlayerColor.Black;
            else return PlayerColor.White;
        }
        public string Tok
        {
            get { return this.tok; }
        }
        public abstract int[,] nextStep(Map field, int[] position); //returns a list of possible next fields to visit
        public enum PlayerColor { Black, White,Empty }; //enum for black and white tokens
    }
}
