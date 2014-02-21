using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace graphic.GUI
{
    class GuiLabel : GuiComponent
    {

        private String text;
        private Color color;
        private Font font;


        public GuiLabel(int ID, Gui parent): base(ID, parent)
        {
            
            color = Color.Black;
            font = new Font("", 12);
        }

        public void setBounds(Rectangle rect)
        {
            this.bounds = rect;
        }

        public void setText(String text)
        {
            this.text = text;
        }

        public void setColor(Color color)
        {
            this.color = color;
        }

        public void setFont(Font font)
        {
            this.font = font;
        }


        public override void mouseClicked(Point p) { }


        public override void draw(Graphics g)
        {
            SolidBrush brush = new SolidBrush(color);
            g.DrawString(text, font, brush, 0, 0);
        }


        public override void mouseMoved(Point p) { }

    }
}
