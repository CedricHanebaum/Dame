using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

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

        public override void draw(Graphics g)
        {
            SolidBrush brush = new SolidBrush(color);
            g.DrawString(text, font, brush, bounds);
        }

        public override void mouseClicked(MouseEventArgs e)
        {
        }

        public override void mouseMoved(MouseEventArgs e)
        {
        }

    }
}
