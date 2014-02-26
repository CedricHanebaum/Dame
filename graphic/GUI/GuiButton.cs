using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace graphic.GUI {
	
	class GuiButton: GuiComponent {

		private Bitmap img;
		private String text;
		private bool mouseOver;
		private Color color;
		private Font font;

		public GuiButton(int id, Gui parent): base(id, parent) {
            font = new Font("Arial", 16, FontStyle.Italic);
            color = Color.Black;
		}

		public void setColor(Color color) {
			this.color = color;
		}

		public void setFont(Font font) {
			this.font = font;
		}

		public void setText(string text) {
			this.text = text;
		}
        public void setImage(Bitmap b)
        {
            this.img = b;
        }

		public override void draw(Graphics g) {
			if(img!=null)g.DrawImageUnscaled(img, bounds.Location);
			Brush brush = new SolidBrush(color);
			PointF p = new PointF(bounds.X, bounds.Y + bounds.Height/2);
			g.DrawString(text, font, brush, p);
            g.DrawRectangle(new Pen(Color.Beige), bounds);
			if (mouseOver) {
				Brush transparentBrush = new SolidBrush(Color.FromArgb(200, 200, 200, 60));
				g.FillRectangle(transparentBrush, bounds);
			}
		}

		public override void mouseClicked(MouseEventArgs e) {
			if (bounds.Contains(e.Location)) {
				parent.actionPerformed(this.getID());
			}
		}

		public override void mouseMoved(MouseEventArgs e) {
			if (bounds.Contains(e.Location)) {
				mouseOver = true;
			} else {
				mouseOver = false;
			}
		}
	
	}
}
