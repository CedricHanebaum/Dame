using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Draught
{
    interface Player
    {
       int[] ChooseToken(Map m, Token.PlayerColor color);
       int[] SetStep(Map m, Token.PlayerColor color, int[] pos);

    }

}
