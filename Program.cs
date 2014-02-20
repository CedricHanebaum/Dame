using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;

namespace WindowsFormsApplication1 
{
    static class Program 
    {
        static void Main(string[] args) 
        {
            Draught.Control contrl = new Draught.Control(new Draught.Map(10), Draught.Control.Players.HumanWhite, Draught.Control.Players.AIBlack);
            Draught.Control.Players temp = Draught.Control.Players.HumanBlack;
            for (int i = 0; i < 10; i++)
            {
                temp = contrl.changeIndex();
            }
        }
    }
}
