﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ISO {

    abstract class FilledShape {

        private List<Triangle3D> triangleList = new List<Triangle3D>();
        private Vector3D basePoint;

        protected FilledShape(Vector3D basePoint) {
            this.basePoint = basePoint;
        }

        protected void addTriangle(Triangle3D t) {
            triangleList.Add(t);
        }

        protected Vector3D getBasePoint() {
            return basePoint;
        }

        public void draw(Graphics g) {
            foreach (var t in triangleList) {
                (t + basePoint).draw(g);
            }
        }
    
    }
}
