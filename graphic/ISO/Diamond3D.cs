using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ISO {
    
    class Diamond3D: FilledShape {

        public Diamond3D(Vector3D basePoint):base(basePoint){

            Vector3D v1 = new Vector3D(5, 5, 0);
            Vector3D v2 = new Vector3D(-5, 5, 0);
            Vector3D v3 = new Vector3D(5, -5, 0);
            Vector3D v4 = new Vector3D(-5, -5, 0);


            Vector3D v5 = new Vector3D(0, 0, 8);
            Vector3D v6 = new Vector3D(0, 0, -8);

            Triangle3D t1 = new Triangle3D(v5, v1, v2, Color.Green);
            Triangle3D t2 = new Triangle3D(v6, v1, v2, Color.Green);

            Triangle3D t3 = new Triangle3D(v5, v1, v3, Color.DarkGreen);
            Triangle3D t4 = new Triangle3D(v6, v1, v3, Color.DarkGreen);

            this.addTriangle(t1);
            this.addTriangle(t2);
            this.addTriangle(t3);
            this.addTriangle(t4);

        }
    
    
    }
}
