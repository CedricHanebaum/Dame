using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ISO;
using System.Drawing;
using System.Windows.Forms;

namespace graphic.GUI {
	
	abstract class Gui: Drawable, IMouseNoticeable {

		private Bitmap background;
		private Rectangle bounds;
		private List<GuiComponent> componentsList = new List<GuiComponent>();
		private GuiManager guiManager;

		public Gui(GuiManager guiManager): base(1) {
			this.guiManager = guiManager;
		}

		public void setBackground(Bitmap background) {
			this.background = background;
		}

		public void setBounds(Rectangle bounds) {
			this.bounds = bounds;
		}

		public void addComponent(GuiComponent c) {
			componentsList.Add(c);
		}

		public abstract void actionPerformed(int id);

		public override void draw(Graphics g) {
			if (visible) {
				g.DrawImageUnscaled(background, bounds.Location);

				foreach (GuiComponent c in componentsList) {
					c.draw(g);
				}
			}
		}

		public void mouseMoved(MouseEventArgs e) {
			if (bounds.Contains(e.Location) && visible) {
				foreach (var c in componentsList) {
					c.mouseMoved(e);
				}
			}
		}

		public void mouseClicked(MouseEventArgs e) {
			if (bounds.Contains(e.Location)) {
				foreach (var c in componentsList) {
					c.mouseClicked(e);
				}
			}
		}
	
	}
}
