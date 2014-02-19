using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace WindowsFormsApplication1 {
    
    class Board3D: Shape {

        public const int RECTSIZE = 48;

        private Color COLOR_BLACK = Color.Brown;
        private Color COLOR_WHITE = Color.LightYellow;

        private int size;

        public Board3D(Vector3D basePoint, int size):base(basePoint){
            Console.WriteLine(size);
            Console.WriteLine(size == 8);
            if (!(size == 8 || size == 10)) throw new ArgumentException();
            this.size = size;

            for (int i = 0; i < size; i++) {
                for (int j = 0; j < size; j++) {
                    Color color = (i + j) % 2 == 0 ? COLOR_BLACK : COLOR_WHITE;

                    Triangle3D[] rect = this.getRect(i * RECTSIZE, j * RECTSIZE, color);

                    this.addTriangle(rect[0]);
                    this.addTriangle(rect[1]);
                }
            }
        }

        public bool isInside(Vector3D v) {
            Console.WriteLine("RelPos: " + new Vector3D(v.getX() - this.getBasePoint().getX(), v.getY() - this.getBasePoint().getY(), v.getZ() - this.getBasePoint().getZ()));

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

            Vector3D basePoint = this.getBasePoint();


            Console.WriteLine(new Vector3D(x * RECTSIZE, y * RECTSIZE, 0));

            Vector3D fieldBase = new Vector3D(basePoint.getX() + x * RECTSIZE, basePoint.getY() + x * RECTSIZE, basePoint.getZ());

            if (v.getX() >= fieldBase.getX() && v.getX() <= fieldBase.getX() + RECTSIZE) {
                if (v.getY() >= fieldBase.getY() && v.getY() <= fieldBase.getY() + RECTSIZE) {
                    if (v.getZ() == fieldBase.getZ()) {
                        return true;
                    }
                }
            }

            return false;
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
