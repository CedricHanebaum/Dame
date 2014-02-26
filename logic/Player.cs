using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Draught
{
    interface Player
    {
       int[] ChooseToken(Map m, Token.PlayerColor color, List<int[]> taken);
       int[] SetStep(Map m, Token.PlayerColor color, int[] pos, int check, Control contrl, List<int[]> taken);

    }

}
