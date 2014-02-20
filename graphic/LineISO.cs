using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace tmp.graphic {


	class LineISO {

		private Point p1, p2;
		private Color color;

		public LineISO(Point p1, Point p2, Color color) {
			this.p1 = p1;
			this.p2 = p2;

			this.color = color;
		}

		public void draw(Graphics g) {
			Pen pen = new Pen(color, 1);
			g.DrawLine(pen, p1, p2);
		}
	}
}
