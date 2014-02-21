using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ISO {
    
    class Triangle3D {

        private Vector3D v1, v2, v3;
        private Color color;

        public Triangle3D(Vector3D v1, Vector3D v2, Vector3D v3, Color color) {
            this.v1 = v1;
            this.v2 = v2;
            this.v3 = v3;
            this.color = color;
        }

        public Vector3D getV1(){
            return v1;
        }

        public Vector3D getV2() {
            return v2;
        }

        public Vector3D getV3() {
            return v3;
        }

        public Color getColor() {
            return color;
        }

        public static Triangle3D operator +(Triangle3D t, Vector3D v) {
            Vector3D v1, v2, v3;
            v1 = t.getV1() + v;
            v2 = t.getV2() + v;
            v3 = t.getV3() + v;
            return new Triangle3D(v1, v2, v3, t.getColor());
        }

        public static TriangleISO Triangle3DToISO(Triangle3D t) {
            Point p1, p2, p3;
            p1 = Vector3D.Vector3DToISO(t.getV1());
            p2 = Vector3D.Vector3DToISO(t.getV2());
            p3 = Vector3D.Vector3DToISO(t.getV3());

            return new TriangleISO(p1, p2, p3, t.getColor());
        }

        public void draw(Graphics g) {
            Triangle3D.Triangle3DToISO(this).draw(g);
        }

        public override string ToString() {
            return "[ " + v1 + ", " + v2 + ", " + v3 + " ]";
        }
    
    }
}
