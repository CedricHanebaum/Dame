using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace graphic.GUI
{
    class Options:Gui
    {
        public Options(GuiManager guimanager): base(guimanager)
        {
            GuiButton startB = new GuiButton(0,this);
            GuiButton endB = new GuiButton(1, this);

            GuiLabel player1L = new GuiLabel(0,this);
            GuiLabel player2L = new GuiLabel(1, this);
            GuiLabel sizeL = new GuiLabel(2,this);

            GuiRadioButtonGroup player1RG = new GuiRadioButtonGroup(0,this);
            GuiRadioButton human1R = new GuiRadioButton();
            GuiRadioButton ki1R = new GuiRadioButton();
            player1RG.Add(human1R);
            player1RG.Add(ki1R);


            GuiRadioButtonGroup player2RG = new GuiRadioButtonGroup(1,this);
            GuiRadioButton human2R = new GuiRadioButton();
            GuiRadioButton ki2R = new GuiRadioButton();
            player2RG.Add(human2R);
            player2RG.Add(ki2R);

            GuiRadioButtonGroup sizeRG = new GuiRadioButtonGroup(3, this);
            GuiRadioButton size8R = new GuiRadioButton();
            GuiRadioButton size10R = new GuiRadioButton();


            startB.setBounds(new Rectangle(100,0,200,100));
            startB.setText("Start Game");

            endB.setBounds(new Rectangle(100,500,200,100));
            endB.setText("End Game");


            player1L.setBounds(new Rectangle(100, 100, 200, 100));
            player1L.setText("Player 1");


            player2L.setBounds(new Rectangle(400, 100, 200, 100));
            player2L.setText("Player 2");


            sizeL.setBounds(new Rectangle(250, 200, 200, 100));
            sizeL.setText("Size of the field");

            human1R.setBounds(new Rectangle(100,120,100,20));
            ki1R.setBounds(new Rectangle(100, 140, 100, 20));

            human2R.setBounds(new Rectangle(300, 120, 100, 20));
            ki2R.setBounds(new Rectangle(300, 140, 100, 20));

            size8R.setBounds(new Rectangle(100, 160, 100, 20));
            size10R.setBounds(new Rectangle(300, 160, 100, 20));
        }
    }
}
