using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace graphic.GUI {


	class GuiRadioButton: GuiComponent {

		private bool active;
		private GuiRadioButtonGroup group;

		private Color color;
		private string text;
		private Font font;

		public GuiRadioButton(int id, Gui parent): base(id, parent) {
			active = false;
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
			active = true;
		}

		internal void activate() {
			active = false;
		}

		public override void draw(Graphics g) {
			Brush brush1 = new SolidBrush(color);
			PointF p = new PointF(bounds.X + 10, bounds.Y + bounds.Height / 2);
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
	}
}
