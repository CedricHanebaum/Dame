using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace graphic.GUI {

	abstract class GuiComponent: IMouseNoticeable {

		protected Rectangle bounds;
		private int id;
		protected Gui parent;

		public GuiComponent(int id, Gui parent) {
			this.id = id;
			this.parent = parent;
		}

		public void setBounds(Rectangle bounds) {
			this.bounds = bounds;
		}

		public bool isInside(Point p) {
			return bounds.Contains(p);
		}

		protected int getID() {
			return id;
		}

		public abstract void draw(Graphics g);

		public abstract void mouseMoved(System.Windows.Forms.MouseEventArgs e);

		public abstract void mouseClicked(System.Windows.Forms.MouseEventArgs e);

	}
}
