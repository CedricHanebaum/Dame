using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ISO {
    
    class TriangleISO {

        private Point p1, p2, p3;
        private Color color;

        public TriangleISO(Point p1, Point p2, Point p3, Color color) {
            this.p1 = p1;
            this.p2 = p2;
            this.p3 = p3;

            //Console.WriteLine(this);

            this.color = color;
        }

        public void draw(Graphics g) {
            SolidBrush brush = new SolidBrush(color);
            Point[] p = {p1, p2, p3 };

            g.FillPolygon(brush, p, System.Drawing.Drawing2D.FillMode.Alternate);
        }

        public override String ToString() {
            return "[ " + p1 + ", " + p2 + ", " + p3 + " ]";
        }

    }
}
