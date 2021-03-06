﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ISO {
	
	class Board3D: FilledShape {

		public const int RECTSIZE = 48;
		public const int EDGEWIDTH = 10;

		private Color COLOR_BLACK = Color.Brown;
		private Color COLOR_WHITE = Color.LightYellow;
		private Color EDGE_COLOR = Color.SandyBrown;

		private int size;

		public Board3D(Vector3D basePoint, int size):base(basePoint){
			if (!(size == 8 || size == 10)) throw new ArgumentException();
			this.size = size;

			for (int i = 0; i < size; i++) {
				for (int j = 0; j < size; j++) {
					Color color = (i + j) % 2 == 0 ? COLOR_WHITE: COLOR_BLACK;

					Triangle3D[] rect = this.getRect(i * RECTSIZE, j * RECTSIZE, color);

					this.addTriangle(rect[0]);
					this.addTriangle(rect[1]);
				}
			}

			Vector3D v01 = new Vector3D(0, 0, 0);
			Vector3D v02 = new Vector3D(RECTSIZE * size, 0, 0);
			Vector3D v03 = new Vector3D(0, RECTSIZE * size, 0);
			Vector3D v04 = new Vector3D(RECTSIZE * size, RECTSIZE * size, 0);

			Vector3D v05 = v01 + new Vector3D(-EDGEWIDTH, -EDGEWIDTH, 0);
			Vector3D v06 = v02 + new Vector3D(EDGEWIDTH, -EDGEWIDTH, 0);
			Vector3D v07 = v03 + new Vector3D(-EDGEWIDTH, EDGEWIDTH, 0);
			Vector3D v08 = v04 + new Vector3D(EDGEWIDTH, EDGEWIDTH, 0);

			Vector3D v09 = v01 + new Vector3D(-EDGEWIDTH, 0, 0);
			Vector3D v10 = v02 + new Vector3D(0, -EDGEWIDTH, 0);
			Vector3D v11 = v03 + new Vector3D(0, EDGEWIDTH, 0);
			Vector3D v12 = v04 + new Vector3D(EDGEWIDTH, 0, 0);

			Triangle3D t1 = new Triangle3D(v05, v09, v02, EDGE_COLOR);
			Triangle3D t2 = new Triangle3D(v05, v02, v10, EDGE_COLOR);

			Triangle3D t3 = new Triangle3D(v10, v06, v04, EDGE_COLOR);
			Triangle3D t4 = new Triangle3D(v12, v06, v04, EDGE_COLOR);

			Triangle3D t5 = new Triangle3D(v12, v03, v08, EDGE_COLOR);
			Triangle3D t6 = new Triangle3D(v07, v03, v08, EDGE_COLOR);

			Triangle3D t7 = new Triangle3D(v11, v07, v09, EDGE_COLOR);
			Triangle3D t8 = new Triangle3D(v11, v01, v09, EDGE_COLOR);

			this.addTriangle(t1);
			this.addTriangle(t2);
			this.addTriangle(t3);
			this.addTriangle(t4);
			this.addTriangle(t5);
			this.addTriangle(t6);
			this.addTriangle(t7);
			this.addTriangle(t8);

		}

		public bool isInside(Vector3D v) {
			Vector3D basePoint = this.getBasePoint();
			int length = RECTSIZE * size;

			if (v.getX() >= basePoint.getX() && v.getX() <= basePoint.getX() + length) {
				if (v.getY() >= basePoint.getY() && v.getY() <= basePoint.getY() + length) {
					if (v.getZ() == basePoint.getZ()) {
						return true;
					}
				}
			}

			return false;
		}

		// kaputt
		public int getFieldX(Vector3D v) {
			if (!this.isInside(v)) throw new ArgumentException();

			for (int i = 0; i < size; ++i) {
				for (int j = 0; j < size; ++j) {
					if (this.isInsideField(i, j, v)) return i;
				}
			}

			return -1;
		}

		// kaputt
		public int getFieldY(Vector3D v) {
			if (!this.isInside(v)) throw new ArgumentException();

			for (int i = 0; i < size; ++i) {
				for (int j = 0; j < size; ++j) {
					if (this.isInsideField(i, j, v)) return j;
				}
			}

			return -1;
		}

		private bool isInsideField(int x, int y, Vector3D v) {
			if (x < 0 || x > size) throw new ArgumentException();
			if (y < 0 || y > size) throw new ArgumentException();
			bool ret = false;

			Vector3D basePoint = this.getBasePoint();

			// up left
			Vector3D field1 = new Vector3D(x * RECTSIZE, y * RECTSIZE, 0);
			// down right
			Vector3D field2 = new Vector3D(x * RECTSIZE + RECTSIZE, y * RECTSIZE + RECTSIZE, 0);

			if (v.getX() >= (basePoint + field1).getX() && v.getX() <= (basePoint + field1).getX() + RECTSIZE) {
				if (v.getY() >= (basePoint + field1).getY() && v.getY() <= (basePoint + field1).getY() + RECTSIZE) {
					if (v.getZ() == (basePoint + field1).getZ()) {
						return ret = true;
					}
				}
			}

			return ret;
		}

		private Triangle3D[] getRect(int x, int y, Color color) {
			Triangle3D[] ret = new Triangle3D[2];

			Vector3D v1 = new Vector3D(x, y, 0);
			Vector3D v2 = new Vector3D(x + RECTSIZE, y, 0);
			Vector3D v3 = new Vector3D(x, y + RECTSIZE, 0);
			Vector3D v4 = new Vector3D(x + RECTSIZE, y + RECTSIZE, 0);

			ret[0] = new Triangle3D(v1, v2, v3, color);
			ret[1] = new Triangle3D(v4, v2, v3, color);

			return ret;
		}
	
	}
}
