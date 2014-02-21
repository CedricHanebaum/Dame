using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace graphic.GUI
{
    class Options:Gui
    {

        private GuiRadioButtonGroup sizeRG;
        private GuiRadioButtonGroup player2RG;
        private GuiRadioButtonGroup player1RG;

        public Options(GuiManager guimanager): base(guimanager)
        {
            this.setBounds(new Rectangle(0, 0, 1024, 768));
            GuiButton startB = new GuiButton(6,this);
            GuiButton endB = new GuiButton(7, this);

            GuiLabel player1L = new GuiLabel(-1,this);
            GuiLabel player2L = new GuiLabel(-1, this);
            GuiLabel sizeL = new GuiLabel(-1,this);

            player1RG = new GuiRadioButtonGroup();
            GuiRadioButton human1R = new GuiRadioButton(0, this);
            GuiRadioButton ki1R = new GuiRadioButton(1, this);
            player1RG.addRadioButton(human1R);
            player1RG.addRadioButton(ki1R);
            
            player2RG = new GuiRadioButtonGroup();
            GuiRadioButton human2R = new GuiRadioButton(2, this);
            GuiRadioButton ki2R = new GuiRadioButton(3, this);
            player2RG.addRadioButton(human2R);
            player2RG.addRadioButton(ki2R);

            sizeRG = new GuiRadioButtonGroup();
            GuiRadioButton size8R = new GuiRadioButton(4, this);
            GuiRadioButton size10R = new GuiRadioButton(5, this);
            sizeRG.addRadioButton(size8R);
            sizeRG.addRadioButton(size10R);


            startB.setBounds(new Rectangle(100,0,300,100));
            startB.setImage(new Bitmap("../go.bmp"));
            startB.setText("Start Game");
            this.addComponent(startB);

            endB.setBounds(new Rectangle(100,500,300,100));
            endB.setImage(new Bitmap("../end.bmp"));
            endB.setText("End Game");
            this.addComponent(endB);

            player1L.setBounds(new Rectangle(100, 110, 200, 100));
            player1L.setColor(Color.Red);
            player1L.setText("Player 1");
            this.addComponent(player1L);

            player2L.setBounds(new Rectangle(300, 110, 200, 100));
            player2L.setColor(Color.Blue);
            player2L.setText("Player 2 ");
            this.addComponent(player2L);

            sizeL.setBounds(new Rectangle(180, 240, 200, 100));
            sizeL.setText("Size of the field");
            this.addComponent(sizeL);

            human1R.setBounds(new Rectangle(100,140,100,20));
            human1R.setText("Human Player");
            ki1R.setBounds(new Rectangle(100, 160, 100, 20));
            ki1R.setText("Computer");
            this.addComponent(human1R);
            this.addComponent(ki1R);


            human2R.setBounds(new Rectangle(300, 140, 100, 20));
            human2R.setText("Human Player");
            ki2R.setBounds(new Rectangle(300, 160, 100, 20));
            ki2R.setText("Computer");
            this.addComponent(human2R);
            this.addComponent(ki2R);


            size8R.setBounds(new Rectangle(100, 260, 100, 20));
            size8R.setText("8x8");
            size10R.setBounds(new Rectangle(300, 260, 100, 20));
            size10R.setText("10x10");
            this.addComponent(size8R);
            this.addComponent(size10R);

            player1RG.activateButton(human1R.getID());
            player2RG.activateButton(ki2R.getID());
            sizeRG.activateButton(size8R.getID());
        }

        public override void actionPerformed(int id)
        {
            switch (id)
            {
                case 6:
                    int size = sizeRG.getActiveButtonId() == 4 ? 8 : 10;
                    Draught.Control.Players p1 = player1RG.getActiveButtonId() == 0 ? Draught.Control.Players.HumanWhite : Draught.Control.Players.AIWhite;
                    Draught.Control.Players p2 = player2RG.getActiveButtonId() == 2 ? Draught.Control.Players.HumanBlack : Draught.Control.Players.AIBlack;


                    guiManager.startGame(size, p1, p2);
                    break;
                case 7:
                    break;
            }
        }
    }
}
