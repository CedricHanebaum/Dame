using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ISO;
using System.Drawing;

namespace ISO {


	class GhostShape {
	
		private List<Line3D> lineList = new List<Line3D>();
		private Vector3D basePoint;

		protected GhostShape(Vector3D basePoint) {
			this.basePoint = basePoint;
		}

		protected void addLine(Line3D l) {
			lineList.Add(l);
		}

		protected Vector3D getBasePoint() {
			return basePoint;
		}

		public void draw(Graphics g) {
			foreach (var t in lineList) {
				(t + basePoint).draw(g);
			}
		}

	}
}
