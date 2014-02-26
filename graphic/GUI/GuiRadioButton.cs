using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace graphic.GUI {


	class GuiRadioButton: GuiComponent {

		private bool active;
		private GuiRadioButtonGroup group;

		private Color color;
		private string text;
		private Font font;

		public GuiRadioButton(int id, Gui parent): base(id, parent) {
			active = false;
            font = new Font("Arial", 10, FontStyle.Regular);
            color = Color.Black;
		}

		public void setColor(Color color) {
			this.color = color;
		}

		public void setText(string text) {
			this.text = text;
		}

		public void setFont(Font font) {
			this.font = font;
		}

		public void setGroup(GuiRadioButtonGroup group) {
			this.group = group;
		}

		internal void deactivate() {
			active = false;
		}

		internal void activate() {
			active = true;
		}

		public override void draw(Graphics g) {
			Brush brush1 = new SolidBrush(color);
            PointF p = new PointF(bounds.X + 20, bounds.Y + (bounds.Height - font.Height) / 2);
			g.DrawString(text, font, brush1, p);
			Rectangle rect = new Rectangle(bounds.Location, new Size(10, 10));
            if (active) {
				Brush brush2 = new SolidBrush(Color.Black);
				g.FillEllipse(brush2, rect);
			} else {
				Pen pen = new Pen(Color.Black, 1);
				g.DrawEllipse(pen, rect);
			}
		}

        public override void mouseClicked(MouseEventArgs e)
        {
            if (bounds.Contains(e.Location))
            {
                group.activateButton(this.getID());

            }

        }

        public override void mouseMoved(MouseEventArgs e)
        {
           
        }
	}
}
