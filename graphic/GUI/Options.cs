using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace graphic.GUI {
    class Options: Gui {

        private GuiRadioButtonGroup sizeRG;
        private GuiRadioButtonGroup player2RG;
        private GuiRadioButtonGroup player1RG;

        public Options(GuiManager guimanager): base(guimanager) {
            this.setBounds(new Rectangle(0, 0, 1024, 768));
            this.setBackground(new Bitmap("../bg.bmp"));

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


            startB.setBounds(new Rectangle(100,5,300,100));
            startB.setImage(new Bitmap("../go.bmp"));
            startB.setText("Start Game");
            this.addComponent(startB);

            endB.setBounds(new Rectangle(100,300,300,100));
            endB.setImage(new Bitmap("../end.bmp"));
            endB.setText("End Game");
            this.addComponent(endB);

            player1L.setBounds(new Rectangle(100, 110, 200, 100));
            player1L.setColor(Color.Blue);
            player1L.setText("P l a y e r   1");
            this.addComponent(player1L);

            player2L.setBounds(new Rectangle(300, 110, 200, 100));
            player2L.setColor(Color.Red);
            player2L.setText("P l a y e r   2 ");
            this.addComponent(player2L);

            sizeL.setBounds(new Rectangle(140, 240, 200, 100));
            sizeL.setText("S i z e   o f   t h e   f i e l d");
            this.addComponent(sizeL);

            human1R.setBounds(new Rectangle(100,150,100,20));
            human1R.setText("H u m a n   P l a y e r");
            ki1R.setBounds(new Rectangle(100, 180, 100, 20));
            ki1R.setText("C o m p u t e r");
            this.addComponent(human1R);
            this.addComponent(ki1R);


            human2R.setBounds(new Rectangle(300, 150, 100, 20));
            human2R.setText("H u m a n   P l a y e r");
            ki2R.setBounds(new Rectangle(300, 180, 100, 20));
            ki2R.setText("C o m p u t e r");
            this.addComponent(human2R);
            this.addComponent(ki2R);


            size8R.setBounds(new Rectangle(100, 270, 100, 20));
            size8R.setText("8 x 8");
            size10R.setBounds(new Rectangle(300, 270, 100, 20));
            size10R.setText("10 x 10");
            this.addComponent(size8R);
            this.addComponent(size10R);

            player1RG.activateButton(human1R.getID());
            player2RG.activateButton(ki2R.getID());
            sizeRG.activateButton(size8R.getID());
        }

        public override void actionPerformed(int id) {
            switch (id) {
                case 6:
                    int size = sizeRG.getActiveButtonId() == 4 ? 8 : 10;

                    Draught.Intelligence p1 = this.getPlayerFromButton(player1RG.getActiveButtonId());
                    Draught.Intelligence p2 = this.getPlayerFromButton(player2RG.getActiveButtonId());

                    guiManager.startGame(size, p1, p2);
                    break;
                case 7:
                    guiManager.closeGame();
                    break;
            }
        }

        private Draught.Intelligence getPlayerFromButton(int buttonID) {
            switch(buttonID){
                case 0: goto case 2;
                case 2:
                    return new Draught.Intelligence(Draught.Intelligence.eType.human);
                case 1: goto case 3;
                case 3:
                    return new Draught.Intelligence(Draught.Intelligence.eType.ai);
                default:
                    return new Draught.Intelligence(Draught.Intelligence.eType.human);
           }
        }
    }
}
