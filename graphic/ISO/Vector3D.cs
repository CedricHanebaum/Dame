using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ISO {

    class Vector3D {

        private double x, y, z;

        public Vector3D(double x, double y, double z) {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public double getX() {
            return x;
        }

        public double getY() {
            return y;
        }

        public double getZ() {
            return z;
        }

        public static Vector3D operator +(Vector3D v1, Vector3D v2) {
            return new Vector3D(v1.getX() + v2.getX(), v1.getY() + v2.getY(), v1.getZ() + v2.getZ());
        }

        public static Point Vector3DToISO(Vector3D v) {
            int x = 0, y = 0;

            x = (int)(1 * v.getX() + -1 * v.getY() + 0 * v.getZ());
            y = (int)(0.5 * v.getX() + 0.5 * v.getY() + -1 * v.getZ());

            return new Point(x, y);
        }

        // assuming z = 0
        public static Vector3D ISOToVector3D(Point p) {
            int x, y, z = 0;

            x = (int) (0.5 * p.X + 1 * p.Y);
            y = (int)(-0.5 * p.X + 1 * p.Y);

            return new Vector3D(x, y, z);
        }

        public override string ToString() {
            return "{ " + x + ", " + y + ", " + z + "}";
        }

    }
}
