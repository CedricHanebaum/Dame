using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace graphic {
	
	
	abstract class Drawable: IComparable {

		private int priority;
		protected bool visible;

		protected Drawable(int priority) {
			this.priority = priority;
			visible = false;
		}

		public abstract void draw(Graphics g);

		public void setVisible(bool visible) {
			this.visible = visible;
		}

		public int getPriority() {
			return priority;
		}

		public int CompareTo(object d) {
			return this.priority - ((Drawable)d).priority;
		}
	}
}
