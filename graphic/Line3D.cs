using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsFormsApplication1;
using System.Drawing;

namespace tmp.graphic {

	class Line3D {

		private Vector3D v1, v2;
		private Color color;

		public Line3D(Vector3D v1, Vector3D v2, Color color) {
			this.v1 = v1;
			this.v2 = v2;
			this.color = color;
		}

		public Vector3D getV1() {
			return v1;
		}

		public Vector3D getV2() {
			return v2;
		}

		public Color getColor() {
			return color;
		}

		public static Line3D operator + (Line3D l, Vector3D v){
			Vector3D v1 = l.getV1() + v;
			Vector3D v2 = l.getV2() + v;

			return new Line3D(v1, v2, l.getColor());
		}

		public static LineISO line3DToISO(Line3D l) {
			Point p1 = Vector3D.Vector3DToISO(l.getV1());
			Point p2 = Vector3D.Vector3DToISO(l.getV2());

			return new LineISO(p1, p2, l.getColor());
		}

		public void draw(Graphics g) {
			Line3D.line3DToISO(this).draw(g);
		}
	
	}
}
